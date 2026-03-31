using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _sponsorRepository;

        public SponsorService(ISponsorRepository sponsorRepository)
        {
            _sponsorRepository = sponsorRepository;
        }

        public async Task<IEnumerable<Sponsor>> GetAllAsync()// trae todos los sponsors
        {
            return await _sponsorRepository.GetAllAsync();
        }

        public async Task<Sponsor?> GetByIdAsync(int id)// este metodo busca sponsor por id
        {
            var sponsor = await _sponsorRepository.GetByIdAsync(id);

            if (sponsor == null)
                throw new KeyNotFoundException("Sponsor not found");// si el sponsor existe devuelve el sponsor sino sale msj KNFE

            return sponsor;
        }

        public async Task<Sponsor> CreateAsync(Sponsor sponsor)// crea un sponsor. pero primero hace las validaciones
        {
            // validacion # 1: nombre duplicado
            if (await _sponsorRepository.ExistsByNameAsync(sponsor.Name))
                throw new InvalidOperationException("Sponsor name already exists");

            // validacion # 2: email simple
            if (!sponsor.ContactEmail.Contains("@"))
                throw new InvalidOperationException("Invalid email format");

            await _sponsorRepository.CreateAsync(sponsor);
            return sponsor;
        }

        public async Task<Sponsor> UpdateAsync(int id, Sponsor sponsor)//actualiza el sponsor
        {
            var existing = await _sponsorRepository.GetByIdAsync(id);// trae el sponsor actual bd

            if (existing == null)
                throw new KeyNotFoundException("Sponsor not found");

            if (await _sponsorRepository.ExistsByNameAsync(sponsor.Name) && existing.Name != sponsor.Name)
                throw new InvalidOperationException("Sponsor name already exists");

            existing.Name = sponsor.Name;//
            existing.ContactEmail = sponsor.ContactEmail;
            existing.Phone = sponsor.Phone;
            existing.WebsiteUrl = sponsor.WebsiteUrl;
            existing.Category = sponsor.Category;

            await _sponsorRepository.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)//elimina un sponsor, pero primero valida si el sponsor existe para eliminarlo
        {
            var sponsor = await _sponsorRepository.GetByIdAsync(id);

            if (sponsor == null)
                throw new KeyNotFoundException("Sponsor not found");

            await _sponsorRepository.DeleteAsync(id);
            return true;
        }
    }
}
