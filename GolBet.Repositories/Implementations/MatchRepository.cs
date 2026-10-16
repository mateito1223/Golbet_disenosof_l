using GolBet.Entities;
using GolBet.Entities.Enums;
using GolBet.Repositories.Data;
using GolBet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolBet.Repositories.Implementations;
public class MatchRepository : GenericRepository<Match>, IMatchRepository

{

    public MatchRepository(AppDbContext context) : base(context) { }
    public async Task<IEnumerable<Match>> GetAllWithTeamsAsync(MatchStatus? status = null, string? search = null) // Cambiamos la firma igual que la 
                                                                                                                  // interfaz para que acepte un
                                                                                                                  // parámetro de búsqueda opcional

    {
        var query = _dbSet
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Where(m => m.IsActive)
            .AsNoTracking()
            .AsQueryable();

        if (status.HasValue)
            query = query.Where(m => m.Status == status.Value);

        // Agregamos la lógica de búsqueda por nombre de equipo local o visitante
        // para que el parámetro search sea opcional y no genere errores si es nulo o vacío
        if (!string.IsNullOrWhiteSpace(search))

        {
            var term = search.Trim().ToLower(); 
            query = query.Where(m => m.HomeTeam.Name.ToLower().Contains(term)    
                                  || m.AwayTeam.Name.ToLower().Contains(term));  // Buscamos en el nombre del equipo y el ToLower() funciona para 
                                                                                 // que la búsqueda ignore a mayúsculas y minúsculas
                                                                                 // y el contains para que busque coincidencias parciales
                                                                                 // en el nombre del equipo local o visitante.
        }

        return await query.OrderBy(m => m.Date).ToListAsync();

    }



    public async Task<Match?> GetByIdWithDetailsAsync(int id)

        => await _dbSet

            .Include(m => m.HomeTeam)

            .Include(m => m.AwayTeam)

            .Include(m => m.Bets)

            .AsNoTracking()

            .FirstOrDefaultAsync(m => m.Id == id);

}
