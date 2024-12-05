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
    }
}
