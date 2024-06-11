namespace CodeFirst.DTOs.Request;

public class AddPrescritionToPatienDTO
{
    public PatientDTO patient { get; set; }
    public DoctorDTO doctor { get; set; }
    public PrescriptionDTO prescription { get; set; }
    public MedicamentDTO medicament { get; set; }
    
}