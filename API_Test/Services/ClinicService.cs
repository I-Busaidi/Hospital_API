using API_Test.Models;
using API_Test.Repositories;

namespace API_Test.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepository;
        public ClinicService(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        public List<Clinic> GetAllClinics()
        {
            var clinics = _clinicRepository.GetAll()
                .OrderBy(c => c.cSpec)
                .ToList();
            if (clinics == null || clinics.Count == 0)
            {
                throw new InvalidOperationException("No clinics found.");
            }
            return clinics;
        }

        public Clinic GetClinicById(int id)
        {
            var clinic = _clinicRepository.GetById(id);
            if (clinic == null)
            {
                throw new KeyNotFoundException("Could not find clinic with this ID.");
            }
            return clinic;
        }

        public Clinic GetClinicByName(string name)
        {
            var clinic = _clinicRepository.GetAll()
                .FirstOrDefault(c => c.cSpec == name);

            if (clinic == null)
            {
                throw new KeyNotFoundException("Could not find clinic with this name.");
            }

            return clinic;
        }

        public string AddClinic(Clinic clinic)
        {
            var clinics = _clinicRepository.GetAll().ToList();
            if (clinics.Any(c => c.cSpec == clinic.cSpec))
            {
                throw new ArgumentException("Clinic with this specialization already exists.");
            }

            if (string.IsNullOrWhiteSpace(clinic.cSpec))
            {
                throw new ArgumentException("Clinic specialization is required.");
            }

            return _clinicRepository.Add(clinic);
        }
    }
}
