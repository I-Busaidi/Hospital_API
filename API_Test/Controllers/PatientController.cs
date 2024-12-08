using API_Test.Models;
using API_Test.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Test.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("GetPatients")]
        public IActionResult GetAllPatients()
        {
            try
            {
                var patients = _patientService.GetAllPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddPatient([FromBody] Patient patient)
        {
            try
            {
                string newPatientName = _patientService.AddPatient(patient);
                return Created(string.Empty, newPatientName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
