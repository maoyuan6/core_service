﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.RequestModel
{
    public class PageListModel<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public long Count { get; set; }

        public List<T> Data { get; set; }
    }
}
