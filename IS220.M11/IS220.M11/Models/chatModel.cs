using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IS220.M11.Models
{
    public class chatModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChatID { get; set; }
        public int ChUserID { get; set; }
        [ForeignKey("UserID")]
        public accountModel accounts { get; set; }
        [Display(Name = "Thời điểm chat")]
        public DateTime ChDate { get; set; }
        [Display(Name = "Nội dung chat")]
        public string ChContent { get; set; }
    }
}
