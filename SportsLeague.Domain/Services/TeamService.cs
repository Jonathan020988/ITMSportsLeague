using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly ILogger<TeamService> _logger;

    public TeamService(ITeamRepository teamRepository, ILogger<TeamService> logger)
    {
        _teamRepository = teamRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Team>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all teams");//metodo para loguear la informacion, traduce obtener todos los equipos
        return await _teamRepository.GetAllAsync();
    }

    public async Task<Team?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving team with ID: {TeamId}", id);
        var team = await _teamRepository.GetByIdAsync(id);

        if (team == null)
            _logger.LogWarning("Team with ID {TeamId} not found", id);

        return team;
    }

    public async Task<Team> CreateAsync(Team team)
    {
        // Validación de negocio: nombre único
        var existingTeam = await _teamRepository.GetByNameAsync(team.Name);
        if (existingTeam != null)// si el team existe entra al if, sino existe pasa al logger
        {
            _logger.LogWarning("Team with name '{TeamName}' already exists", team.Name);
            throw new InvalidOperationException(
                $"Ya existe un equipo con el nombre '{team.Name}'");
        }

        _logger.LogInformation("Creating team: {TeamName}", team.Name);
        return await _teamRepository.CreateAsync(team);
    }

    public async Task UpdateAsync(int id, Team team)
    {
        var existingTeam = await _teamRepository.GetByIdAsync(id);
        if (existingTeam == null)
        {
            _logger.LogWarning("Team with ID {TeamId} not found for update", id);
            throw new KeyNotFoundException(
                $"No se encontró el equipo con ID {id}");
        }

        // Validar nombre único (si cambió)
        if (existingTeam.Name != team.Name) 
        {
            var teamWithSameName = await _teamRepository.GetByNameAsync(team.Name);
            if (teamWithSameName != null)// si los nombres son diferentes entra al if, y verifico, si  no es nula significa que el equipo ya existe, pero si es nula se salta el if y actualiza los datos 
            {
                throw new InvalidOperationException(
                    $"Ya existe un equipo con el nombre '{team.Name}'");
            }
        }

        existingTeam.Name = team.Name;
        existingTeam.City = team.City;
        existingTeam.Stadium = team.Stadium;
        existingTeam.LogoUrl = team.LogoUrl;
        existingTeam.FoundedDate = team.FoundedDate;

        _logger.LogInformation("Updating team with ID: {TeamId}", id);
        await _teamRepository.UpdateAsync(existingTeam);
    }

    public async Task DeleteAsync(int id)
    {
        var exists = await _teamRepository.ExistsAsync(id);// exists es una variable boleana y recibo un true o false, 
        if (!exists)// aca estoy negando la existencia con !,si recibe true el equipo existe  y si recibe un false entra al if
        {
            _logger.LogWarning("Team with ID {TeamId} not found for deletion", id);
            throw new KeyNotFoundException(
                $"No se encontró el equipo con ID {id}");
        }

        _logger.LogInformation("Deleting team with ID: {TeamId}", id);
        await _teamRepository.DeleteAsync(id);
    }
}
