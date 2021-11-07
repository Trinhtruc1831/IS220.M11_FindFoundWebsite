using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IS220.M11.Models
{
    public class reportModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int ReportID {get; set;}
       

        [Display(Name ="Lý do báo cáo")]
        [StringLength(40, ErrorMessage ="Nội dung báo cáo phải < 200 kí tự")]
        public string RReason {get; set;}
        [Display(Name ="Ngày báo cáo")]
        public DateTime RDate {get; set;}

        [ForeignKey("UserID")]
        [Display(Name ="Mã tài khoản báo cáo")]
        public int RUserID {get; set;}
        [ForeignKey("UserID")]
        [Display(Name ="Mã tài khoản bị báo cáo")]
        public int RedUserID {get; set;}
        public accountModel accounts {get; set;}
        
    }
}