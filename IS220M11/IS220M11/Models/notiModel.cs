using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IS220M11.Models
{
    public class notiModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotiID {get; set;}
        
        [Display(Name ="Ngày thông báo")]
        public DateTime NDate{get; set;}
        
        [Display(Name ="Nội dung thông báo")]
        public string NContent {get; set;}

        [ForeignKey("UserID")]
        [Display(Name ="Mã tài khoảng thông báo")]
        public int NUserID {get; set;}
        public accountModel accounts {get; set;}
    }
}