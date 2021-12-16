using RossyStore.Core.Repositories.Order_Repository;
using RossyStore.Core.Repositories.Product_Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Repositories.PDF_Repository
{
    public  class TemplateGenerator
    {
        private OrderRepository order = new OrderRepository();
        public string GetHTMLString(string order_id)
        {
            var data = order.Get_Order(order_id);
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "PDF_Template", "template.html");
            string tempHtml = File.ReadAllText(templatePath);
            string data_invoice = $@"
                Invoice #: {data.id}<br />
                OrderDate: {data.order_date.ToString("MM dd, yyyy")}<br />
            ";
            string data_customer = $@"
                {data.customer.customer_name}<br />
                {data.customer.customer_address}<br />
                {data.customer.customer_phoneNumber}8<br />
                {data.customer.customer_email}
            ";

            string data_product = "";
            foreach (var item in data.list_cart)
            {
                var product = new ProductRepository().Get_Product(item.id);
                data_product += $@"
                    <tr class='item'>
                        <td class='Name'>{product.product_name}</td>
                        <td class='color'>{item.Product_detail_color}</td>
                        <td class='size'>{item.Product_detail_size}</td>
                        <td class='amount'>{item.Quantity}</td>
                        <td class='price'>{String.Format("{0:0,0}", product.product_price)}</td>
                        <td class='price'>{String.Format("{0:0,0}", item.Quantity * product.product_price)}</td>
                    </tr>
                ";
            }

            string grand_total = $"<td class='price'>{String.Format("{0:0,0}", data.order_total)}</td>";

            return tempHtml
                .Replace("{data_invoice}", data_invoice)
                .Replace("{data_customer}", data_customer)
                .Replace("{data_product}", data_product)
                .Replace("{grand_total}", grand_total);
        }
    }
}
