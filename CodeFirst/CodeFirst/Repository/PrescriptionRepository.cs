using CodeFirst.Context;
using CodeFirst.Interfaces;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Repository;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly Apbd9Context _dbContext;

    public PrescriptionRepository(Apbd9Context dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int?> GetCountMedicamentInPrescription(int idPrescrition)
    {
        var numberOfMedicament = await  _dbContext.PrescriptionMedicaments
            .CountAsync(p => p.IdPrescription == idPrescrition);

        return numberOfMedicament;

    }

    public async Task<Prescription?> GetPrescription(int idPrescrition)
    {
        try
        {
            var prescription = await _dbContext.Prescriptions
                .Where(p => p.IdPrescription == idPrescrition)
                .FirstOrDefaultAsync();
            
            return prescription;
        }
        catch (ArgumentNullException e)
        {
            return null;
        }
    }

    public async Task<Prescription> CreatePrescription(Prescription prescription)
    {
        var newPrescription = await _dbContext.Prescriptions.AddAsync(prescription);
        await _dbContext.SaveChangesAsync();
        return newPrescription.Entity;
    }
}