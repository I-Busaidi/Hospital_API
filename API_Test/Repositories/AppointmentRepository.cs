using API_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Test.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _context.appointments
                .Include(p => p.Patient)
                .Include(c => c.Clinic)
                .ToList();
        }

        public Appointment GetById(int id)
        {
            return _context.appointments
                .Include(p => p.Patient)
                .Include(c => c.Clinic)
                .FirstOrDefault(a => a.appointmentId == id);
        }

        public (DateTime, string, string) Add(Appointment appointment)
        {
            _context.appointments.Add(appointment);
            _context.SaveChanges();
            return (appointment.appointmentDate, appointment.Clinic.cSpec, appointment.Patient.pName);
        }

        public void Update(int id, Appointment newAppointment)
        {
            var existingAppointment = GetById(id);
            if (existingAppointment != null)
            {
                existingAppointment.slotNumber = newAppointment.slotNumber;
                existingAppointment.appointmentDate = newAppointment.appointmentDate;
                existingAppointment.pId = newAppointment.pId;
                existingAppointment.cId = newAppointment.cId;

                _context.appointments.Update(existingAppointment);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var appointment = GetById(id);
            if (appointment != null)
            {
                _context.appointments.Remove(appointment);
                _context.SaveChanges();
            }
        }
    }
}
