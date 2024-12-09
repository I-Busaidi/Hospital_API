using API_Test.Models;

namespace API_Test.Repositories
{
    public interface IAppointmentRepository
    {
        (DateTime, string, string) Add(Appointment appointment);
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        void Update(int id, Appointment newAppointment);
        void Delete(int id);
    }
}