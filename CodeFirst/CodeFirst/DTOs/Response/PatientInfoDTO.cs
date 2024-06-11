namespace CodeFirst.DTOs.Response;

public class PatientInfoDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birth { get; set; }
    public List<PatientInfoPrescriptionDTO> Prescriptions { get; set; }
}