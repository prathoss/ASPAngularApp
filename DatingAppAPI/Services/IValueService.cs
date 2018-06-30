using DatingAppAPI.Data;
using DatingAppAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingAppAPI.Services
{
    public interface IValueService
    {
        Task<Value> GetByIdAsync(int id);
        Task<List<Value>> GetAllAsync();
    }
}
