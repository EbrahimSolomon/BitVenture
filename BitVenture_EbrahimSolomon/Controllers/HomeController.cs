using BitVenture_EbrahimSolomon.Data;
using BitVenture_EbrahimSolomon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System;
using System.Globalization;
using System.IO;
using System.Linq;


namespace BitVenture_EbrahimSolomon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IDataRepository _dataRepository;

        public HomeController(ILogger<HomeController> logger, DataContext context, IDataRepository dataRepository)
        {
            _logger = logger;
            _context = context;
            _dataRepository = dataRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImportFile()
        {
            return View();
        }

        public IActionResult ViewImports()
        {
            var masterRecords = _context.MasterRecords.ToList();
            var detailRecords = _context.DetailRecords.ToList();

            var viewModel = new ImportsViewModel
            {
                MasterRecords = masterRecords,
                DetailRecords = detailRecords
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Report()
        {
            var reportData = await _dataRepository.GetReportDataAsync();
            return View(reportData);
        }
    }
}