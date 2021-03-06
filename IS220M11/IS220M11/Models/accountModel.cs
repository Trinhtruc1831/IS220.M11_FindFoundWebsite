using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IS220M11.Models
{
    public class accountModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID {get; set;}
        public string UName {get; set;}
        public string UPass {get; set;}
        public int UType {get; set;}
        public int UStatus {get; set;}
        public string UEmail {get; set;}
        public string UPhone {get; set;}
        public string UAva { get; set; }
        [NotMapped]
        public IFormFile CoverPhoto { get; set; }
        public ICollection<banModel> bans {get; set;}
        public ICollection<reportModel> reports {get; set;}
        public ICollection<chatModel> chats { get; set; }
        public ICollection<notiModel> notis {get; set;}
        public ICollection<interestModel> interests { get; set; }
        public ICollection<postModel> posts { get; set; }
        public ICollection<commentModel> comments { get; set; }
    }
}