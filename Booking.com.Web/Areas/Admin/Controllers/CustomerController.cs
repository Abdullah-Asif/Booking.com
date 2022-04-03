using Booking.com.Web.Models;
using Booking.com.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.com.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ILogger<CustomerController> logger) => _logger = logger;
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public IActionResult Create()
        {
            var model = new CreateCustomerModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateCustomer();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create customer");
                    _logger.LogError(ex, "Failed to Create Customer");
                }
            }
            return View(model);
        }

        public JsonResult GetCustomersData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new CustomerListModel();
            var data = model.GetCustomers(dataTablesModel);
            return Json(data);
        }
    
        public IActionResult Edit(int id)
        {
            var model = new EditCustomerModel();
            model.LoadCustomerData(id);
            return View(model);
        }
        [HttpPost]
        
        public IActionResult Edit(EditCustomerModel model)
        {
            model.Update();
            return View(model);
        }
   
        public IActionResult Delete(int id)
        {
            var model = new CreateCustomerModel();
            model.Delete(id);
            return Redirect(nameof(Index));
        }
    }
}
