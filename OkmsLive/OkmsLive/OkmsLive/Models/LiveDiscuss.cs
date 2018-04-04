using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkmsLive.Model
{
    public class LiveDiscuss
    {
        public int lid { get; set; }
        public string HeadPhoto { get; set; }

        public string STrueName { get; set; }

        public string Content { get; set; }
        public string SendToConnID { get; set; }

        public string ATrueName { get; set; }
        public string CreateDate { get; set; }

        public DateTime SendDate { get; set; }
    }

    public class DiscussModel
    {
        public int ID { get; set; }

        public string UserName { get; set; }
        public string TrueName { get; set; }

        public string Contents { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateDateString { get; set; }
        public int? CourseID { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string oID { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int? Status { get; set; }

        public string GroupID { get; set; }

        public string CheckName { get; set; }
    }
}
