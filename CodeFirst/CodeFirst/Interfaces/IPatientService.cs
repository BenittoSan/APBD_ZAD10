using CodeFirst.DTOs.Response;

namespace CodeFirst.Interfaces;

public interface IPatientService
{
    public Task<PatientInfoDTO> GetFullPatientInfo(int idPatient);
}