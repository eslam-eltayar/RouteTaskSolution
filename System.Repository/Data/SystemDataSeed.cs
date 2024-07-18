using System;
using System.Collections.Generic;
using System.Domain.Entities;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace System.Repository.Data
{
    public class SystemDataSeed
    {
        public static async Task SeedAsync(SystemContext dbcontext)
        {
            if (!dbcontext.Products.Any())
            {
                var ProductData = File.ReadAllText("../System.Repository/Data/DataSeed/Product.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                foreach (var item in products)
                {
                    await dbcontext.Set<Product>().AddAsync(item);
                }
                await dbcontext.SaveChangesAsync();
            }
        }
    }
}
