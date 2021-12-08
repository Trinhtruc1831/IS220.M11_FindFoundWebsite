using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IS220M11.Models
{
    public class pictureModel
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ImageID { get; set; }
		public int IPostID { get; set; }
		[ForeignKey("PostID")]
		public postModel posts { get; set; }
		public string ILink { get; set; }
		public int IOrder { get; set; }
	}
}