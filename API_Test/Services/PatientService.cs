using API_Test.Models;
using API_Test.Repositories;

namespace API_Test.Services
{
    public class PatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public List<Patient> GetAllPatients()
        {
            var patients = _patientRepository.GetAll()
                .OrderBy(p => p.pName)
                .ToList();
            if (patients == null || patients.Count == 0)
            {
                throw new InvalidOperationException("No patients found.");
            }
            return patients;
        }

        public Patient GetPatientById(int id)
        {
            var patient = _patientRepository.GetById(id);
            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }
            return patient;
        }

        public Patient GetPatientByName(string name)
        {
            var patient = _patientRepository.GetAll()
                .FirstOrDefault(p => p.pName == name);
            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }
            return patient;
        }
    }
}
