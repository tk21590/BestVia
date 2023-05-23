using AutoMapper;
using BestVia.API.Data;
using Microsoft.EntityFrameworkCore;
using BestVia.Models;
using Z.EntityFramework.Plus;

namespace BestVia.API.Services
{
    public interface IOrderServicesAPI
    {
        Task<ApiRespone> ListAsync();
        Task<ApiRespone> ListIdAsync(string userId);
        Task<ApiRespone> ListRefAsync(SearchModel searchModel);
        Task<ApiRespone> SearchListAsync(SearchModel searchModel);
        Task<ApiRespone> GetIdAsync(int id);
        Task<ApiRespone> GetNameAsync(string name);
        Task<ApiRespone> EditAsync(Order order);
        Task<ApiRespone> AddAsync(Order order);
        Task<ApiRespone> ComplainAsync(Complain complain);
    }
    public class OrderServicesAPI : IOrderServicesAPI
    {
        private readonly HttpClient _http;
        private AppDbContext _appDb;
        private readonly IMapper _mapper;
        private readonly IHistoryServicesAPI _historyServices;
        private readonly IUserServicesAPI _userServices;
        public OrderServicesAPI(HttpClient http,
            AppDbContext appDb,
            IMapper mapper,
            IHistoryServicesAPI historyServices,
            IUserServicesAPI userServices)
        {
            _http = http;
            _appDb = appDb;
            _mapper = mapper;
            _historyServices = historyServices;
            _userServices = userServices;
        }

