using Api.Models.ViewModels;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {

        private readonly IReportRepository _repository;
        public ReportController(IReportRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportViewModel>>> GetReports()
        {


            var reports = await _repository.GetAllReports();
            return Ok(reports);
        }

        // GET: api/advanceReport
        [HttpGet("advanceReport")]
        public async Task<ActionResult<IEnumerable<ReportViewModel>>> GetAdavanceReports()
        {

            var advanceReports = await _repository.GetAllAdvanceReports();
            return Ok(advanceReports);
        }

    }
}
