using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SponsorController : ControllerBase
{
    private readonly ITournamentSponsorService _tournamentSponsorService;// dependencias inyectadas
    private readonly ISponsorService _sponsorService;
    private readonly IMapper _mapper;

    public SponsorController(//inyecta los servicios
        ISponsorService sponsorService,
        ITournamentSponsorService tournamentSponsorService,
        IMapper mapper)
    {
        _sponsorService = sponsorService;
        _tournamentSponsorService = tournamentSponsorService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SponsorResponseDTO>>> GetAll()//trae todos lo sponsrs
    {
        var sponsors = await _sponsorService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<SponsorResponseDTO>>(sponsors));// los convierte a dtos
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SponsorResponseDTO>> GetById(int id)//busca el sponosr
    {
        var sponsor = await _sponsorService.GetByIdAsync(id);

        if (sponsor == null)
            return NotFound(new { message = $"El Patrocinador con ID {id} no fue encontrado" });

        return Ok(_mapper.Map<SponsorResponseDTO>(sponsor));
    }

    [HttpPost]
    public async Task<ActionResult<SponsorResponseDTO>> Create(SponsorRequestDTO dto)// crear
    {
        try
        {
            var sponsor = _mapper.Map<Sponsor>(dto);
            var created = await _sponsorService.CreateAsync(sponsor);
            var responseDto = _mapper.Map<SponsorResponseDTO>(created);

            return CreatedAtAction(nameof(GetById),
                new { id = responseDto.Id },
                responseDto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, SponsorRequestDTO dto)//actualiza
    {
        try
        {
            var sponsor = _mapper.Map<Sponsor>(dto);
            await _sponsorService.UpdateAsync(id, sponsor);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)// elimina
    {
        try
        {
            await _sponsorService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    //relaciones 

    [HttpPost("{id}/tournaments")]// para asociar un sponsor a un torneo
    public async Task<ActionResult> LinkToTournament(int id, TournamentSponsorRequestDTO dto)
    {
        try
        {
            await _tournamentSponsorService.LinkSponsorToTournamentAsync(
                id,
                dto.TournamentId,
                dto.ContractAmount
            );

            return Ok(new { message = "Sponsor vinculado al torneo correctamente" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}/tournaments")]// para ver en que torneos esta un sponsor
    public async Task<ActionResult<IEnumerable<TournamentSponsorResponseDTO>>> GetTournamentsBySponsor(int id)
    {
        var relations = await _tournamentSponsorService.GetBySponsorIdAsync(id);

        return Ok(_mapper.Map<IEnumerable<TournamentSponsorResponseDTO>>(relations));
    }

    [HttpDelete("{id}/tournaments/{tournamentId}")]//eliminar la relacion de sponsor-torneo
    public async Task<ActionResult> UnlinkSponsor(int id, int tournamentId)
    {
        try
        {
            await _tournamentSponsorService.UnlinkSponsorFromTournamentAsync(id, tournamentId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}