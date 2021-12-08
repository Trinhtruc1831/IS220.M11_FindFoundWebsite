using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IS220M11.Models
{
    public class interestModel
    {
        [Key]
        /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
        public int InPostID { get; set; }
        [ForeignKey("PostID")]
        public postModel posts{ get; set; }

        [Key]
        /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
        public string InUserID { get; set; }
        [ForeignKey("UserID")]
        public accountModel accounts { get; set; }

    }
}

