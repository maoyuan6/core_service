using FreeSql.DataAnnotations;
using Infrastructure.Base;
using Repository.Entities;
using Repository.Entities.Product.Order;

namespace Repository.Entities
{
    [Index("uk_id", "user_id", true)]
    [Table(Name = "user")]
    public class User : IEntityBase
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Column(IsIdentity = true, Name = "user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        [Column(IsNullable = false, Name = "code", StringLength = 128)]
        public string Code { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Column(IsNullable = false, Name = "user_name", StringLength = 128)]
        public string UserName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [Column(IsNullable = false, Name = "login_name", StringLength = 128)]
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column(Name = "password", StringLength = 128)]
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Column(Name = "email", StringLength = 128)]
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Column(Name = "phone_number", StringLength = 64)]
        public string PhoneNumber { get; set; } 
    }
}
