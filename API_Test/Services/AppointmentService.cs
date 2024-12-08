using API_Test.Models;
using API_Test.Repositories;

namespace API_Test.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IClinicService _clinicService;
        private readonly IPatientService _patientService;

        public AppointmentService(IAppointmentRepository appointmentRepository, IClinicService clinicService, IPatientService patientService)
        {
            _appointmentRepository = appointmentRepository;
            _clinicService = clinicService;
            _patientService = patientService;
        }

        public List<Appointment> GetAllAppointments()
        {
            var appointments = _appointmentRepository.GetAll()
                .OrderBy(ap => ap.appointmentDate)
                .ToList();

            if (appointments == null || appointments.Count == 0)
            {
                throw new InvalidOperationException("No appointments found.");
            }
            return appointments;
        }

        public List<Appointment> GetAppointmentsByDate(DateTime date)
        {
            var appointments = _appointmentRepository.GetAll()
                .Where(ap => ap.appointmentDate.Date == date.Date)
                .ToList();

            if (appointments == null || appointments.Count == 0)
            {
                throw new InvalidOperationException("No appointments found on this date.");
            }
            return appointments;
        }

        public List<Appointment> GetPatientAppointments(string name)
        {
            var patient = _patientService.GetPatientByName(name);
            if (patient.Appointments == null || patient.Appointments.Count == 0)
            {
                throw new InvalidOperationException($"{name} has no appointments.");
            }
            return patient.Appointments.ToList();
        }

        public List<Appointment> GetClinicAppointments(string name)
        {
            var clinic = _clinicService.GetClinicByName(name);
            if (clinic.Appointments == null || clinic.Appointments.Count == 0)
            {
                throw new InvalidOperationException($"{name} clinic has no appointments currently.");
            }
            return clinic.Appointments.ToList();
        }

        public string AddAppointment(string clinicName, string patientName, DateTime date)
        {
            var clinic = _clinicService.GetClinicByName(clinicName);
            var patient = _patientService.GetPatientByName(patientName);
            var appointments = _appointmentRepository.GetAll();

            int appointmentCount = clinic.Appointments.Count(ap => ap.appointmentDate.Date == date.Date);

            if (appointmentCount >= clinic.numberOfSlots)
            {
                throw new ArgumentException($"No slots available for this date in {clinicName} clinic.");
            }

            if (patient.Appointments != null || patient.Appointments.Count > 0)
            {
                if (patient.Appointments.Any(ap => ap.appointmentDate.Date == date.Date && ap.cId == clinic.cId))
                {
                    throw new InvalidOperationException($"Patient {patientName} already has an appointment in {clinicName} clinic on this date.");
                }
            }

            var createAppointment = _appointmentRepository.Add(new Appointment
            {
                appointmentDate = date,
                slotNumber = appointmentCount + 1,
                cId = clinic.cId,
                pId = patient.pId
            });

            return $"Appointment created for {createAppointment.Item3} in {createAppointment.Item2} clinic on {createAppointment.Item1}.";
        }
    }
}
