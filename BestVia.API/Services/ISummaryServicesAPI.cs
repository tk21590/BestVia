using AutoMapper;
using BestVia.API.Data;
using Microsoft.EntityFrameworkCore;
using BestVia.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace BestVia.API.Services
{
    public interface ISummaryServicesAPI
    {
        Task<List<Order>> SearchOrders(SearchModel search);
        Task<List<History>> SearchHistoryOrder(SearchModel search);
        Task<List<History>> SearchHistoryAll(SearchModel search);
        Task<List<History>> SearchHistoryLast();
        Task<UserInfo> SearchUserInfo(string userId, bool is_ref);
    }
    public class SummaryServicesAPI : ISummaryServicesAPI
    {
        private readonly HttpClient _http;
        private AppDbContext _appDb;
        private UserManager<IUsers> _userManager;
        public SummaryServicesAPI(HttpClient http,
            AppDbContext appDb, UserManager<IUsers> userManager
           )
        {
            _http = http;
            _appDb = appDb;
            _userManager = userManager;

        }

        public async Task<List<Order>> SearchOrders(SearchModel search)
        {
            //Tìm kiếm nằm giữa khoảng thời gian cho phép
            try
            {
                return await _appDb.OrderDb
                               .Where(c => c.DateOrder >= search.from && c.DateOrder <= search.to &&
                                   (string.IsNullOrEmpty(search.userorderid) || c.UserOrderId == search.userorderid))
                                   .Include(c => c.Status)
                                   .Include(c => c.Product)
                                   .ToListAsync();


            }
            catch (Exception)
            {
            }
            return new List<Order>();

        }
        public async Task<List<History>> SearchHistoryAll(SearchModel search)
        {
            try
            {
                var query = await _appDb.HistoryDb
                                .Where(c => c.DateCreated >= search.from && c.DateCreated <= search.to &&
                                    (string.IsNullOrEmpty(search.userorderid) || c.Userid == search.userorderid))
                                    .Include(c => c.HistoryType).OrderByDescending(c => c.Id).ToListAsync();
                if (search.historyid > 0)
                {
                    query = query.Where(c => c.HistoryTypeId == search.historyid).OrderByDescending(c => c.Id).ToList();
                }
                return query;
            }
            catch (Exception ex)
            {
                return new List<History>();
            }
        }
        public async Task<List<History>> SearchHistoryOrder(SearchModel search)
        {
            try
            {
                var query = await _appDb.HistoryDb
                                .Where(c => c.DateCreated >= search.from && c.DateCreated <= search.to && c.HistoryTypeId == 5 &&
                                    (string.IsNullOrEmpty(search.userorderid) || c.Userid == search.userorderid))
                                    .Include(c => c.HistoryType).OrderByDescending(c => c.Id).ToListAsync();
                if (search.historyid > 0)
                {
                    query = query.Where(c => c.HistoryTypeId == search.historyid).OrderByDescending(c => c.Id).ToList();
                }
                return query;
            }
            catch (Exception ex)
            {
                return new List<History>();
            }
        }

        public async Task<UserInfo> SearchUserInfo(string userId, bool is_ref)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var userInfo = new UserInfo
                    {
                        apikey = user.ApiKey,
                        balance = user.Balance,
                        balance_used = user.Balance_Used,
                        balance_total = user.Balance_Total,
                        block = user.Block,
                        refcode = user.RefCode,
                        username = user.UserName,
                    };
                    if (!is_ref)
                    {
                        return userInfo;
                    }
                    else
                    {
                        List<string> list_user_id = await _appDb.Users.Where(c => c.RefAdd == user.RefCode).Select(c => c.Id).ToListAsync();

                        if (list_user_id.Count < 1)
                        {
                            return userInfo;
                        }
                        userInfo.total_ref = list_user_id.Count;

                        var list_order = await _appDb.OrderDb
                                          .Where(c => list_user_id.Contains(c.UserOrderId) && c.StatusId == 1)
                                          .ToListAsync();
                        if (list_order == null || list_order.Count == 0)
                        {
                            return userInfo;


                        }
                        userInfo.total_income = list_order.Sum(c => c.TotalPriceIncome);

                        return userInfo;
                    }

                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<History>> SearchHistoryLast()
        {
            try
            {
                var query = await _appDb.HistoryDb
                                    .Where(c => c.HistoryTypeId == 5) //Là đơn đặt hàng thành công
                                    .Include(c => c.HistoryType)
                                    .OrderByDescending(c => c.Id)
                                    .Take(7)
                                    .ToListAsync();
                //Giấu toàn bộ thông tin
                query.ForEach(c => c.Username = Core.ReplaceMiddleChars(c.Username));
                query.ForEach(c => c.Userid = string.Empty);
                query.ForEach(c => c.OrderId = 0);
                return query;
            }
            catch (Exception ex)
            {
                return new List<History>();
            }
        }
    }
}
