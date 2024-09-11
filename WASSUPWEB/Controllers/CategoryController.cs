using Microsoft.AspNetCore.Mvc;
using WASSUPWEB.Data;
using WASSUPWEB.Models;

namespace WASSUPWEB.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbcontext; //field -> represents state
        public CategoryController(ApplicationDbContext dbContext) //constructor parameter -> allows to intialize obj with specific values
        {
            _dbcontext = dbContext; //assign CP to F -> state of obj reflects values passed, allows obj to remember those values
        }


        //List

        public IActionResult Index()
        {
            List<Category> objCategoryList = _dbcontext.Categories.ToList();
            return View(objCategoryList);
        }


        //Add

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order should not exactly match the Name.");
            }


            if (ModelState.IsValid)
            {
                _dbcontext.Categories.Add(obj);
                _dbcontext.SaveChanges();
                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index", "Category");
            }
                return View();
            
        }



        //Edit

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            { 
                return NotFound(); 
            }
            Category? categoryFromDb = _dbcontext.Categories.Find(id);
            //Category? categoryFromDb1 = _dbcontext.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _dbcontext.Categories.Where(u => u.Id == id).FirstOrDefault(); 


            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }


        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order should not exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _dbcontext.Categories.Update(obj);
                _dbcontext.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();

        }




        //Delete Action
        
        //Get for Del
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _dbcontext.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //Post for Del

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _dbcontext.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _dbcontext.Categories.Remove(obj);
            _dbcontext.SaveChanges();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
            
        }

    }
}
