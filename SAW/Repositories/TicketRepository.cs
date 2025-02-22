using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SAW.Models;
using SAW.DTO.Ticket;
using SAW.Infrastructure;

namespace SAW.Repositories
{
    public class TicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Pobranie listy biletów dla wydarzenia
        public async Task<List<Ticket>> GetTicketsForEventAsync(long eventId)
        {
            return await _context.Set<Ticket>()
                .Where(t => t.EventEntity.Id == eventId)
                .ToListAsync();
        }

        // Pobranie biletu po ID z projekcją
        public async Task<TicketProjections?> FindTicketByIdAsync(long ticketId)
        {
            return await _context.Set<Ticket>()
                .Where(t => t.Id == ticketId)
                .Select(t => new TicketProjections
                {
                    Id = t.Id,
                    Barcode = t.Barcode,
                    PurchaseDate = t.PurchaseDate,
                    EventEntity = t.EventEntity,
                    UserEntity = t.UserEntity
                })
                .FirstOrDefaultAsync();
        }

        // Dodanie nowego biletu
        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            await _context.Set<Ticket>().AddAsync(ticket);
            await SaveChangesAsync(); // Zapisanie zmian w bazie danych
            return ticket;
        }

        // Usunięcie biletu
        public async Task DeleteAsync(Ticket ticket)
        {
            _context.Set<Ticket>().Remove(ticket);
            await SaveChangesAsync(); // Zapisanie zmian po usunięciu
        }

        // Usunięcie biletów przekazanych w liście
        public async Task DeleteTicketsAsync(ICollection<Ticket> tickets)
        {
            _context.Set<Ticket>().RemoveRange(tickets);
            await SaveChangesAsync(); // Zapisanie zmian po usunięciu biletów
        }


        // Pobranie biletu po ID
        public async Task<Ticket?> GetByIdAsync(long ticketId)
        {
            return await _context.Set<Ticket>().FindAsync(ticketId);
        }

        // Zapisanie zmian w bazie danych
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync(); // Metoda SaveChangesAsync zapisuje zmiany w bazie
        }
    }
}


