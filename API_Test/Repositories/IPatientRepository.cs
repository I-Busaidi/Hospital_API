using API_Test.Models;

namespace API_Test.Repositories
{
    public interface IPatientRepository
    {
        string Add(Patient patient);
        IEnumerable<Patient> GetAll();
        Patient GetById(int id);
    }
}