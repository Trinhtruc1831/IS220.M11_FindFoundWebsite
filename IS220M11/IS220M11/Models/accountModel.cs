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
        public int Hideinfo {get; set;}
        public ICollection<banModel> bans {get; set;}
        public ICollection<reportModel> reports {get; set;}
        public ICollection<notiModel> notis {get; set;}
    }
}