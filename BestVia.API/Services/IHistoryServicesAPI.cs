using BestVia.Models;
using Microsoft.AspNetCore.Identity;
using BestVia.API.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace BestVia.API.Services
{
    public interface IHistoryServicesAPI
    {     //===================AUTH=====================================//
        Task<List<History>> ListAsync();
        Task<History> GetIdAsync(int id);
        Task<List<History>> GetListUserNameAsync(string username);
        Task<List<History>> GetListUserIdAsync(string userid);
        Task<bool> AddAsync(History history);
        Task<bool> DeleteAsync(SearchModel searchModel);

    }

    public class HistoryServicesAPI : IHistoryServicesAPI
    {
        private readonly HttpClient _http;
        private AppDbContext _appDb;
        public HistoryServicesAPI(HttpClient http,
            AppDbContext appDb
           )
        {
            _http = http;
            _appDb = appDb;

        }

        public async Task<List<History>> ListAsync()
        {
            try
            {
                return await _appDb.HistoryDb.Take(500)
                    .OrderByDescending(c => c.Id).ToListAsync();
            }
            catch (Exception)
            {
                return new List<History>();
            }
        }

        public async Task<History> GetIdAsync(int id)
        {
            try
            {
                return await _appDb.HistoryDb
                    .FirstAsync(c => c.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<History>> GetListUserNameAsync(string username)
        {
            try
            {
                return await _appDb.HistoryDb
                    .Where(c => c.Username == username).Take(500)
                    .OrderByDescending(c => c.Id).ToListAsync();
            }
            catch (Exception)
            {
                return new();
            }
        }

        public async Task<List<History>> GetListUserIdAsync(string userid)
        {
            try
            {
                return await _appDb.HistoryDb
                    .Where(c => c.Userid == userid).Take(500)
                    .OrderByDescending(c => c.Id).ToListAsync();
            }
            catch (Exception)
            {
                return new();
            }
        }



        public async Task<bool> AddAsync(History history)
        {
            try
            {
                await _appDb.HistoryDb.AddAsync(history);
                await _appDb.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> DeleteAsync(SearchModel searchModel)
        {
            try
            {
                await _appDb.HistoryDb
                  .Where(c => c.DateCreated >= searchModel.from && c.DateCreated <= searchModel.to)
                  .DeleteFromQueryAsync();

                await _appDb.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}
