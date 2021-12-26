using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IS220M11.Models
{
    public class chatModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChatID { get; set; }
        public int ChUserID { get; set; }
        [ForeignKey("ChUserID")]
        public accountModel account { get; set; }
        [Display(Name = "Thời điểm chat")]
        public string ChDate { get; set; }
        [Display(Name = "Nội dung chat")]
        public string ChContent { get; set; }
    }
}
