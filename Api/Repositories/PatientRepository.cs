using Api.Data;
using Api.Models;
using Api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientDbContext _context;

        public PatientRepository(PatientDbContext context)
        {
            _context = context;
        }
        public async Task<Patient> Add(Patient patient)
        {
            patient.CreatedAt = DateTime.Now;
            Console.WriteLine(patient.CreatedAt);
            _context.Patients.Add(patient);

            await _context.SaveChangesAsync();

            return  patient;
        }

        public async Task<Patient> Delete(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return null;
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<Patient> Get(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            return patient;
        }

        public async Task<List<PatientViewModel>> GetAll()
        {
            var patients = await _context.Patients.Include(p => p.Treatments)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            var result = new List<PatientViewModel>();
            foreach (var patient in patients)
            {
                var totalCost = patient.Treatments.Sum(t => t.TreatmentCost);
                result.Add(new PatientViewModel()
                {
                    PatientId = patient.PatientId,
                    UserId = patient.UserId,
                    Age = patient.Age,
                    Gender = patient.Gender,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    TotalTreatmentCost = totalCost,
                    CreatedAt = patient.CreatedAt
                });
            }
            return  result;
        }
        public async Task<List<AdvanceReportViewModel>> GetAllWithTreatments()
        {
            List<AdvanceReportViewModel> patients = (from patient in _context.Patients
                            join t in _context.Treatments on patient.PatientId equals t.PatientId
                            join ty in _context.TreatmentTypes on t.TreatmentTypeId equals ty.TreatmentTypeId
                            select new AdvanceReportViewModel()
                            {
                                ReportKind = "Patient",
                                TypeOfReportKind = ty.Name,
                                Date = t.CreatedAt,
                                Balance = t.TreatmentCost
                            }
                            ).ToList();
              
            foreach (var patient in patients)
            {

              
            }
                /* var result = new List<PatientViewModel>();
                 foreach (var patient in patients)
                 {
                     var totalCost = patient.Treatments.Sum(t => t.TreatmentCost);
                     result.Add(new PatientViewModel()
                     {
                         PatientId = patient.PatientId,
                         UserId = patient.UserId,
                         Age = patient.Age,
                         Gender = patient.Gender,
                         FirstName = patient.FirstName,
                         LastName = patient.LastName,
                         PhoneNumber = patient.PhoneNumber,
                         TotalTreatmentCost = totalCost,
                         CreatedAt = patient.CreatedAt
                     });
                 }*/
                return patients;
        }

        public async Task<Patient> Update(int id, Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            var result = await _context.Patients.FindAsync(id);

            return  result;


        }
        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.PatientId == id);
        }
    }
}
