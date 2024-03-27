using Infrastructure.Cache;
using Infrastructure.Extensions;
using Infrastructure.Helpers;
using Infrastructure.JWT.Check;
using Infrastructure.Model.RequestModel;
using Mapster;
using Repository.Entities;
using Service.Contracts;
using Service.Model.User;

namespace Service.Service
{
    internal class UserService : IUserService
    {
        private readonly IFreeSql _freeSql;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICacheService _cacheService;
        public UserService(IFreeSql freeSql, IAuthenticationService authenticationService, ICacheService cacheService)
        {
            _freeSql = freeSql;
            _authenticationService = authenticationService;
            _cacheService = cacheService;
        }


        public async Task<string> CheckLoginAsync(string userName, string passWord)
        {
            var user = await _freeSql.Select<User>().Where(a => a.LoginName == userName && a.Password == passWord).FirstAsync();
            user.IsNullOrEmpty().TrueThrowException("账号密码不正确");
            return _authenticationService.CreateToken(user);
        }

        public async Task<User> AddUserAsync(UserModel arg)
        {
            var user = arg.Adapt<User>();
            var repo = _freeSql.GetRepository<User>();
            await repo.InsertAsync(user);
            EmailHelper.SingleSendMailAsync(user.Email);
            return user;
        }

        public async Task<PageListModel<UserModel>> GetUserListAsync(QueryPageListModel arg)
        {
            var result = new PageListModel<UserModel>();
            var repo = _freeSql.GetRepository<User>();
            var list = await repo.Where(a => true).Skip((arg.Index - 1) * arg.Size).Take(arg.Size).OrderBy(a => a.UserName).ToListAsync();
            result.Data = list.Adapt<List<UserModel>>();
            result.Count = await repo.Where(a => true).CountAsync();
            return result;
        }

        public async Task<UserModel> GetUserInfoAsync(string code)
        {
            User user;
            var userCache = _cacheService.GetCacheByKey("User_" + code);
            if (userCache.IsNullOrEmpty())
            {
                var repo = _freeSql.GetRepository<User>();
                user = await repo.Where(a => a.Code == code).FirstAsync<User>();
                _cacheService.SetCache(user, "User_" + code);
            }
            else
            {
                user = userCache.ToObject<User>();
            }

            return user.Adapt<UserModel>();
        }
    }
}
