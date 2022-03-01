using ItExp_dotNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItExp_dotNet.Services
{
    public class ThisAppContext : DbContext
    {
        public ThisAppContext() : base("ItExpDB") { }

        public DbSet<User> Users { get; set; }
    }
}
