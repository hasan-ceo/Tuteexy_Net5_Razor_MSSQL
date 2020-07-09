using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Titan.DataAccess.Data;
using Titan.DataAccess.Repository.IRepository;
using Titan.Utility;

namespace Titan.Areas.Ironman.Controllers
{
    [Area("Ironman")]
    [Authorize(Roles = SD.Role_Ironman)]
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