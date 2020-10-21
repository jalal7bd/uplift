using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Extensions;
using Uplift.Models;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace Uplift.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public CartViewModel CartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartVM = new CartViewModel()
            {
                OrderHeader = new Models.OrderHeader(),
                ProductList = new List<Product>()
            };
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach (int productId in sessionList)
                {
                    CartVM.ProductList.Add(_unitOfWork.Product.GetFirstOrDefault(s => s.Id == productId, includeProperties: "Frequency,Category"));
                }
            }
            return View(CartVM);

        }
        public IActionResult Summary()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach (int productId in sessionList)
                {
                    CartVM.ProductList.Add(_unitOfWork.Product.GetFirstOrDefault(s => s.Id == productId, includeProperties: "Frequency,Category"));
                }
            }
            return View(CartVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPost()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                CartVM.ProductList = new List<Product>();
                foreach (int productId in sessionList)
                {
                    CartVM.ProductList.Add(_unitOfWork.Product.GetFirstOrDefault(s => s.Id == productId, includeProperties: "Frequency,Category"));
                }
            }
            if (!ModelState.IsValid)
            {
                return View(CartVM);
            }
            else
            {
                CartVM.OrderHeader.OrderDate = DateTime.Now;
                CartVM.OrderHeader.Status = SD.StatusSubmitted;
                CartVM.OrderHeader.ProductCount = CartVM.ProductList.Count;
                _unitOfWork.OrderHeader.Add(CartVM.OrderHeader);
                _unitOfWork.Save();

                foreach (var item in CartVM.ProductList)
                {
                    OrderDetails orderDetails = new OrderDetails
                    {
                        ProductId = item.Id,
                        OrderheaderId = CartVM.OrderHeader.Id,
                        ProductName = item.Name,
                        Quantity=item.Frequency.FrequencyCount,
                        Price = item.B2CPrice
                    };
                    _unitOfWork.OrderDetails.Add(orderDetails);
                }
                _unitOfWork.Save();
                HttpContext.Session.SetObject(SD.SessionCart, new List<int>());
                return RedirectToAction("OrderConfirmation", "Cart", new { id = CartVM.OrderHeader.Id });
            }

        }
        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }
        public IActionResult Remove(int productId)
        {

            List<int> sessionList = new List<int>();
            sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            sessionList.Remove(productId);
            HttpContext.Session.SetObject(SD.SessionCart,sessionList);
            return RedirectToAction(nameof(Index));

        }
    }
}
