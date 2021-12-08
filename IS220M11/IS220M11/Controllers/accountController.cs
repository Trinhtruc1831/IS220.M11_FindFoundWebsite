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
    }
}
