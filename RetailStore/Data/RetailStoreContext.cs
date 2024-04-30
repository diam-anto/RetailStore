using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RetailStore.Data
{
    public class RetailStoreContext : DbContext
    {
        public RetailStoreContext (DbContextOptions<RetailStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;

        public DbSet<Customer> Customer { get; set; } = default!;

        public DbSet<Purchase> Purchase { get; set; } = default!;
        public DbSet<OrderProduct> OrderProduct { get; set; } = default!;
    }
}
