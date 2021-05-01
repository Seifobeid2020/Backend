using Api.Models;
using Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IPatientRepository
    {
        Task<List<PatientViewModel>> GetAll();
        Task<List<AdvanceReportViewModel>> GetAllWithTreatments();
        Task<Patient> Get(int id);
        Task<Patient> Add(Patient patient);
        Task<Patient> Update(int id, Patient patient);
        Task<Patient> Delete(int id);
        Task<decimal> GetPatientsCount();
        Task<decimal> GetTotalIncomes();
        Task<decimal> GetNewWeeklyPatientsCount();
        Task<List<FivePatientViewModel>> GetLastFivePatients();

     

    }
}
