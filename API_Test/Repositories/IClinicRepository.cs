﻿using API_Test.Models;

namespace API_Test.Repositories
{
    public interface IClinicRepository
    {
        string Add(Clinic clinic);
        IEnumerable<Clinic> GetAll();
        Clinic GetById(int id);
    }
}