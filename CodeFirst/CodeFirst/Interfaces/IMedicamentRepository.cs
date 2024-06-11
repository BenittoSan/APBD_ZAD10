using CodeFirst.Models;

namespace CodeFirst.Interfaces;

public interface IMedicamentRepository
{
    Task<Medicament?> GetMedicamentAsync(int idMedicament);
}