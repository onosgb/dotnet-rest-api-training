using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> AddAsync(CreateStockRequestDto stock);
        Task<Stock?> UpdateAsync(int id, UpdatStockRequestDto stock);
        Task<Stock?> DeleteAsync(int id);
    }
}