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
        [ForeignKey("InPostID")]
        public postModel post{ get; set; }

        [Key]
        /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
        public int InUserID { get; set; }
        [ForeignKey("InUserID")]
        public accountModel account{ get; set; }

        public DateTime InDate { get; set; }

    }
}

