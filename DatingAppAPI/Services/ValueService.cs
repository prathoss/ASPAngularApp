using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingAppAPI.Data;
using DatingAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingAppAPI.Services
{
    public class ValueService : IValueService
    {
        DataContext _context;

        public ValueService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Value>> GetAllAsync()
        {
            return await _context.Values.ToListAsync();
        }

        public async Task<Value> GetByIdAsync(int id)
        {
            return await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
