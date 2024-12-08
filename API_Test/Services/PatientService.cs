﻿using API_Test.Models;
using API_Test.Repositories;

namespace API_Test.Services
{
    public class PatientService : IPatientService
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

        public string AddPatient(Patient patient)
        {
            var patients = _patientRepository.GetAll().ToList();
            if (string.IsNullOrWhiteSpace(patient.pName))
            {
                throw new ArgumentException("Name is required.");
            }

            if (patients.Any(p => p.pName.ToLower().Trim() == patient.pName.ToLower().Trim()))
            {
                throw new ArgumentException("Patient with this name already exists.");
            }

            if (patient.gender.ToLower().Trim() != "male" && patient.gender.ToLower().Trim() != "female")
            {
                throw new ArgumentException("Gender is not valid.");
            }

            return _patientRepository.Add(patient);
        }
    }
}
