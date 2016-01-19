using System;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using OrderForm.Domain.Abstract;

namespace OrderForm.Controllers
{
    public class OrderController: Controller
    {
        private IOrderRepository orderRepository;
        private IOrderProcessor orderProcessor;
        private ISaveOrderProcessor saveOrderProcessor;

        public OrderController(IOrderRepository repository, IOrderProcessor processor,ISaveOrderProcessor saveProcessor)
        {
            orderRepository = repository;
            orderProcessor = processor;
            saveOrderProcessor = saveProcessor;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View("OrderForm", orderRepository.Order);
        }
        [HttpPost]
        public ViewResult Index(Domain.Entities.Order orderForm)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Country = FindCountry(orderForm);
                return View("ConfirmationForm", orderForm);
            }
            else
            {
                return View("OrderForm", orderRepository.Order);
            }
        }
        public ViewResult Success(Domain.Entities.Order orderForm)
        {
            try
            {
                orderProcessor.ProcessOrder(orderForm, FindCountry(orderForm));
                saveOrderProcessor.ProcessOrder(orderForm, FindCountry(orderForm));
            }
            catch (Exception ex)
            {
                string filePath = HostingEnvironment.MapPath(@"~/Orders/") + @"\Error.txt";

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now);
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
                return View("Error");
            }
            return View("Success");
        }

        public string FindCountry(Domain.Entities.Order orderForm)
        {
            foreach (var country in orderRepository.Order.Countries.Where(country => Convert.ToInt32(country.Value) == orderForm.Country))
            {
                return country.Text;
            }
            return string.Empty;
        }
    }
}