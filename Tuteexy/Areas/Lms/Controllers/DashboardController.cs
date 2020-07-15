using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Utility;

namespace Tuteexy.Areas.Lms.Controllers
{
    [Area("Lms")]
    [Authorize(Roles = SD.Role_User)]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(ILogger<DashboardController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}