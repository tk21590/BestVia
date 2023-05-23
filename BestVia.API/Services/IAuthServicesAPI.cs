using BestVia.Models;
using Microsoft.AspNetCore.Identity;
using BestVia.API.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BestVia.API.Services
{
    public interface IAuthServicesAPI
    {     //===================AUTH=====================================//
        Task<ApiRespone> RegisterUserAsync(RegisterModel model);
        Task<ApiRespone> LoginUserAsync(LoginModel model);

    }

    public class AuthServicesAPI : IAuthServicesAPI
    {
        private UserManager<IUsers> _userManager;
        private SignInManager<IUsers> _singinManager;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _config;

        public AuthServicesAPI(UserManager<IUsers> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IUsers> singinManager,
             IConfiguration config
             )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _singinManager = singinManager;
            _config = config;
        }

        public async Task<ApiRespone> LoginUserAsync(LoginModel model)
        {
            try
            {


                var user = await _userManager.FindByNameAsync(model.Username);

                //await _siginManager.SignInAsync(user, isPersistent: false);
                if (user == null)
                {
                    return Core.CreateRespone(respone_code.notfound, null, "sai pass hoặc username");
                }
                //sử dụng sigin để kiểm tra password
                //lockoutOnFailure cập nhật số lần đăng nhập
                //isPersitent ; có lưu cookie hay không (Rêmmember)
                //Nếu user bị khóa vĩnh viễn
                if (user.Block)
                {
                    return Core.CreateRespone(respone_code.error, null, "user đang bị khóa");
                }


                var signin = await _singinManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: true);

                if (signin.IsLockedOut) //Kiểm tra xem account có bị khóa tạm thời hay không
                {
                    return Core.CreateRespone(respone_code.error, null, "tài khoản đang bị khóa tạm thời!");
                }

                if (!signin.Succeeded)
                {
                    return Core.CreateRespone(respone_code.failed, null, "sai pass hoặc username");

                }

                var list_user_role = await _userManager.GetRolesAsync(user);
                string role = list_user_role[0];
                var claims = new List<Claim>
                {
                	new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Sid, user.ApiKey),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim("Balance", user.Balance.ToString()),

                };
                foreach (var item in list_user_role)
                {
                    claims.Add(new Claim("roles", item));

                }

                ///add thêm lần cuối login
                user.DateLastLogin = Core.Date_GetTimeNowInt();
                var updatelastLogin = await _userManager.UpdateAsync(user);
                if (!updatelastLogin.Succeeded)
                {
                    return Core.CreateRespone(respone_code.failed, null, "lỗi cài đặt user");

                }

                var keyAuths = _config["AuthSettings:Key"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyAuths));
                var token = new JwtSecurityToken
                    (
                    issuer: _config["AuthSettings:Issuer"],
                    audience: _config["AuthSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(7).AddDays(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );

                string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
                return Core.CreateRespone(respone_code.success, tokenAsString, "Login thành công");

            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, null, ex.Message);
            }
        }
        public async Task<ApiRespone> RegisterUserAsync(RegisterModel model)
        {
            try
            {


                if (model == null)
                {
                    return Core.CreateRespone(respone_code.failed, null, "param không đúng");
                }
                if (model.Password != model.ConfirmPassword)
                {
                    return Core.CreateRespone(respone_code.failed, null, "re-password không đúng");
                }


                var find_name = await _userManager.FindByNameAsync(model.Username);
                if (find_name != null)
                {
                    return Core.CreateRespone(respone_code.failed, null, "đã có người sử dụng tên tài khoản này");
                }
                var subid = _userManager.Users.Count();
                subid++;
                var identityUser = new IUsers
                {
                    UserName = model.Username,
                    ApiKey = Core.RandomToken(32).ToLower(),
                    RefCode = "REF" + Core.RandomToken(8).ToUpper(),
                    Balance = 0,
                    Balance_Total = 0,
                    Balance_Used = 0,
                    Block = false,
                    RoleName = "Customer",
                    DateLastLogin = Core.Date_GetTimeNowInt(),
                    SubId = subid,
                    DateCreated = Core.Date_GetTimeNowInt(),
                    RefAdd = model.RefAdd
                    
                };


                string roleName = "Customer";
                var checkroleIsExist = await _roleManager.FindByNameAsync(roleName);
                if (checkroleIsExist == null)
                {
                    var role = new IdentityRole();
                    role.Name = roleName;
                    var resultRole = await _roleManager.CreateAsync(role);
                    if (resultRole == null || !resultRole.Succeeded)
                    {
                        return Core.CreateRespone(respone_code.failed, null, "Thiết lập quyền thất bại");

                    }

                }
                try
                {
                    var result = await _userManager.CreateAsync(identityUser, model.Password);
                    if (result.Succeeded)
                    {

                        var result1 = await _userManager.AddToRoleAsync(identityUser, roleName);
                        return Core.CreateRespone(respone_code.success, null, "Đăng kí thành công");
                    }
                }
                catch (Exception err)
                {
                    string k = err.Message;
                    return Core.CreateRespone(respone_code.failed, null, err.Message);

                }



                return Core.CreateRespone(respone_code.failed, "đăng kí thất bại");
            }
            catch (Exception ex)
            {

                return Core.CreateRespone(respone_code.error, null, ex.Message);
            }
        }

    }
}
