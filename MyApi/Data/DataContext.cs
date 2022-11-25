using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;

namespace MyApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public string DbPath { get; }

        public DataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "product.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // //C:\Users\userName\AppData\Local
        protected override void OnConfiguring(DbContextOptionsBuilder options)
                    => options.UseSqlite($"Data Source={DbPath}");

    }


}
