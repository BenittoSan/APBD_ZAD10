namespace CodeFirst.DTOs.Request;

public class PatientDTO
{
    public int idPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birth { get; set; }
}