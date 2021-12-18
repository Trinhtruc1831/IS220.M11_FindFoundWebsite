using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IS220M11.Models
{
    public class picpostModel
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PostID { get; set; }
		// public int PUserID { get; set; }
		// [ForeignKey("PUserID")]
		// public accountModel account { get; set; }
		public string PTitle { get; set; }
        public string ILink { get; set; }
		public int PPrice { get; set; }
		public ICollection<pictureModel> pictures { get; set; }
	}
}