using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;
using Api.Models.ViewModels;
using Api.Repositories;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        
      private readonly IPatientRepository _repository;
      public PatientsController(IPatientRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientViewModel>>> GetPatients()
        {
            var patients = await _repository.GetAll();
            return Ok(patients);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
        
            var patient = await _repository.Get(id);
            if (patient == null)
            {
                return NotFound();
            }
            return patient;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<Patient>> PutPatient(int id, Patient patient)
        {
            if (id != patient.PatientId)
            {
                return BadRequest();
            }

            var result = await _repository.Update(id, patient);
            
                if (result == null)
                {
                    return NotFound();
                }
              

            return result;
        }

        // POST: api/Patients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
               return await _repository.Add(patient);

        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
          
            var patient = await _repository.Delete(id);
            if (patient == null)
            {
                return NotFound();
            }
            return patient;
        }

      
    }
}