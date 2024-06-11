using CodeFirst.DTOs.Response;
using CodeFirst.Interfaces;

namespace CodeFirst.Services;

public class PatientService : IPatientService
{
    private readonly IMedicamentRepository _medicamentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PatientService(IMedicamentRepository medicamentRepository, IPatientRepository patientRepository,
        IPrescriptionRepository prescriptionRepository)
    {
        _medicamentRepository = medicamentRepository;
        _patientRepository = patientRepository;
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<PatientInfoDTO> GetFullPatientInfo(int idPatient)
    {
        var patien = await _patientRepository.GetFullPatientInfo(idPatient);
        return patien;



    }
}