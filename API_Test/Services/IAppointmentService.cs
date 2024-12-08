using API_Test.Models;

namespace API_Test.Services
{
    public interface IAppointmentService
    {
        string AddAppointment(string clinicName, string patientName, DateTime date);
        List<Appointment> GetAllAppointments();
        List<Appointment> GetAppointmentsByDate(DateTime date);
        List<Appointment> GetClinicAppointments(string name);
        List<Appointment> GetPatientAppointments(string name);
    }
}