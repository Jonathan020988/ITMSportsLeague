using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers;

[ApiController]// dataannottations es para validar el modelo sino lo es devuelve un 400, esto lo hace segun lo configurado en el dbcontext
[Route("api/[controller]")]// enruta las urls, en este caso team
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly IMapper _mapper;  

    public TeamController(
        ITeamService teamService,
        IMapper mapper)
    {
        _teamService = teamService;
        _mapper = mapper;       
    }

    [HttpGet]// para obtener
    public async Task<ActionResult<IEnumerable<TeamResponseDTO>>> GetAll()// obtiene todos los teams
    {
        var teams = await _teamService.GetAllAsync();
        var teamsDto = _mapper.Map<IEnumerable<TeamResponseDTO>>(teams);//retorna una lista, el automaper es el que mapea del dto a la entidad
        return Ok(teamsDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamResponseDTO>> GetById(int id)// si el equipo es nulo devuelvame un msj notfound
    {
        var team = await _teamService.GetByIdAsync(id);

        if (team == null)
            return NotFound(new { message = $"Equipo con ID {id} no encontrado" });

        var teamDto = _mapper.Map<TeamResponseDTO>(team);
        return Ok(teamDto);
    }

    [HttpPost]
    public async Task<ActionResult<TeamResponseDTO>> Create(TeamRequestDTO dto)
    {
        try
        {
            var team = _mapper.Map<Team>(dto);
            var createdTeam = await _teamService.CreateAsync(team);
            var responseDto = _mapper.Map<TeamResponseDTO>(createdTeam);

            return CreatedAtAction(
                nameof(GetById),
                new { id = responseDto.Id },
                responseDto);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, TeamRequestDTO dto)
    {
        try
        {
            var team = _mapper.Map<Team>(dto);
            await _teamService.UpdateAsync(id, team);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _teamService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
