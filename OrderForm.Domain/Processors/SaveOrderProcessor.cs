using System;
using System.IO;
using System.Text;
using System.Web.Hosting;
using OrderForm.Domain.Abstract;
using OrderForm.Domain.Entities;

namespace OrderForm.Core
{
    public class SaveOrderProcessor:ISaveOrderProcessor
    {
        private static readonly string FilePath = HostingEnvironment.MapPath(@"~/Orders/")+@"\OrdersDB.txt";
        public void ProcessOrder(Order order, string country)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                StringBuilder body = new StringBuilder()
                    .AppendLine("")
                    .AppendLine("-----------------------------------------------------------------------------")
                    .AppendLine("New Order:");

                body.AppendFormat("Customer Name: {0:c}", order.Name)
                    .AppendLine(string.Empty)
                     .AppendLine("Delivery Data:")
                     .AppendLine(country)
                     .AppendLine(order.City)
                     .AppendLine(order.Address ?? "")
                     .AppendLine("---")
                     .AppendFormat("Safety  pack: {0}",
                         order.IsPacked ? "Yes" : "No");
                writer.WriteLine(body + Environment.NewLine + "Date :" + DateTime.Now);
                writer.WriteLine(Environment.NewLine +
                                 "-----------------------------------------------------------------------------" +
                                 Environment.NewLine);
            }
        }
    }
}
