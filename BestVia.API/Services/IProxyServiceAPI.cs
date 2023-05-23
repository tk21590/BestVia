using AutoMapper;
using BestVia.API.Data;
using Microsoft.EntityFrameworkCore;
using BestVia.Models;

namespace BestVia.API.Services
{
    public interface IProxyServicesAPI
    {
        Task<ApiRespone> ListAsync();
        Task<ApiRespone> ListOrderId(int orderId);
        Task<ApiRespone> EditAsync(Proxy Proxy);
        Task<ApiRespone> AddAsync(Proxy Proxy);
        Task<ApiRespone> AddRangeAsync(Proxy[] Proxy);
    }
    public class ProxyServicesAPI : IProxyServicesAPI
    {
        private readonly HttpClient _http;
        private AppDbContext _appDb;
        private readonly IMapper _mapper;
        public ProxyServicesAPI(HttpClient http,
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
                var list_Proxy = await _appDb.ProxyDb
                                  .OrderByDescending(c => c.Id)
                                  .ToListAsync();
                if (list_Proxy == null || list_Proxy.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_Proxy);

            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> EditAsync(Proxy Proxy)
        {
            var Proxy_finded = await _appDb.ProxyDb.FirstOrDefaultAsync(c => c.Id == Proxy.Id);

            if (Proxy_finded == null)
            {
                return Core.CreateRespone(respone_code.notfound);
            }

            _mapper.Map(Proxy, Proxy_finded);

            try
            {
                var entity = _appDb.ProxyDb.Update(Proxy_finded);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, entity.Entity);
            }
            catch (DbUpdateException ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> AddAsync(Proxy Proxy)
        {
            try
            {
                var add = await _appDb.ProxyDb.AddAsync(Proxy);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, add.Entity, "Thêm Proxy thành công");
            }
            catch (Exception ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> AddRangeAsync(Proxy[] Proxy)
        {
            try
            {
                if (Proxy == null || Proxy.Length == 0)
                {
                    return Core.CreateRespone(respone_code.failed, null, "List Empty");
                }
                await _appDb.ProxyDb.BulkInsertAsync(Proxy);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, null, $"Đã thêm {Proxy.Length} Proxy thành công");
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
                var list_Proxy = await _appDb.ProxyDb
                    .Where(c => c.OrderId == orderId)
                     .ToListAsync();
                if (list_Proxy == null || list_Proxy.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_Proxy);

            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }
    }
}
