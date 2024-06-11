using CodeFirst.DTOs.Request;
using CodeFirst.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers;

[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost("PrescriptionDTP:AddPrescritionToPatienDTO")]
    public async Task<IActionResult> CreatePrescription(AddPrescritionToPatienDTO prescritionToPatienDto)
    {
        var prescription = await _prescriptionService.CreatePrescription(prescritionToPatienDto);

        if (prescription == null)
        {
            return BadRequest();
        }

        return Ok(prescription);

    }

}