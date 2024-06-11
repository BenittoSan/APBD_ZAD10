using CodeFirst.DTOs.Response;
using CodeFirst.Models;

namespace CodeFirst.Interfaces;

public interface IPatientRepository
{
    Task<Patient?> GetPatient(int idPatient);
    Task<Patient> CreatePatient(Patient patient);

    Task<PatientInfoDTO?> GetFullPatientInfo(int idPatient);
}