using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ISponsorService
    {
        Task<IEnumerable<Sponsor>> GetAllAsync();// trae todos los sponsors
        Task<Sponsor?> GetByIdAsync(int id);//busca un sponsor por id
        Task<Sponsor> CreateAsync(Sponsor sponsor);// crea sponsor
        Task<Sponsor> UpdateAsync(int id, Sponsor sponsor);//actualiza un sponsor, tambien valida que existan duplicados
        Task<bool> DeleteAsync(int id);// elimina sponsor
    }
}
