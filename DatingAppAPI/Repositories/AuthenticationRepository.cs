﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingAppAPI.Data;
using DatingAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingAppAPI.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DataContext _context;

        public AuthenticationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string userName, string password)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Name == userName);
            if (user == null) return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return null;

            //authentication successful
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordSalt = hmac.Key;
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Name == username)) return true;
            return false;
        }
    }
}