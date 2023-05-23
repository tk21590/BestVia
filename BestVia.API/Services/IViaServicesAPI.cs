using AutoMapper;
using BestVia.API.Data;
using Microsoft.EntityFrameworkCore;
using BestVia.Models;

namespace BestVia.API.Services
{
    public interface IViaServicesAPI
    {
        Task<ApiRespone> ListAsync();
        Task<ApiRespone> ListOrderId(int orderId);
        Task<ApiRespone> GetIdAsync(int id);
        Task<ApiRespone> GetNameAsync(string name);
        Task<ApiRespone> EditAsync(Via Via);
        Task<ApiRespone> AddAsync(Via Via);
        Task<ApiRespone> AddRangeAsync(Via[] Via);
    }
    public class ViaServicesAPI : IViaServicesAPI
    {
        private readonly HttpClient _http;
        private AppDbContext _appDb;
        private readonly IMapper _mapper;
        public ViaServicesAPI(HttpClient http,
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
                var list_Via = await _appDb.ViaDb
                                  .OrderByDescending(c => c.Id)
                                  .ToListAsync();
                if (list_Via == null || list_Via.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_Via);

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
                var Via = await _appDb.ViaDb.FirstOrDefaultAsync(c => c.Id == id);
                if (Via == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, Via);
            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> GetNameAsync(string name)
        {
            try
            {
                var Via = await _appDb.ViaDb.FirstOrDefaultAsync(c => c.Username == name);
                if (Via == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, Via);
            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> EditAsync(Via Via)
        {
            var Via_finded = await _appDb.ViaDb.FirstOrDefaultAsync(c => c.Id == Via.Id);

            if (Via_finded == null)
            {
                return Core.CreateRespone(respone_code.notfound);
            }

            _mapper.Map(Via, Via_finded);

            try
            {
                var entity = _appDb.ViaDb.Update(Via_finded);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, entity.Entity);
            }
            catch (DbUpdateException ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> AddAsync(Via Via)
        {
            try
            {
                var add = await _appDb.ViaDb.AddAsync(Via);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, add.Entity, "Thêm via thành công");
            }
            catch (Exception ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> AddRangeAsync(Via[] Via)
        {
            try
            {
                if (Via == null || Via.Length == 0)
                {
                    return Core.CreateRespone(respone_code.failed, null, "List Empty");
                }
                await _appDb.ViaDb.BulkInsertAsync(Via);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, null, $"Đã thêm {Via.Length} via thành công");
            }
            catch (Exception ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> ListOrderId(int orderId)
        {
            try
            {
                var list_Via = await _appDb.ViaDb
                    .Where(c => c.OrderId == orderId)
                     .ToListAsync();
                if (list_Via == null || list_Via.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_Via);

            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }
    }
}
