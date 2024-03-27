using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.AppSetting.AppSettingSubModel
{
    public class NacosConnection
    {

        /// <summary>
        /// 
        /// </summary>
        public List<ListenersItem> Listeners { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ServerAddresses { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
    }
    public class ListenersItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Optional { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DataId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string @Group { get; set; }
    }
}
