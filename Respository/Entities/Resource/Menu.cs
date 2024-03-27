using FreeSql.DataAnnotations;
using Infrastructure.Base;

namespace Repository.Entities
{
    [Table(Name = "menu")]
    public class Menu : IEntityBase
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        [Column(IsNullable = false, Name = "menu_code", StringLength = 128)]
        public string MenuCode { get; set; }
        /// <summary>
        /// 资源路径
        /// </summary>
        [Column(IsNullable = false, Name = "menu_path", StringLength = 1024)]
        public string MenuPath { get; set; }
    }
}
