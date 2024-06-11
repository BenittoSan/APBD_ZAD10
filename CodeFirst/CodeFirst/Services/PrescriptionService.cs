using System.Data;
using CodeFirst.DTOs.Request;
using CodeFirst.Exceptions;
using CodeFirst.Interfaces;
using CodeFirst.Models;

namespace CodeFirst.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IMedicamentRepository _medicamentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionService(IMedicamentRepository medicamentRepository,
        IPatientRepository patientRepository,
        IPrescriptionRepository prescriptionRepository)
    {
        _medicamentRepository = medicamentRepository;
        _patientRepository = patientRepository;
        _prescriptionRepository = prescriptionRepository;
    }
    
    public async Task<PrescriptionDTO?> CreatePrescription(AddPrescritionToPatienDTO addPrescritionToPatienDto)
    {
        try
        {

            var patient = await _patientRepository.GetPatient(addPrescritionToPatienDto.patient.idPatient);
            if (patient == null)
            {
                await _patientRepository.CreatePatient(
                    new Patient
                    {
                        FirstName = addPrescritionToPatienDto.patient.FirstName,
                        LastName = addPrescritionToPatienDto.patient.LastName,
                        Birthdate = addPrescritionToPatienDto.patient.Birth,
                    });
            }

            var medicamentAsync =
                await _medicamentRepository.GetMedicamentAsync(addPrescritionToPatienDto.medicament.idMedicament);

            if (medicamentAsync == null)
            {
                throw new DomainException($"Medicament with id {addPrescritionToPatienDto.medicament.idMedicament}" +
                                          $"dose not exist");
            }

            var numerOfMedicament = await _prescriptionRepository
                .GetCountMedicamentInPrescription(addPrescritionToPatienDto.prescription.idPrescription);

            int maxMedicaments = 10;
            if (numerOfMedicament >= maxMedicaments)
            {
                throw new DataException($"Prescription can not have more than {maxMedicaments} medicaments");
            }

            if (addPrescritionToPatienDto.prescription.DueDate <= addPrescritionToPatienDto.prescription.Date)
            {
                throw new DataException("Error Date");
            }

            Prescription prescription = new Prescription
            {
                Date = addPrescritionToPatienDto.prescription.Date,
                DueDate = addPrescritionToPatienDto.prescription.DueDate,
                IdPatient = addPrescritionToPatienDto.patient.idPatient,
                IdDoctor = addPrescritionToPatienDto.doctor.IdDoctor
            };

            var newPrescription = await _prescriptionRepository.CreatePrescription(prescription);

            PrescriptionDTO responsePrescription = addPrescritionToPatienDto.prescription;

            return responsePrescription;
            

        }
        catch (ArgumentNullException e)
        {
            return null;
        }
    }
}