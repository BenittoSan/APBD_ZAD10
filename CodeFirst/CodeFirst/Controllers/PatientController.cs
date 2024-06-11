using CodeFirst.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{idPatient:int}")]
    public async Task<IActionResult> GetFullInfoOfPatient(int idPatient)
    {
        var patient = await _patientService.GetFullPatientInfo(idPatient);
        if (patient == null)
        {
            return NoContent();
        }

        return Ok(patient);
    }

}