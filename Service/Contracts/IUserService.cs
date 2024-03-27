using Infrastructure.Base;
using Infrastructure.Model.RequestModel;
using Repository.Entities;
using Service.Model.User;

namespace Service.Contracts
{
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        Task<string> CheckLoginAsync(string userName, string passWord);
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        Task<User> AddUserAsync(UserModel arg);
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        Task<PageListModel<UserModel>> GetUserListAsync(QueryPageListModel arg);
        /// <summary>
        /// 获得用户详情
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<UserModel> GetUserInfoAsync(string code);
    }
}
