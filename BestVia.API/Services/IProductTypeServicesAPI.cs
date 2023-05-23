using AutoMapper;
using BestVia.API.Data;
using Microsoft.EntityFrameworkCore;
using BestVia.Models;

namespace BestVia.API.Services
{
    public interface IProductTypeServicesAPI
    {
        Task<ApiRespone> ListAsync();
        Task<ApiRespone> SearchListAsync(string name);
        Task<ApiRespone> GetIdAsync(int id);
        Task<ApiRespone> GetNameAsync(string name);
        Task<ApiRespone> EditAsync(ProductType ptype);
        Task<ApiRespone> AddAsync(ProductType ptype);
    }
    public class ProductTypeServicesAPI : IProductTypeServicesAPI
    {
        private readonly HttpClient _http;
        private AppDbContext _appDb;
        private readonly IMapper _mapper;
        public ProductTypeServicesAPI(HttpClient http,
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
                var list_ptype = await _appDb.ProductTypeDb
                                  .OrderByDescending(c => c.Id)
                                  .Take(100)
                                  .Include(c => c.Category)
                                  .ToListAsync();
                if (list_ptype == null || list_ptype.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_ptype);

            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> SearchListAsync(string name)
        {
            try
            {
                var list_ptype = await _appDb.ProductTypeDb
                .OrderByDescending(c => c.Id)
                                  .Where(c => c.Name.Contains(name))
                                  .Take(100)
                                   .Include(c => c.Category)
                                  .ToListAsync();
                if (list_ptype == null || list_ptype.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_ptype);
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
                var ptype = await _appDb.ProductTypeDb
                    .Include(c=>c.Category)
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (ptype == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, ptype);
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
                var ptype = await _appDb.ProductTypeDb
                    .Include(c => c.Category)
                    .FirstOrDefaultAsync(c => c.Name == name);
                if (ptype == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, ptype);
            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> EditAsync(ProductType ptype)
        {
            var ptype_finded = await _appDb.ProductTypeDb
                .FirstOrDefaultAsync(c => c.Id == ptype.Id);

            if (ptype_finded == null)
            {
                return Core.CreateRespone(respone_code.notfound);
            }
            ptype.Category = null;
            _mapper.Map(ptype, ptype_finded);

            try
            {
                var entity = _appDb.ProductTypeDb.Update(ptype_finded);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, entity.Entity);
            }
            catch (DbUpdateException ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> AddAsync(ProductType ptype)
        {
            try
            {
                var add = await _appDb.ProductTypeDb.AddAsync(ptype);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, add.Entity,$"Thêm nhóm {add.Entity.Name} thành công!");
            }
            catch (Exception ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }
    }
}
