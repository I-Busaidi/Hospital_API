using API_Test.Models;

namespace API_Test.Services
{
    public interface IClinicService
    {
        string AddClinic(Clinic clinic);
        List<Clinic> GetAllClinics();
        Clinic GetClinicById(int id);
        Clinic GetClinicByName(string name);
        void UpdateClinicSpecialization(int id, string newSpec);
        void DeleteClinic(int id);
        void UpdateClinic(int id, Clinic clinic);
    }
}