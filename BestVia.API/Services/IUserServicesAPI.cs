using AutoMapper;
using BestVia.Models;
using BestVia.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.WebSockets;

namespace BestVia.API.Services
{
    public interface IUserServicesAPI
    {
        Task<ApiRespone> ListAsync();
        Task<ApiRespone> SearchListAsync(string name);
        Task<ApiRespone> GetIdAsync(string userid);
        Task<ApiRespone> GetNameAsync(string username);
        Task<ApiRespone> EditAsync(UserView user);
        Task<string> Balance(string userId, long balance_used);
        Task<bool> CheckBalance(string userId, long balance_used);
        Task<string> CheckRole(string userId);

    }

    public class UserServicesAPI : IUserServicesAPI
    {
        private UserManager<IUsers> _userManager;
        private readonly HttpClient _http;
        private AppDbContext _appDb;
        private readonly IMapper _mapper;
        private readonly IHistoryServicesAPI _historyServices;

        public UserServicesAPI(UserManager<IUsers> userManager,
            HttpClient http, AppDbContext appDb, IMapper mapper, IHistoryServicesAPI historyServices)
        {
            _userManager = userManager;
            _http = http;
            _appDb = appDb;
            _mapper = mapper;
            _historyServices = historyServices;
        }

        public async Task<ApiRespone> ListAsync()
        {

            var list_user = await _appDb.Users
                           .OrderByDescending(c => c.DateLastLogin)
                           .OrderBy(c => c.SubId)
                           .Take(200)
                           .ToListAsync();


            if (list_user == null || list_user.Count == 0)
            {
                return Core.CreateRespone(respone_code.notfound);

            }
            List<UserView> userViews = new List<UserView>();
            foreach (var user in list_user)
            {
                var newUser = new UserView();
                _mapper.Map(user, newUser);
                userViews.Add(newUser);
            }
            return Core.CreateRespone(respone_code.success, userViews);

        }

        public async Task<ApiRespone> SearchListAsync(string name)
        {

            var list_user = await _appDb.Users
                           .OrderBy(c => c.SubId)
                           .Where(c => c.UserName.Contains(name))
                           .Take(10)
                           .ToListAsync();
            if (list_user == null)
            {
                return Core.CreateRespone(respone_code.notfound);

            }
            return Core.CreateRespone(respone_code.success, list_user);
        }

        public async Task<ApiRespone> GetIdAsync(string userid)
        {

            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return Core.CreateRespone(respone_code.notfound);
            }
            return Core.CreateRespone(respone_code.success, user);

        }

        public async Task<ApiRespone> GetNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Core.CreateRespone(respone_code.notfound);
            }
            return Core.CreateRespone(respone_code.success, user);
        }

        public async Task<ApiRespone> EditAsync(UserView iusers)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(iusers.Id);
                if (user == null)
                {
                    return Core.CreateRespone(respone_code.notfound);
                }



                string message = $"Update user {user.UserName} thành công!";
                if (iusers.Balance != user.Balance)//Có sự thay đổi về số dư
                {
                    var balance = iusers.Balance - user.Balance;

                    string content = string.Empty;
                    int id = 0;
                    if (balance > 0)
                    {
                        iusers.Balance_Total += balance;
                        content = $"Tăng số dư +{balance}";
                        id = 1;
                    }
                    else
                    {
                        iusers.Balance_Used += balance;
                        content = $"Giảm số dư -{balance}";
                        id = 2;
                    }
                    await _historyServices.AddAsync(new History
                    {
                        Userid = user.Id,
                        HistoryTypeId = id,//Admin tác động
                        DateCreated = Core.Date_GetTimeNowInt(),
                        Value = balance,
                        Header = $"Admin tác động tới số dư",
                        Content = content,
                        Username = user.UserName,
                    });
                    message += "\r\n" + content;

                }

                if (user.RoleName != iusers.RoleName)
                {
                    await _userManager.RemoveFromRoleAsync(user, user.RoleName);
                    await _userManager.AddToRoleAsync(user, iusers.RoleName);
                    message += $"\r\nQuyền của user đã thay đổi từ {user.RoleName} đến {iusers.RoleName}";
                }

                _mapper.Map(iusers, user);


                await _userManager.UpdateAsync(user);

                await _appDb.SaveChangesAsync();

                return Core.CreateRespone(respone_code.success, user, message);
            }
            catch (DbUpdateException ex)
            {
                return Core.CreateRespone(respone_code.failed, null, ex.Message);
            }
        }
 
        public async Task<string> Balance(string userId, long balance_used)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return $"không tìm thấy user {userId}";
                }
                string message = "";
                int idHistory = 0;

                var balance = Math.Abs(balance_used);
                if (balance_used >= 0) //Thêm số dư
                {
                    user.Balance += balance; //Số dư tăng lên
                    //Số dư sử dụng không thay đổi
                    user.Balance_Total += balance;//Tổng số nạp tăng lên
                    message = $"Tăng số dư +{balance}";
                    idHistory = 1;
                }
                else //Trừ số dư
                {
                    user.Balance -= balance;
                    user.Balance_Used += balance;//Số dư sử dụng tăng lên
                    message = $"Giảm số dư -{balance}";
                    idHistory = 2;

                }

                await _userManager.UpdateAsync(user);
                await _historyServices.AddAsync(new History
                {
                    Userid = user.Id,
                    HistoryTypeId = idHistory,
                    DateCreated = Core.Date_GetTimeNowInt(),
                    Value = balance_used,
                    Header = $"Thay đổi số dư từ hệ thống ",
                    Content = message,
                    Username = user.UserName,
                });
                return "Thành công " + message;

            }
            catch (Exception ex)
            {
                return $"Lỗi exception khi cài đặt số dư {ex.Message}";
            }
        }
        public async Task<bool> CheckBalance(string userId, long balance_used)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    if (user.Balance >= balance_used)
                    {
                        return true;
                    }
                }


            }
            catch (Exception)
            {

            };
            return false;
        }
        public async Task<string> CheckRole(string userId)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    return user.RoleName;

                }

            }
            catch (Exception)
            {

            };
            return string.Empty;
        }
    }

}
