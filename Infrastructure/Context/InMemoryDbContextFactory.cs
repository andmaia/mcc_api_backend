using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public static class InMemoryDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Nome único por teste
                .Options;

            var dbContext = new ApplicationDbContext(options);

            SeedDatabase(dbContext);

            return dbContext;
        }

        private static void SeedDatabase(ApplicationDbContext context)
        {
           
        }
    }
}
