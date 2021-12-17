using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IS220M11.Models
{
    public class commentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }
        public int CPostID { get; set; }
        [ForeignKey("CPostID")]
        public postModel post { get; set; }
        public int CUserID { get; set; }
        [ForeignKey("CUserID")]
        public accountModel account { get; set; }
        [Display(Name = "Thời điểm comment")]
        public DateTime CDate { get; set; }
        [Display(Name = "Nội dung comment")]
        public string CContent { get; set; }
        [Display(Name = "Giá đề nghị")]
        public int CPrice { get; set; }

    }
}