using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache c = MemoryCache.Default;
        List<ProductCategory> pcl;

        public ProductCategoryRepository()
        {
            pcl = c["pcl"] as List<ProductCategory>;
            if (pcl == null)
            {
                pcl = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            c["pcl"] = pcl;

        }

        public void Insert(ProductCategory pc)
        {
            pcl.Add(pc);
        }
        public void Update(ProductCategory pc)
        {
            ProductCategory productCategoryToUpdate = pcl.Find(p => p.Id == pc.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = pc;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory pc = pcl.Find(p => p.Id == Id);

            if (pc != null)
            {
                return pc;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return pcl.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productToDelete = pcl.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                pcl.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
    }
}
