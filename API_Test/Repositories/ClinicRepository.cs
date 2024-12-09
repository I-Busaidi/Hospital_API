using API_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Test.Repositories
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly ApplicationDbContext _context;
        public ClinicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Clinic> GetAll()
        {
            return _context.Clinics.Include(c => c.Appointments).ToList();
        }

        public Clinic GetById(int id)
        {
            return _context.Clinics.Include(c => c.Appointments).FirstOrDefault(c => c.cId == id);
        }

        public string Add(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            _context.SaveChanges();
            return clinic.cSpec;
        }

        public void Update(int id, Clinic newClinic)
        {
            var existingClinic = GetById(id);
            if (existingClinic != null)
            {
                existingClinic.cSpec = newClinic.cSpec;
                existingClinic.numberOfSlots = newClinic.numberOfSlots;

                _context.Clinics.Update(existingClinic);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var clinic = GetById(id);
            if (clinic != null)
            {
                _context.Clinics.Remove(clinic);
                _context.SaveChanges();
            }
        }
    }
}
