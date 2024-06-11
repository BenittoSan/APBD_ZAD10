using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models;

public class PrescriptionMedicament
{
    [Key,Column(Order = 0)]
    public int IdMedicament { get; set; }
    
    [Key,Column(Order = 1)]
    public Medicament Medicament { get; set; }
    
    [Required]
    public int IdPrescription { get; set; }
    
    [ForeignKey(nameof(IdPrescription))]
    public Prescription Prescription { get; set; }
    
    public int? Dose { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
}