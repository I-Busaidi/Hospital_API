using API_Test.Models;

namespace API_Test.Services
{
    public interface IClinicService
    {
        string AddClinic(Clinic clinic);
        List<Clinic> GetAllClinics();
        Clinic GetClinicById(int id);
        Clinic GetClinicByName(string name);
    }
}