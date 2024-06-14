using System;
using System.Collections.Generic;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Context;

public partial class Apbd9Context : DbContext
{
    public Apbd9Context()
    {
    }

    public Apbd9Context(DbContextOptions<Apbd9Context> options)
        : base(options)
    {
    }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<User> Users { get; set; }

}