        public async Task<ApiRespone> ListAsync()
        {
            try
            {
                var list_order = await _appDb.OrderDb
                                  .OrderByDescending(c => c.Id)
                                  .Take(100)
                                  .Include(c => c.Status)
                                  .Include(c => c.Product).ThenInclude(c => c.ProductType).ThenInclude(c => c.Category)
                                  .ToListAsync();
                if (list_order == null || list_order.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_order);

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
                var order = await _appDb.OrderDb
                    .Include(c => c.Product).ThenInclude(c => c.ProductType).ThenInclude(c => c.Category)
                    .Include(c => c.Status)
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (order == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, order);
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
                var order = await _appDb.OrderDb
                    .Include(c => c.Product).ThenInclude(c => c.ProductType).ThenInclude(c => c.Category)
                    .Include(c => c.Status)
                    .FirstOrDefaultAsync(c => c.User == name);
                if (order == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }
                return Core.CreateRespone(respone_code.success, order);
            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> EditAsync(Order order)
        {
            var order_finded = await _appDb.OrderDb.FirstOrDefaultAsync(c => c.Id == order.Id);

            if (order_finded == null)
            {
                return Core.CreateRespone(respone_code.notfound);
            }

            if (order.StatusId == 1) //Nếu chuyển đổi đơn hàng về Hoàn thành
            {
                order.isComplain = false;
                order.Reason = string.Empty;
            }
            order.Status = null;
            order.Product = null;

            _mapper.Map(order, order_finded);

            try
            {
                var entity = _appDb.OrderDb.Update(order_finded);
                await _appDb.SaveChangesAsync();
                return Core.CreateRespone(respone_code.success, entity.Entity);
            }
            catch (DbUpdateException ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> AddAsync(Order order)
        {
            try
            {

                var product = await _appDb.ProductDb
                    .Include(c => c.ProductType).ThenInclude(c => c.Category)
                    .FirstOrDefaultAsync(c => c.Id == order.ProductId);
                if (product == null)
                {
                    return Core.CreateRespone(respone_code.notfound, null, "Sản phẩm không có!");

                }

                if (product.Stock < order.Quantity)
                {
                    return Core.CreateRespone(respone_code.failed, null, "Sản phẩm không đủ hoặc đã hết!");

                }
                bool isBox = false;

                if (product.ProductType.CategoryId == 4)//Là bán box
                {
                    isBox = true;
                    ////Kiểm tra user có phải người dùng không
                    //var user = await _userServices.CheckRole(order.UserOrderId);
                    //if (user.Contains("Admin")
                    //    || user.Contains("SuperMod")
                    //    || user.Contains("Mod"))
                    //{
                    //    isBox = true;

                    //}
                    //else
                    //{
                    //    return Core.CreateRespone(respone_code.failed, null, $"Bạn không có quyền để yêu cầu sản phẩm này!");

                    //}
                }


                product.Stock -= order.Quantity; //Số lượng còn lại giảm xuống
                product.Sold += order.Quantity; //Số lượng bán tăng lên
                                                //Kiểm tra user còn đủ tiền không



                var updated_product = _appDb.ProductDb.Update(product);
                await _appDb.SaveChangesAsync();

                if (isBox)
                {

                    order.TotalPrice = (product.Price * order.Quantity) / 10;//Tổng tiền đơn hàng đặt cọc
                    order.StatusId = 5;//Chờ xử lý
                }
                else
                {
                    order.TotalPrice = product.Price * order.Quantity;//Tổng tiền đơn hàng
                    order.StatusId = 1;//Hoàn thành

                }

                if (!await _userServices.CheckBalance(order.UserOrderId, order.TotalPrice))
                {
                    //Trả lại trạng thái đơn hàng cũ
                    product.Stock += order.Quantity;
                    product.Sold -= order.Quantity;
                    _appDb.ProductDb.Update(product);
                    await _appDb.SaveChangesAsync();

                    if (isBox)
                    {
                        return Core.CreateRespone(respone_code.failed, null, $"Không đủ tiền đặt cọc!");
                    }
                    else
                    {
                        return Core.CreateRespone(respone_code.failed, null, $"Số dư không đủ !");

                    }
                }
                order.DateOrder = Core.Date_GetTimeNowInt();

                ////Kiểm tra user có phải người dùng không
                var user = await _userServices.CheckRole(order.UserOrderId);
                bool isMod = false;
                if (user.Contains("Admin")
                    || user.Contains("SuperMod")
                    || user.Contains("Mod"))
                {
                    order.TotalPriceIncomeMod = product.PriceIncomeMod * order.Quantity;


                }
                else
                {
                    order.TotalPriceIncome = product.PriceIncome * order.Quantity;

                }

                var add = await _appDb.OrderDb.AddAsync(order);
                await _appDb.SaveChangesAsync();

                //Giảm số dư
                string content = await _userServices.Balance(add.Entity.UserOrderId, -add.Entity.TotalPrice);
                await _historyServices.AddAsync(new History
                {
                    Userid = add.Entity.UserOrderId,
                    HistoryTypeId = 5,
                    DateCreated = add.Entity.DateOrder,
                    Value = add.Entity.TotalPrice,
                    Header = $"Đặt hàng {add.Entity.Product.Name} - Số lượng :{add.Entity.Quantity}",
                    Content = content,
                    Username = add.Entity.UserOrderName,
                    OrderId = add.Entity.Id,
                });

                if (product.ProductType.CategoryId == 2) //TOOL
                {
                    var add_key = await _appDb.KeyDb.AddAsync(new Key
                    {
                        KeyName = add.Entity.Product.Name,
                        KeyId = Guid.NewGuid().ToString(),
                        DateCreated = Core.Date_GetTimeNowInt(),
                        DateExpired = Core.Date_DateTimeToUnitUnixTimestamp(DateTime.UtcNow.AddHours(7).AddDays(product.Guarantee)),
                        OrderId = add.Entity.Id,
                        ProductId = product.Id,
                        UserId = add.Entity.UserOrderId,
                        UserName = add.Entity.UserOrderName,
                        IsAvaiable = true,

                    });
                    await _appDb.SaveChangesAsync();
                    return Core.CreateRespone(respone_code.success, add.Entity, $"Đặt hàng thành công .Key của bạn là {add_key.Entity.KeyId} !");

                }


                //Đặt hàng thành công >> khi đó lấy ra danh sách VIA nếu là VIA
                if (product.ProductType.CategoryId == 3)//VIA
                {
                    //Lấy ra danh sách via chưa bán từ cũ tới thấp và có id =  productId
                    var query_sold = await _appDb.ViaDb.Where(c => !c.IsSold && c.ProductId == product.Id)
                              .OrderBy(c => c.Id)
                              .Take(order.Quantity)
                              .ToListAsync();

                    query_sold.ForEach(c =>
                    {
                        c.IsSold = true;
                        c.Userid = order.UserOrderId;
                        c.Username = order.UserOrderName;
                        c.OrderId = add.Entity.Id;
                    });

                    await _appDb.ViaDb.BulkUpdateAsync(query_sold);
                    await _appDb.SaveChangesAsync();
                    return Core.CreateRespone(respone_code.success, add.Entity, $"Đặt hàng thành công.\r\n{query_sold.Count} account via đã được gửi đến tài khoản của bạn !");

                }

                if (product.ProductType.CategoryId == 1)//PROXY
                {
                    //Lấy ra danh sách via chưa bán từ cũ tới thấp và có id =  productId
                    var query_sold = await _appDb.ProxyDb.Where(c => !c.IsSold && c.ProductId == product.Id)
                              .OrderBy(c => c.Id)
                              .Take(product.Combo * order.Quantity)
                              .ToListAsync();

                    query_sold.ForEach(c =>
                    {
                        c.IsSold = true;
                        c.Userid = order.UserOrderId;
                        c.Username = order.UserOrderName;
                        c.OrderId = add.Entity.Id;

                    });

                    await _appDb.ProxyDb.BulkUpdateAsync(query_sold);
                    await _appDb.SaveChangesAsync();
                    return Core.CreateRespone(respone_code.success, add.Entity, $"Đặt hàng thành công.\r\n{query_sold.Count} Proxy đã được gửi đến tài khoản của bạn !");

                }



                return Core.CreateRespone(respone_code.success, add.Entity, $"Đặt hàng thành công !");
            }
            catch (Exception ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> ComplainAsync(Complain complain)
        {
            try
            {



                var order = await _appDb.OrderDb
                    .Include(c => c.Product).ThenInclude(c => c.ProductType)
                    .FirstOrDefaultAsync(c => c.Id == complain.orderid);
                if (order == null)
                {
                    return Core.CreateRespone(respone_code.notfound, null, "Đơn hàng không có!");

                }
                if (order.UserOrderId != complain.userid)
                {
                    return Core.CreateRespone(respone_code.error, null, "Chỉ có chủ sở hữu đơn hàng mới được quyền khiếu nại");

                }
                int check_day_complain = 2;
                if (order.Product.ProductType.CategoryId == 4)//Là box
                {

                    check_day_complain = 7;
                }

                if (Core.Date_CheckTime(order.DateOrder, check_day_complain))
                {
                    return Core.CreateRespone(respone_code.error, null, "Đơn hàng đã hết thời gian khiếu nại");

                }
                order.StatusId = 4;
                order.isComplain = true;
                order.Reason = complain.reason;
                _appDb.OrderDb.Update(order);
                await _appDb.SaveChangesAsync();
                await _historyServices.AddAsync(new History
                {
                    Userid = order.UserOrderId,
                    HistoryTypeId = 5,
                    DateCreated = order.DateOrder,
                    Value = order.TotalPrice,
                    Header = $"Khiếu nại về đơn hàng {order.Id}",
                    Content = complain.reason,
                    Username = order.UserOrderName,
                    OrderId = order.Id,
                });
                return Core.CreateRespone(respone_code.success, null, $"Khiếu nại đơn hàng {order.Id} thành công!\r\nVui lòng chờ admin phản hồi và xử lý đơn hàng của bạn!");

            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }

        public async Task<ApiRespone> ListRefAsync(SearchModel searchModel)
        {
            try
            {
                List<string> list_user_id = await _appDb.Users.Where(c => c.RefAdd == searchModel.ref_add).Select(c => c.Id).ToListAsync();
                if (list_user_id.Count < 1)
                {
                    return Core.CreateRespone(respone_code.notfound, null, "Không có khách hàng nào!");

                }

                var list_order = await _appDb.OrderDb
                                  .Where(c => list_user_id.Contains(c.UserOrderId) && c.StatusId == 1
                                  && c.DateOrder >= searchModel.from
                                  && c.DateOrder <= searchModel.to)
                                  .Include(c => c.Status)
                                  .Include(c => c.Product)
                                  .OrderByDescending(c => c.Id)
                                  .ToListAsync();
                if (list_order == null || list_order.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound, null, "Không có đơn hàng nào!");

                }
                return Core.CreateRespone(respone_code.success, list_order);

            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> ListIdAsync(string userId)
        {
            try
            {
                var list_order = await _appDb.OrderDb
                                .Where(c => c.UserOrderId == userId)
                                  .OrderByDescending(c => c.Id)
                                  .Take(100)
                                  .Include(c => c.Status)
                                  .Include(c => c.Product).ThenInclude(c => c.ProductType).ThenInclude(c => c.Category)
                                  .ToListAsync();
                if (list_order == null || list_order.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_order);

            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }

        public async Task<ApiRespone> SearchListAsync(SearchModel search)
        {

            try
            {
                var list_order = await _appDb.OrderDb
                              .Where(c => c.DateOrder >= search.from && c.DateOrder <= search.to &&
                                  (string.IsNullOrEmpty(search.userorderid) || c.UserOrderId == search.userorderid))
                                  .OrderByDescending(c => c.Id)
                                  .Include(c => c.Status)
                                  .Include(c => c.Product).ThenInclude(c => c.ProductType).ThenInclude(c => c.Category)
                                  .ToListAsync();

                if (list_order == null || list_order.Count == 0)
                {
                    return Core.CreateRespone(respone_code.notfound);

                }
                return Core.CreateRespone(respone_code.success, list_order);

            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, ex, ex.Message);
            }
        }
    }
}
