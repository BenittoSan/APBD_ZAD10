using CodeFirst.Context;
using CodeFirst.Exceptions;
using CodeFirst.Interfaces;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Repository;

public class MedicamentRepository : IMedicamentRepository
{
    private readonly Apbd9Context _dbContext;

    public MedicamentRepository(Apbd9Context dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Medicament?> GetMedicamentAsync(int idMedicament)
    {
        try
        {
            var medicament = await _dbContext.Medicaments.Where(m => m.IdMedicament == idMedicament)
                .FirstOrDefaultAsync();
            if (medicament == null)
            {
                throw new DomainException($"Medicament with id {idMedicament} does not exist");
            }

            return medicament;

        }
        catch (ArgumentNullException e)
        {
            return null;
        }
    }
}