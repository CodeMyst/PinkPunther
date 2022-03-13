using Microsoft.AspNetCore.Mvc;
using PinkPunter.Backend.Models;
using PinkPunter.Backend.Services;

namespace PinkPunter.Backend.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PunsController : ControllerBase
{
    private readonly PunsService _punsService;

    public PunsController(PunsService punsService) => _punsService = punsService;

    [HttpGet]
    public async Task<List<Pun>> Get() => await _punsService.GetAsync();
}