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
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        // GET: api/PatientCount
        [HttpGet("PatientCount")]
        public async Task<ActionResult<decimal>> GetPatientsCount()
        {

            return await _dashboardRepository.PatientsCount();
        }

        // GET: api/LastFivePatients
        [HttpGet("LastFivePatients")]
        public async Task<ActionResult<List<FivePatientViewModel>>> GetLastFivePatients()
        {
            return  Ok( await _dashboardRepository.LastFivePatients());
        }
        // GET: api/TotalExpanse
        [HttpGet("TotalExpanse")]
        public async Task<ActionResult<decimal>> GetTotalExpanse()
        {
            return await _dashboardRepository.TotalExpanse();
        }

        // GET: api/TotalIncomes
        [HttpGet("TotalIncomes")]
        public async Task<ActionResult<decimal>> GetTotalIncomes()
        {
            return await _dashboardRepository.TotalIncomes();
        }
        // GET: api/NewWeeklyPatientsCount
        [HttpGet("NewWeeklyPatientsCount")]
        public async Task<ActionResult<decimal>> GetNewWeeklyPatientsCount()
        {
            return await _dashboardRepository.NewWeeklyPatientsCount();
        }

        // GET: api/LastFiveExpenses
        [HttpGet("LastFiveExpenses")]
        public async Task<ActionResult<List<FiveExpenseViewModel>>> GetLastFiveExpenses()
        {
            return await _dashboardRepository.LastFiveExpenses();
        }
    }
}
