using Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IExpenseRepository _expenseRepository;

        public DashboardRepository(IPatientRepository patientRepository, IExpenseRepository expenseRepository) {
            _patientRepository = patientRepository;
            _expenseRepository = expenseRepository;
        }
        public async Task<List<FiveExpenseViewModel>> LastFiveExpenses()
        {
            return await _expenseRepository.GetLastFiveExpenses();
        }

        public async Task<List<FivePatientViewModel>> LastFivePatients()
        {
            return await _patientRepository.GetLastFivePatients();
        }

        public async Task<decimal> NewWeeklyPatientsCount()
        {
            return await _patientRepository.GetNewWeeklyPatientsCount();
        }

        public async Task<decimal> PatientsCount()
        {
            return await _patientRepository.GetPatientsCount();
        }

        public async Task<decimal> TotalExpanse()
        {
            return await _expenseRepository.GetTotalExpenses();        }

        public async Task<decimal> TotalIncomes()
        {
            return await _patientRepository.GetTotalIncomes();
        }
    }
}
