using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShopMates.Data.EF
{
    public class ShopMatesDbContextFactory : IDesignTimeDbContextFactory<ShopMatesDbContext>
    {
        public ShopMatesDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var connectionString = config.GetConnectionString("ShopMatesSolustionDatabase");

            var optionBuilder = new DbContextOptionsBuilder<ShopMatesDbContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new ShopMatesDbContext(optionBuilder.Options);
        }
    }
}
