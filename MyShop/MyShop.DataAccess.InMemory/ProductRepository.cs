using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;
namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache c= MemoryCache.Default;
        List <Product> pl = new List<Product> ();

        public ProductRepository()
        {
            pl = c["pl"] as List<Product>;
            if (pl == null)
            {
                pl = new List<Product> ();
            }
        }

        public void Commit()
        {
            c["pl"] = pl;

        }

        public void Insert(Product p)
        {
            pl.Add(p);
        }
        public void Update(Product product)
        {
            Product productToUpdate = pl.Find(p => p.Id == product.Id);
            
            if(productToUpdate != null)
            {
                productToUpdate = product;
            }
            else 
            {
                throw new Exception("Product not found");
            }
        }

        public Product Find(string Id)
        {
            Product product = pl.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return pl.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = pl.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                pl.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }






    }
}
