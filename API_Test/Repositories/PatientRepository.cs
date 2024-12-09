using API_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Test.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.Include(p => p.Appointments).ToList();
        }

        public Patient GetById(int id)
        {
            return _context.Patients.Include(p => p.Appointments).FirstOrDefault(p => p.pId == id);
        }

        public string Add(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return patient.pName;
        }

        public void Update(int id, Patient newPatient)
        {
            var existingPatient = GetById(id);

            if (existingPatient != null)
            {
                existingPatient.age = newPatient.age;
                existingPatient.gender = newPatient.gender;
                existingPatient.pName = newPatient.pName;
                
                _context.Patients.Update(existingPatient);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var patient = GetById(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }
    }
}
