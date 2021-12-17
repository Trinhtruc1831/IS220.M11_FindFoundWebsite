using IS220M11.Data;
using IS220M11.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS220M11.Controllers
{
    public class postController:Controller
    {
        private readonly FindFoundContext _context;
    
        public postController(FindFoundContext context)
        {
            _context = context;
        }
        public IQueryable<object> GetPostTit()
        {
            var query = from post in _context.posts
                        join pic in _context.pictures on post.PostID equals pic.IPostID
                        where pic.IOrder == 1
                        select new
                        {
                            price = post.PPrice,
                            tit = post.PTitle,
                            tnpic = pic.ILink
                        };
            List<object> a = query.ToList<object>();
            return query;
        }
    }

}
