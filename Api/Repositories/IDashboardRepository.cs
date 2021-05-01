using Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IDashboardRepository
    {
        Task<decimal> PatientsCount();
        Task<decimal> TotalExpanse();
        Task<decimal> TotalIncomes();
        Task<decimal> NewWeeklyPatientsCount();
        Task<List<FivePatientViewModel>> LastFivePatients();
        Task<List<FiveExpenseViewModel>> LastFiveExpenses();


    }
}
