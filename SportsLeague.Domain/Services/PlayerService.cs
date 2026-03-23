using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services;

public class PlayerService : IPlayerService//6 METODOS
{
    private readonly IPlayerRepository _playerRepository;// dependencias
    private readonly ITeamRepository _teamRepository;
    private readonly ILogger<PlayerService> _logger;

    public PlayerService(
        IPlayerRepository playerRepository,
        ITeamRepository teamRepository,
        ILogger<PlayerService> logger)
    {
        _playerRepository = playerRepository;
        _teamRepository = teamRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Player>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all players");
        return await _playerRepository.GetAllWithTeamAsync();
    }

    public async Task<Player?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving player with ID: {PlayerId}", id);//jugador por id
        var player = await _playerRepository.GetByIdWithTeamAsync(id);

        if (player == null)
            _logger.LogWarning("Player with ID {PlayerId} not found", id);

        return player;
    }

    public async Task<IEnumerable<Player>> GetByTeamAsync(int teamId)
    {
        // Validar que el equipo existe
        var teamExists = await _teamRepository.ExistsAsync(teamId);
        if (!teamExists)
        {
            _logger.LogWarning("Team with ID {TeamId} not found", teamId);
            throw new KeyNotFoundException($"No se encontró el equipo con ID {teamId}");
            //excepcion controlada para decir q no se encontro ese equipo con ese id
        }

        _logger.LogInformation("Retrieving players for team ID: {TeamId}", teamId);
        return await _playerRepository.GetByTeamAsync(teamId);
    }

    public async Task<Player> CreateAsync(Player player)
    {
        // Validar que el equipo existe
        var teamExists = await _teamRepository.ExistsAsync(player.TeamId);
        if (!teamExists)                       // si  este equipo existe omita lo siguiente luego valida # camiseta
        {
            _logger.LogWarning("Team with ID {TeamId} not found", player.TeamId);
            throw new KeyNotFoundException(
                $"No se encontró el equipo con ID {player.TeamId}");
        }

        // Validar número de camiseta único en el equipo
        var existingPlayer = await _playerRepository
            .GetByTeamAndNumberAsync(player.TeamId, player.Number);
        if (existingPlayer != null)
        {
            _logger.LogWarning(
                "Number {Number} already taken in team {TeamId}",
                player.Number, player.TeamId);
            throw new InvalidOperationException(
                $"El número {player.Number} ya está en uso en este equipo");
            // si el # de camiseta lo tiene alguien mas sale esta excepcion, pero esta es diferente sale esta operacion es invalida
        }

        _logger.LogInformation("Creating player: {FirstName} {LastName}",
            player.FirstName, player.LastName);// despues de validar lo anterior hace esto
        return await _playerRepository.CreateAsync(player);
    }

    public async Task UpdateAsync(int id, Player player)
    {
        var existingPlayer = await _playerRepository.GetByIdAsync(id);
        if (existingPlayer == null)
        {
            throw new KeyNotFoundException(
                $"No se encontró el jugador con ID {id}");
        }

        // Validar que el nuevo equipo existe
        var teamExists = await _teamRepository.ExistsAsync(player.TeamId);
        if (!teamExists)
        {
            throw new KeyNotFoundException(
                $"No se encontró el equipo con ID {player.TeamId}");
        }

        // Validar número único (si cambió el número o el equipo)
        if (existingPlayer.Number != player.Number || existingPlayer.TeamId != player.TeamId)
        {                                         //ó
            var conflict = await _playerRepository
                .GetByTeamAndNumberAsync(player.TeamId, player.Number);
            if (conflict != null && conflict.Id != id)
            {
                throw new InvalidOperationException(
                    $"El número {player.Number} ya está en uso en este equipo");
            }
        }

        existingPlayer.FirstName = player.FirstName;
        existingPlayer.LastName = player.LastName;
        existingPlayer.BirthDate = player.BirthDate;
        existingPlayer.Number = player.Number;
        existingPlayer.Position = player.Position;
        existingPlayer.TeamId = player.TeamId;

        _logger.LogInformation("Updating player with ID: {PlayerId}", id);
        await _playerRepository.UpdateAsync(existingPlayer);
    }

    public async Task DeleteAsync(int id)
    {
        var exists = await _playerRepository.ExistsAsync(id);
        if (!exists)
        {
            throw new KeyNotFoundException(
                $"No se encontró el jugador con ID {id}");
        }

        _logger.LogInformation("Deleting player with ID: {PlayerId}", id);
        await _playerRepository.DeleteAsync(id);
    }
}
