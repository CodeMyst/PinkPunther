using Microsoft.AspNetCore.Mvc;
using PinkPunter.Backend.Models;
using PinkPunter.Backend.Services;

namespace PinkPunter.Backend.Controllers;

[ApiController]
[Route("/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class PunsController : ControllerBase
{
    private readonly PunsService _punsService;
    private readonly PunSubmissionsService _punSubmissionsService;

    public PunsController(PunsService punsService, PunSubmissionsService punSubmissionsService)
    {
        _punsService = punsService;
        _punSubmissionsService = punSubmissionsService;
    }

    [HttpPost("Submit")]
    public async Task<PunSubmission> Submit([FromBody] PunSubmissionCreateInfo createInfo)
    {
        var submission = new PunSubmission
        {
            SubmittedAt = DateTime.Now,
            Type = createInfo.Type,
            Question = createInfo.Question,
            Answer = createInfo.Type == PunType.QuestionAnswer ? createInfo.Answer : null
        };

        await _punSubmissionsService.CreateSubmissionAsync(submission);

        return submission;
    }
}