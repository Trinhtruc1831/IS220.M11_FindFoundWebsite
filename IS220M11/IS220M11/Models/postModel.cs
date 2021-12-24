using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IS220M11.Models
{
    public class postModel
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PostID { get; set; }
		public int PUserID { get; set; }
		[ForeignKey("PUserID")]
		public accountModel account { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
		public string PTitle { get; set; }
		public int PPrice { get; set; }
		public int Heart { get; set; }
		public int PStatus { get; set; }
		public DateTime PDate { get; set; }
		public ICollection<pictureModel> pictures { get; set; }
	}
}