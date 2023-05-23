using AutoMapper;
using BestVia.API.Data;
using Microsoft.EntityFrameworkCore;
using BestVia.Models;
using Z.EntityFramework.Plus;

namespace BestVia.API.Services
{
    public interface IDescriptionServicesAPI
    {
        Task<ApiRespone> ListAsync();
        Task<ApiRespone> GetIdAsync(int id);
        Task<ApiRespone> GetHeaderAsync(string header);
        Task<ApiRespone> EditAsync(Description description);
        Task<ApiRespone> AddAsync(Description description);
    }
    public class DescriptionServicesAPI : IDescriptionServicesAPI
    {
        private readonly HttpClient _http;
        private AppDbContext _appDb;
        private IMapper _mapper;
        public DescriptionServicesAPI(HttpClient http,
            AppDbContext appDb, IMapper mapper
            )
        {
            _http = http;
            _appDb = appDb;
            _mapper = mapper;
        }

        public async Task<ApiRespone> ListAsync()
        {
            try
            {
                var list_Description = await _appDb.DescriptionDb
                                  .OrderByDescending(c => c.Id)
                                  .Take(100)
                                  .ToListAsync();
                if (list_Description == null || list_Description.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_Description);

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
                var Description = await _appDb.DescriptionDb
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (Description == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, Description);
            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> GetHeaderAsync(string header)
        {
            try
            {
                var description = await _appDb.DescriptionDb
                    .FirstOrDefaultAsync(c => c.Header == header);
                if (description == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, description);
            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> EditAsync(Description Description)
        {
            var Description_finded = await _appDb.DescriptionDb
                .FirstOrDefaultAsync(c => c.Id == Description.Id);

            if (Description_finded == null)
            {
                return Core.CreateRespone(respone_code.notfound);
            }

            _mapper.Map(Description, Description_finded);

            try
            {
                var entity = _appDb.DescriptionDb.Update(Description_finded);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, entity.Entity);
            }
            catch (DbUpdateException ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> AddAsync(Description description)
        {
            try
            {
                var checkHeader = await _appDb.DescriptionDb.FirstOrDefaultAsync(c => c.Header == description.Header);
                if (checkHeader != null)
                {
                    checkHeader.Header = description.Header;
                    checkHeader.Content = description.Content;

                    var add = _appDb.DescriptionDb.Update(checkHeader);
                    await _appDb.SaveChangesAsync();
                    return Core.CreateRespone(respone_code.success, add.Entity, "Cài đặt thành công!");

                }
                else
                {
                    var add = await _appDb.DescriptionDb.AddAsync(description);
                    await _appDb.SaveChangesAsync();
                    return Core.CreateRespone(respone_code.success, add.Entity, "Tạo thành công!");

                }

            }
            catch (Exception ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }
    }
}
