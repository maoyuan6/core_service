using FreeSql.DataAnnotations;
using Infrastructure.Base;
using Repository.Entities;

namespace Repository.Entities
{
    [Table(Name = "role")]
    public class Role : IEntityBase
    {
        /// <summary>
        /// 角色id
        /// </summary>
        [Column(IsIdentity = true, Name = "role_id")]
        public int RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [Column(StringLength = 30, Name = "role_name", IsNullable = false)]
        public string RoleName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Column(StringLength = 255, Name = "description")]
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local, CanUpdate = false, IsNullable = false, Name = "create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column(ServerTime = DateTimeKind.Local, CanUpdate = true, IsNullable = false, Name = "update_time")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 用户列表
        /// </summary>
        public virtual ICollection<User> UserList { get; set; }
    }
}
