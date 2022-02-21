using Microsoft.AspNetCore.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<Product> context;
        InMemoryRepository<ProductCategory> pcr;
        public ProductManagerController()
        {
            context = new InMemoryRepository<Product>();
            pcr=new InMemoryRepository<ProductCategory>();
        }

        public IActionResult Index()
        {
            List<Product> pl = context.Collection().ToList();
            return View(pl);
        }

        public IActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();  

            viewModel.Product = new Product();
            viewModel.pcl = pcr.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(string Id)
        {
            Product product= context.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product= product;
                viewModel.pcl = pcr.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return NotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description=product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public IActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return NotFound();
            }
            else
            {
                return View(productToDelete);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return NotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");

            }
        }
    }
}
