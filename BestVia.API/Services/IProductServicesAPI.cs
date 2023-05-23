using AutoMapper;
using BestVia.API.Data;
using Microsoft.EntityFrameworkCore;
using BestVia.Models;

namespace BestVia.API.Services
{
    public interface IProductServicesAPI
    {
        Task<ApiRespone> ListAsync();
        Task<ApiRespone> ListIdCateAsync(int idCate);
        Task<ApiRespone> SearchListAsync(string name);
        Task<ApiRespone> GetIdAsync(int id);
        Task<ApiRespone> GetNameAsync(string name);
        Task<ApiRespone> EditAsync(Product product);
        Task<ApiRespone> AddAsync(Product product);
    }
    public class ProductServicesAPI : IProductServicesAPI
    {
        private readonly HttpClient _http;
        private AppDbContext _appDb;
        private readonly IMapper _mapper;
        public ProductServicesAPI(HttpClient http,
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
                var list_product = await _appDb.ProductDb
                                  .OrderByDescending(c => c.Id)
                                  .Take(500)
                                  .Include(c => c.ProductType).ThenInclude(c => c.Category)
                                  .ToListAsync();
                if (list_product == null || list_product.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_product);

            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }
        public async Task<ApiRespone> ListIdCateAsync(int idCate)
        {
            try
            {
                var list_product = await _appDb.ProductDb
                                  .Where(c => c.ProductType.CategoryId == idCate)
                                  .Include(c => c.ProductType).ThenInclude(c => c.Category)
                                  .ToListAsync();
                if (list_product == null || list_product.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_product);

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
                var list_product = await _appDb.ProductDb
                .OrderByDescending(c => c.Id)
                                  .Where(c => c.Name.Contains(name))
                                  .Take(100)
                                  .ToListAsync();
                if (list_product == null || list_product.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_product);
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
                var product = await _appDb.ProductDb
                    .Include(c => c.ProductType).ThenInclude(c => c.Category)
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (product == null)

                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, product);
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
                var product = await _appDb.ProductDb.FirstOrDefaultAsync(c => c.Name == name);
                if (product == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, product);
            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> EditAsync(Product product)
        {
            var product_finded = await _appDb.ProductDb.FirstOrDefaultAsync(c => c.Id == product.Id);

            if (product_finded == null)
            {
                return Core.CreateRespone(respone_code.notfound);
            }


            bool hasVias = product.list_via != null && product.list_via.Count > 0;
            if (hasVias)
            {
                product.Stock += product.list_via.Count();
                product.Amount += product.list_via.Count();

            }

            bool hasProxy = product.list_proxy != null && product.list_proxy.Count > 0;
            if (hasProxy)
            {
                if (product.list_proxy.Count() % product.Combo != 0)
                {
                    return Core.CreateRespone(respone_code.failed, null, $"Số lượng proxy không cân bằng với combo {product.list_proxy.Count()}/{product.Combo}");

                }

                int increase = (product.list_proxy.Count() / product.Combo);
                if (increase == 0)
                {
                    return Core.CreateRespone(respone_code.failed, null, $"Số lượng proxy không đủ combo {product.list_proxy.Count()}/{product.Combo}");

                }

                product.Amount += increase;
                product.Stock += increase;
            }



            _mapper.Map(product, product_finded);

            try
            {

                product_finded.DateUpdated = Core.Date_GetTimeNowInt();//Ngày giờ update
                var update = _appDb.ProductDb.Update(product_finded);
                await _appDb.SaveChangesAsync();
                string message = "";

                if (hasVias)
                {
                    product.list_via.ForEach(c => c.ProductId = update.Entity.Id);
                    await _appDb.ViaDb.BulkInsertAsync(product.list_via);
                    await _appDb.SaveChangesAsync();
                    message += $"Upload {product.list_via.Count} Via thành công!";

                }
                if (hasProxy)
                {
                    product.list_proxy.ForEach(c => c.ProductId = update.Entity.Id);
                    await _appDb.ProxyDb.BulkInsertAsync(product.list_proxy);
                    await _appDb.SaveChangesAsync();
                    message += $"\r\nUpload {product.list_proxy.Count} Proxy thành công!";

                }

                return Core.CreateRespone(respone_code.success, update.Entity, "Update sản phẩm thành công." + message);
            }
            catch (DbUpdateException ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }
        public async Task<ApiRespone> AddAsync(Product product)
        {
            try
            {
                product.Stock = product.Amount;
                bool hasVias = product.list_via != null && product.list_via.Count > 0;
                if (hasVias)
                {
                    product.Stock = product.Amount = product.list_via.Count();
                }

                bool hasProxy = product.list_proxy != null && product.list_proxy.Count > 0;
                if (hasProxy)
                {
                    product.Stock = product.Amount = product.list_proxy.Count() / product.Combo;
                }

                product.Sold = 0;
                product.DateUpdated = product.DateCreated = Core.Date_GetTimeNowInt();

                var add = await _appDb.ProductDb.AddAsync(product);
                await _appDb.SaveChangesAsync();
                string message = "";
                if (hasVias)
                {
                    product.list_via.ForEach(c => c.ProductId = add.Entity.Id);
                    await _appDb.ViaDb.BulkInsertAsync(product.list_via);
                    await _appDb.SaveChangesAsync();
                    message += $"Upload {product.list_via.Count} Via thành công!";

                }
                if (hasProxy)
                {
                    product.list_proxy.ForEach(c => c.ProductId = add.Entity.Id);
                    await _appDb.ProxyDb.BulkInsertAsync(product.list_proxy);
                    await _appDb.SaveChangesAsync();
                    message += $"Upload {product.list_proxy.Count} Proxy thành công!";

                }

                return Core.CreateRespone(respone_code.success, add.Entity, $"Thêm [{product.Name}] thành công." + message);
            }
            catch (Exception ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }
    }
}
