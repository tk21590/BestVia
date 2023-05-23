using AutoMapper;
using BestVia.API.Data;
using Microsoft.EntityFrameworkCore;
using BestVia.Models;

namespace BestVia.API.Services
{
    public interface ICategoryServicesAPI
    {
        Task<ApiRespone> ListAsync();
        Task<ApiRespone> SearchListAsync(string name);
        Task<ApiRespone> GetIdAsync(int id);
        Task<ApiRespone> GetNameAsync(string name);
        Task<ApiRespone> EditAsync(Category cate);
        Task<ApiRespone> AddAsync(Category cate);
    }
    public class CategoryServicesAPI : ICategoryServicesAPI
    {
        private readonly HttpClient _http;
        private AppDbContext _appDb;
        private readonly IMapper _mapper;
        public CategoryServicesAPI(HttpClient http,
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
                var list_cate = await _appDb.CategoryDb
                                  .OrderByDescending(c => c.Id)
                                  .Take(100)
                                  .ToListAsync();
                if (list_cate == null || list_cate.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_cate);

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
                var list_cate = await _appDb.CategoryDb
                .OrderByDescending(c => c.Id)
                                  .Where(c => c.Name.Contains(name))
                                  .Take(100)
                                  .ToListAsync();
                if (list_cate == null || list_cate.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_cate);
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
                var cate = await _appDb.CategoryDb.FirstOrDefaultAsync(c => c.Id == id);
                if (cate == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, cate);
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
                var cate = await _appDb.CategoryDb.FirstOrDefaultAsync(c => c.Name == name);
                if (cate == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, cate);
            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> EditAsync(Category cate)
        {
            var cate_finded = await _appDb.CategoryDb.FirstOrDefaultAsync(c => c.Id == cate.Id);

            if (cate_finded == null)
            {
                return Core.CreateRespone(respone_code.notfound);
            }

            _mapper.Map(cate, cate_finded);

            try
            {
                var entity = _appDb.CategoryDb.Update(cate_finded);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, entity.Entity);
            }
            catch (DbUpdateException ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> AddAsync(Category cate)
        {
            try
            {
                var add = await _appDb.CategoryDb.AddAsync(cate);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, add.Entity,"Thêm phân loại thành công!");
            }
            catch (Exception ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }
    }
}
