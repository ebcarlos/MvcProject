using BusinessLogic.Repository;
using Common.Tools;
using DataAccess.Repository;
using Softox.Models;
using System.Linq;
using System.Web.Mvc;

namespace Softox.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryRepository _repository;

        public CategoryController()
        {
            _repository = new CategoryRepository();
        }

        public ActionResult Index()
        {
            ViewBag.Entities = _repository.Get().OrderBy(x => x.cat_name).ToList();
            return View();
        }

        public ActionResult Remove(int id)
        {
            T_Category entity = _repository.GetById(id);
            _repository.Remove(entity);
            _repository.SaveChanges();
            Log.Info(string.Format("Remove category id={0} name={1}", entity.cat_id, entity.cat_name));

            return Content("Catégorie supprimée.", "text/html");
        }

        public ActionResult Add()
        {
            return PartialView("Manager", new CategoryModel());
        }

        public ActionResult Edit(int id)
        {
            return PartialView("Manager", CategoryModel.EntityToModel(_repository.GetById(id)));
        }

        [HttpPost]
        public ActionResult SaveChanges(CategoryModel model)
        {
            if (!ModelState.IsValid)
                return null;

            T_Category entity = CategoryModel.ModelToEntity(model);

            // Save add action
            if (model.cat_id <= 0)
            {
                _repository.Add(entity);
                _repository.SaveChanges();
                Log.Info(string.Format("Edit category id={0} name={1}", entity.cat_id, entity.cat_name));

                return GenerateJson(entity, true, "Nouvelle catégorie ajoutée.");
            }
            // Save edit action
            else
            {
                _repository.Edit(entity);
                _repository.SaveChanges();
                Log.Info(string.Format("Create category id={0} name={1}", entity.cat_id, entity.cat_name));

                return GenerateJson(entity, false, "Catégorie modifiée.");
            }
        }

        private ActionResult GenerateJson(T_Category entity, bool isNew, string msg)
        {
            return Json(new
                {
                    entityName = "Category",
                    id = entity.cat_id,
                    fields = new
                    {
                        cat_name = entity.cat_name
                    },
                    isNew = isNew,
                    msg = msg
                });
        }
    }
}