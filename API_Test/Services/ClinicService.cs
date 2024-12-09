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
                .FirstOrDefault(c => c.cSpec.ToLower().Trim() == name.ToLower().Trim());

            if (clinic == null)
            {
                throw new KeyNotFoundException("Could not find clinic with this name.");
            }

            return clinic;
        }

        public string AddClinic(Clinic clinic)
        {
            var clinics = _clinicRepository.GetAll().ToList();
            if (clinics.Any(c => c.cSpec.ToLower().Trim() == clinic.cSpec.ToLower().Trim()))
            {
                throw new ArgumentException("Clinic with this specialization already exists.");
            }

            if (string.IsNullOrWhiteSpace(clinic.cSpec))
            {
                throw new ArgumentException("Clinic specialization is required.");
            }

            return _clinicRepository.Add(clinic);
        }

        public void UpdateClinic (int id, Clinic clinic)
        {
            var existingClinic = _clinicRepository.GetById(id);
            var clinicByName = _clinicRepository.GetAll().FirstOrDefault(c => c.cSpec.ToLower().Trim() == clinic.cSpec.ToLower().Trim());
            if (existingClinic == null)
            {
                throw new KeyNotFoundException("Could not find clinic.");
            }

            if (string.IsNullOrWhiteSpace(clinic.cSpec))
            {
                throw new ArgumentException("Clinic Specialization is required.");
            }

            if (clinicByName != null && clinicByName.cId != id)
            {
                throw new ArgumentException("A clinic with this specialization already exists.");
            }

            if (clinic.numberOfSlots <= 0)
            {
                throw new ArgumentException("Number of appointment slots must be greater than 0.");
            }

            _clinicRepository.Update(id, clinic);
        }

        public void DeleteClinic (int id)
        {
            var clinic = _clinicRepository.GetById(id);
            if (clinic == null)
            {
                throw new KeyNotFoundException("Could not find clinic.");
            }

            if (clinic.Appointments != null || clinic.Appointments.Count > 0)
            {
                throw new InvalidOperationException("Clinic has pending appointments.");
            }

            _clinicRepository.Delete(id);
        }

        public void UpdateClinicSpecialization(int id, string newSpec)
        {
            var clinic = _clinicRepository.GetById(id);
            if (string.IsNullOrWhiteSpace(newSpec))
            {
                throw new ArgumentException("Clinic specialization cannot be empty.");
            }
            var clinicByName = _clinicRepository.GetAll().FirstOrDefault(c => c.cSpec.ToLower().Trim() == newSpec.ToLower().Trim());
            if (clinic == null)
            {
                throw new KeyNotFoundException("Could not find clinic.");
            }

            if (clinicByName != null && clinicByName.cId != id)
            {
                throw new ArgumentException("A clinic with this specialization already exists.");
            }

            clinic.cSpec = newSpec;
            _clinicRepository.Update(id, clinic);
        }
    }
}
