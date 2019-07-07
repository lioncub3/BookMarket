using BookMarket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMarket.Data
{
    public class BooksContext : IdentityDbContext<IdentityUser>
    {
        //private UserManager<IdentityUser> userManager;
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public BooksContext(DbContextOptions options) : base(options)
        {
            //this.userManager = userManager;

            //var user = new IdentityUser { Email = "leonid2000.11.11@gmail.com", UserName = "Admin" };
            //if (userManager.FindByNameAsync("Admin") == null)
            //    userManager.CreateAsync(user, "superpass");
        }
    }
}
