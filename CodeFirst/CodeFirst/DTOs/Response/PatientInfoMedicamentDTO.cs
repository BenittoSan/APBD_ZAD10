namespace CodeFirst.DTOs.Response;

public class PatientInfoMedicamentDTO
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
   
    public string Description { get; set; }
    public string Type { get; set; }
    
    public string Details { get; set; }
}