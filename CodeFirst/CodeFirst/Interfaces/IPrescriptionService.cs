using CodeFirst.DTOs.Request;

namespace CodeFirst.Interfaces;

public interface IPrescriptionService
{
    Task<PrescriptionDTO?> CreatePrescription(AddPrescritionToPatienDTO addPrescritionToPatienDto);
}