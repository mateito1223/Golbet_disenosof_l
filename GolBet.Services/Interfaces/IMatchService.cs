using GolBet.Entities.Enums;
using GolBet.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolBet.Services.Interfaces;

public interface IMatchService

{

    /// <summary>Match board: all active matches ordered by date.</summary>
   

    // ANTES
    //Task<IEnumerable<MatchDto>> GetBoardAsync(MatchStatus? status = null);

    // DESPUÉS
    Task<IEnumerable<MatchDto>> GetBoardAsync(MatchStatus? status = null, string? search = null); // Agregamos el contrato del
                                                                                                  // parámetro search como opcional
    Task<MatchDetailDto?> GetDetailAsync(int id);
    Task<MatchFormDto?> GetForEditAsync(int id);
    Task CreateAsync(MatchFormDto dto);
    Task UpdateAsync(MatchFormDto dto);
    Task DeactivateAsync(int id);
}
