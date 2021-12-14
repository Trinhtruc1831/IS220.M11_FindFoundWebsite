using IS220M11.Data;
using IS220M11.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220M11.Controllers
{
    public class accountController:Controller
    {
        private readonly FindFoundContext _context;
        public accountController(FindFoundContext context)
        {
            _context = context;
        }
        public IEnumerable<accountModel> GetListAccount()
        {
            return _context.accounts;
        }
        public string GetEmail()
        {
            var query = from st in _context.accounts
                        select new
                        {
                            email = st.UEmail
                        };
            List<string> EmailList = new List<string>();
            foreach (var ac in query)
            {
                EmailList.Add(ac.email);
            }
            return EmailList[1];
        }
    }
}
