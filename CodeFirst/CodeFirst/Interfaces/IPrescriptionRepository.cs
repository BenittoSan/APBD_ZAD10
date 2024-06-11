using CodeFirst.Models;

namespace CodeFirst.Interfaces;

public interface IPrescriptionRepository
{
    Task<int?> GetCountMedicamentInPrescription(int idPrescrition);
    Task<Prescription?> GetPrescription(int idPrescrition);

    Task<Prescription> CreatePrescription(Prescription prescription);
}