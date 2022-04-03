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
    [Area("Admin"), Authorize]
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> _logger;
        public TicketController(ILogger<TicketController> logger) => _logger = logger;

    
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateTicketModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateTicketModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateTicket();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create ticket");
                    _logger.LogError(ex, "Failed to Create Ticket");
                }
            }
            return View(model);
        }

        public JsonResult GetTicketsData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new TicketListModel();
            var data = model.GetTickets(dataTablesModel);
            return Json(data);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new EditTicketModel();
            model.LoadTicketData(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditTicketModel model)
        {
            model.Update();
            return View(model);
        }
       
        public IActionResult Delete(int id)
        {
            var model = new CreateTicketModel();
            model.Delete(id);
            return Redirect(nameof(Index));
        }
    }
}
