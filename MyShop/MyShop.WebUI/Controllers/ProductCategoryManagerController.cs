using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;
        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }

        public IActionResult Index()
        {
            List<ProductCategory> pcl = context.Collection().ToList();
            return View(pcl);
        }

        public IActionResult Create()
        {
            ProductCategory pc = new ProductCategory();
            return View(pc);
        }

        [HttpPost]
        public IActionResult Create(ProductCategory pc)
        {
            if (!ModelState.IsValid)
            {
                return View(pc);
            }
            else
            {
                context.Insert(pc);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(string Id)
        {
            ProductCategory pc = context.Find(Id);
            if (pc == null)
            {
                return NotFound();
            }
            else
            {
                return View(pc);
            }
        }

        [HttpPost]
        public IActionResult Edit(ProductCategory pc, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return NotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(pc);
                }

                productCategoryToEdit.Category = pc.Category;
                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public IActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return NotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
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
