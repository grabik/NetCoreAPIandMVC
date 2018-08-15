using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.DataManager
{
    public class CategoriesManager : IDataRepository<Categories, long>
    {
        ApplicationContext ctx;
        public CategoriesManager(ApplicationContext c)
        {
            ctx = c;
        }

        public Categories Get(long id)
        {
            var categories = ctx.Category.FirstOrDefault(b => b.CategoriesId == id);
            return categories;
        }

        public IEnumerable<Categories> GetAll()
        {
            var categories = ctx.Category.ToList();
            return categories;
        }

        public long Add(Categories categories)
        {
            ctx.Category.Add(categories);
            long categoriesID = ctx.SaveChanges();
            return categoriesID;
        }

        public long Delete(long id)
        {
            int categoriesID = 0;
            var cat = ctx.Category.FirstOrDefault(b => b.CategoriesId == id);
            if (cat != null)
            {
                ctx.Category.Remove(cat);
                categoriesID = ctx.SaveChanges();
            }
            return categoriesID;
        }

        public long Update(long id, Categories item)
        {
            long categoriesID = 0;
            var category = ctx.Category.Find(id);
            if (category != null)
            {
                category.Name = item.Name;
                category.Description = item.Description;

                categoriesID = ctx.SaveChanges();
            }
            return categoriesID;
        }
    }
}
