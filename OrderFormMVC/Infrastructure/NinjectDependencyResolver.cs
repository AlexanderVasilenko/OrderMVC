using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using OrderForm.Core;
using OrderForm.Domain.Abstract;
using OrderForm.Domain.DB;
using OrderForm.Domain.Entities;
using OrderForm.Domain.Processors;

namespace OrderFormMVC.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IOrdersRepository>().To<OrdRepository>();
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            mock.Setup(m => m.Order).Returns(
                new Order() {
                    Countries = new List<SelectListItem>
                    {
                        new SelectListItem {Value = "1", Text = "USA"},
                        new SelectListItem {Value = "2", Text = "Canada"},
                        new SelectListItem {Value = "3", Text = "Brazil"}
                    }
            });
            kernel.Bind<IOrderRepository>().ToConstant(mock.Object);

            EmailSettings emailSettings = new EmailSettings
            {
                MailToAddress = ConfigurationManager.AppSettings["Email.MailToAddress"],
                MailFromAddress = ConfigurationManager.AppSettings["Email.MailFromAddress"],
                UseSsl = bool.Parse(ConfigurationManager.AppSettings["Email.UseSsl"]),
                Username = ConfigurationManager.AppSettings["Email.Username"],
                Password = ConfigurationManager.AppSettings["Email.Password"],
                ServerName = ConfigurationManager.AppSettings["Email.ServerName"],
                ServerPort = int.Parse(ConfigurationManager.AppSettings["Email.ServerPort"])
            };

            kernel.Bind<IOrderProcessor>().To<MailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            kernel.Bind<ISaveOrderProcessor>().To<SaveOrderProcessor>();
        }
    }
}
