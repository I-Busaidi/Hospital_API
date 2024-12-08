using API_Test.Models;
using API_Test.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Test.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicService;

        public ClinicController (IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        [HttpGet("GetClinics")]
        public IActionResult GetAllClinics()
        {
            try
            {
                var clinics = _clinicService.GetAllClinics();
                return Ok(clinics);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddClinic(string clinicName)
        {
            try
            {
                string newClinic = _clinicService.AddClinic(new Clinic
                {
                    cSpec = clinicName
                });
                return Created(string.Empty, newClinic);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
