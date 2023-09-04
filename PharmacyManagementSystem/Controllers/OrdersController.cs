using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using PharmacyManagementSystem.Dtos.EmailDto;
using PharmacyManagementSystem.Dtos.OrderDtos;
using PharmacyManagementSystem.Interface;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.Services.EmailService;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder IOrder;
        private readonly IEmailService IEmail;

        public OrdersController(IOrder IOrder, IEmailService IEmail)
        {
            this.IOrder = IOrder;
            this.IEmail = IEmail;
        }

        // GET: api/Orders
        [HttpGet, Authorize(Roles = "Admin")
            ]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            try
            {
                return await IOrder.GetAllOrders();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet, Route("GetMyOrders"), Authorize(Roles = "Doctor")
            ]
        public async Task<ActionResult<IEnumerable<Order>>> GetMyOrders()
        {
            try
            {

                return await IOrder.GetMyOrders();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // GET: api/Orders/5
        [HttpGet("{id}"), Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {

                return await IOrder.GetOrder(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/Orders/5
        [HttpGet("GetOrderByUserId/{userId}"), Authorize(Roles = "Doctor")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserId(int userId)
        {
            try
            {
                return await IOrder.GetOrderByUserId(userId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // POST: api/Orders
        [HttpPost, Route("PlaceOrder"), Authorize(Roles = "Doctor")]
        public async Task<ActionResult<Order>> PlaceOrder(PlaceOrderDto request)
        {
            try
            {

                Order newOrder = await IOrder.PlaceOrder(request);
                if (newOrder != null)
                {
                    EmailDto email = new EmailDto()
                    {
                        Subject = $"Your Order Confirmation-[{newOrder.order_no}]",
                        Body = "We are pleased to inform you that your order has been successfully placed and is now being processed.",
                        To = request.createOrderDto.email
                    };
                    await this.IEmail.SendEmail(email);
                    return Ok(newOrder);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet, Route("getOrderDetailsByOrderId/{orderId}"), Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult<OrderResponse>> getOrderDetailsByOrderId(int orderId)
        {
            try
            {
                OrderResponse or = await IOrder.getOrderDetailsByOrderId(orderId);
                if (or != null)
                {
                    return Ok(or);
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("Generatepdf")]

        public async Task<IActionResult> GeneratePdf(string inNo)
        {
            var doc = new PdfDocument();
            string HtmlContent = " <div style='width: 100%; text-align: center'> ";
            HtmlContent += "<h1>Invoice from PMS</h1>";
            HtmlContent += "<h3>Dr.Sangam Dange</h3>";

            HtmlContent += "<table style ='width:100%; border: 1px solid #000'>";
            HtmlContent += "<thead style='font-weight:bold'>";
            HtmlContent += "<tr>";
            HtmlContent += "<td style='border:1px solid #000'> Total amount </td>";
            HtmlContent += "<td style='border:1px solid #000'> Items ordered </td>";
            HtmlContent += "<td style='border:1px solid #000'>Ordered date</td>";
            HtmlContent += "</tr>";
            HtmlContent += "</thead >";

            HtmlContent += "<tbody>";
            HtmlContent += "<tr>";
            HtmlContent += "<td>" + "400" + "</td>";
            HtmlContent += "<td>" + "5" + "</td>";
            HtmlContent += "<td>" + "12/03/2023" + "</td >";
            HtmlContent += "</tr>";
            HtmlContent += "</tbody>";
            HtmlContent += "</table>";

            HtmlContent += " <h3>Order Details</h3>";

            HtmlContent += "<table style ='width:100%; border: 1px solid #000'>";
            HtmlContent += "<thead style='font-weight:bold'>";
            HtmlContent += "<tr>";
            HtmlContent += "<td style='border:1px solid #000'> Drug Name </td>";
            HtmlContent += "<td style='border:1px solid #000'> Expiry Date </td>";
            HtmlContent += "<td style='border:1px solid #000'>Price</td>";
            HtmlContent += "<td style='border:1px solid #000'>Quantity Bought</td>";
            HtmlContent += "<td >Sub Total</td>";
            HtmlContent += "</tr>";
            HtmlContent += "</thead >";

            HtmlContent += "<tbody>";
            HtmlContent += "<tr>";
            HtmlContent += "<td style='border-right:1px solid #000'>" + "para" + "</td>";
            HtmlContent += "<td style='border-right:1px solid #000'>" + "12/03/2023" + "</td>";
            HtmlContent += "<td style='border-right:1px solid #000'>" + "1200" + "</td >";
            HtmlContent += "<td style='border-right:1px solid #000'>" + "12" + "</td >";
            HtmlContent += "<td >" + "1200" + "</td >";
            HtmlContent += "</tr>";
            HtmlContent += "<tr>";
            HtmlContent += "<td style='border-right:1px solid #000'>" + "para" + "</td>";
            HtmlContent += "<td style='border-right:1px solid #000'>" + "12/03/2023" + "</td>";
            HtmlContent += "<td style='border-right:1px solid #000'>" + "1200" + "</td >";
            HtmlContent += "<td style='border-right:1px solid #000'>" + "12" + "</td >";
            HtmlContent += "<td >" + "1200" + "</td >";
            HtmlContent += "</tr>";
            HtmlContent += "</tbody>";
            HtmlContent += "</table>";
            HtmlContent += "</div>";
            PdfGenerator.AddPdfPages(doc, HtmlContent, PageSize.A4);
            byte[] res = null;

            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                res = ms.ToArray();
            }
            string Filename = "Invoice_" + inNo + ".pdf";

            return File(res, "application/pdf", Filename);
        }


        [HttpGet, Route("updateOrderStatus/{orderId}"), Authorize(Roles = "Admin")]
        public async Task<Order> updateOrderStatus(int orderId)
        {
            try
            {
                return await IOrder.updateOrderStatus(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}"), Authorize(Roles = "Doctor")
            ]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                bool check = await IOrder.DeleteOrder(id);
                if (check)
                {
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
