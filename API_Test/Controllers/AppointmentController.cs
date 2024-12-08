using API_Test.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Test.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("GetAppointments")]
        public IActionResult GetAllAppointments()
        {
            try
            {
                var appointments = _appointmentService.GetAllAppointments();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAppointmentByPatient/{pName}")]
        public IActionResult GetPatientAppointments(string pName)
        {
            try
            {
                var appointments = _appointmentService.GetPatientAppointments(pName);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAppointmentByClinic/{cSpec}")]
        public IActionResult GetClinicAppointments(string cSpec)
        {
            try
            {
                var appointments = _appointmentService.GetClinicAppointments(cSpec);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult BookAppointment(string clinicName, string patientName, DateTime date)
        {
            try
            {
                string appointmentDetails = _appointmentService.AddAppointment(clinicName, patientName, date);
                return Created(string.Empty, appointmentDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
