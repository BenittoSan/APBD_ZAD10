using CodeFirst.Context;
using CodeFirst.DTOs.Response;
using CodeFirst.Exceptions;
using CodeFirst.Interfaces;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly Apbd9Context  _dbContext;

    public PatientRepository(Apbd9Context dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Patient?> GetPatient(int idPatient)
    {
        try
        {
            var patient = await _dbContext.Patients.Where(p => p.IdPatient == idPatient).FirstOrDefaultAsync();
            
            return patient;

        }
        catch (ArgumentNullException e)
        {
            return null;
        }
    }

    public async Task<Patient> CreatePatient(Patient patient)
    {
        try
        {
            var newPatien = patient;
            await _dbContext.Patients.AddAsync(newPatien);
           await _dbContext.SaveChangesAsync();
           return newPatien;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<PatientInfoDTO?> GetFullPatientInfo(int idPatient)
    {
        var patient = await _dbContext.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .Where(p => p.IdPatient == idPatient)
            .Select(p => new PatientInfoDTO
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Birth = p.Birthdate,
                Prescriptions = p.Prescriptions.Select(pr => new PatientInfoPrescriptionDTO
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Medicaments = pr.PrescriptionMedicaments.Select(m => new PatientInfoMedicamentDTO
                    {
                        IdMedicament = m.IdMedicament,
                        Name = m.Medicament.Name,
                        Description = m.Medicament.Description,
                        Type = m.Medicament.Type,
                        Details = m.Details
                    }).ToList(),
                    Doctor = new PatientInfoDoctorDTO
                    {
                        IdDoctor = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName,
                        LastName = pr.Doctor.LastName,
                        Email = pr.Doctor.Email
                    }
                }).ToList()

            }).FirstOrDefaultAsync();

        return patient;
    }
}