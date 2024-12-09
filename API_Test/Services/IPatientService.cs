using API_Test.Models;

namespace API_Test.Services
{
    public interface IPatientService
    {
        string AddPatient(Patient patient);
        List<Patient> GetAllPatients();
        Patient GetPatientById(int id);
        Patient GetPatientByName(string name);
        void DeletePatient(int id);
        void UpdatePatient(int id, Patient patient);
    }
}