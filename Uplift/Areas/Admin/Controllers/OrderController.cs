using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace Uplift.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            OrderVM orderVM = new OrderVM()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(id),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(filter: o => o.OrderheaderId == id)
            };
            return View(orderVM);
        }
        public IActionResult Approve(int id)
        {
            var orderFromdb = _unitOfWork.OrderHeader.Get(id);
            if (orderFromdb==null)
            {
                return NotFound();
            }
            _unitOfWork.OrderHeader.ChangeOrderStatus(id, SD.StatusApproved);
            return View(nameof(Index));
        }
        public IActionResult Reject(int id)
        {
            var orderFromdb = _unitOfWork.OrderHeader.Get(id);
            if (orderFromdb == null)
            {
                return NotFound();
            }
            _unitOfWork.OrderHeader.ChangeOrderStatus(id, SD.StatusRejected);
            return View(nameof(Index));
        }
        #region API Calls
        public IActionResult GetAllOrder()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll() });
        }
        public IActionResult GetAllPendingOrder()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o=>o.Status==SD.StatusSubmitted) }) ;
        }
        public IActionResult GetAllApprovedOrder()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o=>o.Status==SD.StatusApproved) });
        }
        public IActionResult GetAllRejectedOrder()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o => o.Status == SD.StatusRejected) });
        }
        #endregion
    }
}
