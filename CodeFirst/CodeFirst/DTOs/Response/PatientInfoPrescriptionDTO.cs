namespace CodeFirst.DTOs.Response;

public class PatientInfoPrescriptionDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<PatientInfoMedicamentDTO> Medicaments { get; set; }
    public PatientInfoDoctorDTO Doctor { get; set; }
}