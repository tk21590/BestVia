using AutoMapper;
using BestVia.API.Data;
using Microsoft.EntityFrameworkCore;
using BestVia.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Key = BestVia.Models.Key;

namespace BestVia.API.Services
{
    public interface IKeyServicesAPI
    {
        Task<ApiRespone> ListAsync();
        Task<ApiRespone> GetIdAsync(int id);
        Task<ApiRespone> GetNameAsync(string keyid);
        Task<ApiRespone> EditAsync(Key key);
        Task<ApiRespone> AddAsync(Key key);
        Task<ApiRespone> DeleteAsync(int keyid);
    }
    public class KeyServicesAPI : IKeyServicesAPI
    {
        private readonly HttpClient _http;
        private AppDbContext _appDb;
        private readonly IMapper _mapper;
        public KeyServicesAPI(HttpClient http,
            AppDbContext appDb,
            IMapper mapper)
        {
            _http = http;
            _appDb = appDb;
            _mapper = mapper;

        }

        public async Task<ApiRespone> ListAsync()
        {
            try
            {
                var list_key = await _appDb.KeyDb
                                  .OrderByDescending(c => c.Id)
                                  .Take(100)
                                  .Include(c => c.Order)
                                  .ToListAsync();
                if (list_key == null || list_key.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_key);

            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

     

        public async Task<ApiRespone> GetIdAsync(int id)
        {
            try
            {
                var key = await _appDb.KeyDb
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (key == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, key);
            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> GetNameAsync(string keyid)
        {
            try
            {
                var key = await _appDb.KeyDb
                    .Include(c => c.Order)
                    .FirstOrDefaultAsync(c => c.KeyId == keyid);
                if (key == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, key);
            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> EditAsync(Key key)
        {
            var key_finded = await _appDb.KeyDb
                .FirstOrDefaultAsync(c => c.Id == key.Id);

            if (key_finded == null)
            {
                return Core.CreateRespone(respone_code.notfound);
            }

            _mapper.Map(key, key_finded);

            try
            {
                var entity = _appDb.KeyDb.Update(key_finded);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, entity.Entity, $"Update key [{key.KeyId}] thành công!");
            }
            catch (DbUpdateException ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> AddAsync(Key key)
        {
            try
            {
                if (key.OrderId==0)
                {
                    key.OrderId = 1;
                }
                if (key.ProductId == 0)
                {
                    key.ProductId = 1;
                }
                var add = await _appDb.KeyDb.AddAsync(key);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, add.Entity,"Thêm key thành công!");
            }
            catch (Exception ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> DeleteAsync(int keyid)
        {
            try
            {
                var key = await _appDb.KeyDb.FirstOrDefaultAsync(c=>c.Id==keyid);
                if (key==null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                _appDb.KeyDb.Remove(key);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success,null, $"Xóa key [{key.KeyId}] thành công!");
            }
            catch (Exception ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }
    }
}
