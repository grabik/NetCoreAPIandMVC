using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.DataManager
{
    public class ProductsManager : IDataRepository<Products, long>
    {
        ApplicationContext ctx;
        public ProductsManager(ApplicationContext c)
        {
            ctx = c;
        }

        public Products Get(long id)
        {
            var products = ctx.Product.FirstOrDefault(b => b.ProductsId == id);
            return products;
        }

        public IEnumerable<Products> GetAll()
        {
            var products = ctx.Product.ToList();
            return products;
        }

        public long Add(Products products)
        {
            ctx.Product.Add(products);
            long productsID = ctx.SaveChanges();
            return productsID;
        }

        public long Delete(long id)
        {
            int productsID = 0;
            var prod = ctx.Product.FirstOrDefault(b => b.ProductsId == id);
            if (prod != null)
            {
                ctx.Product.Remove(prod);
                productsID = ctx.SaveChanges();
            }
            return productsID;
        }

        public long Update(long id, Products item)
        {
            long productsID = 0;
            var product = ctx.Product.Find(id);
            if (product != null)
            {
                product.Name = item.Name;
                product.ShortDescription = item.ShortDescription;
                product.Description = item.Description;
                product.Category = item.Category;

                productsID = ctx.SaveChanges();
            }
            return productsID;
        }
    }
}
