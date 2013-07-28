using BusinessLogic.Repository;
using Common.Tools;
using DataAccess;
using DataAccess.Repository;
using Softox.Models;
using System.Linq;
using System.Web.Mvc;

namespace Softox.Controllers
{
    public class CategoryController : Controller
    {
        /// <summary>
        /// Index of the page
        /// </summary>
        public ActionResult Index()
        {
            using (var context = new CategoryRepository())
            {
                ViewBag.Entities = context.Get().OrderBy(x => x.cat_name).ToList();
                return View();
            }
        }

        /// <summary>
        /// Action to load the view where you can add an object
        /// </summary>
        public ActionResult Add()
        {
            return PartialView("Manager", new CategoryModel());
        }

        /// <summary>
        /// Action to load the view where you can edit an object
        /// </summary>
        public ActionResult Edit(int id)
        {
            using (var context = new CategoryRepository())
            {
                return PartialView("Manager", CategoryModel.EntityToModel(context.GetById(id)));
            }
        }

        /// <summary>
        /// Action to save an object (Add or Edit it in the db)
        /// </summary>
        [HttpPost]
        public ActionResult SaveChanges(CategoryModel model)
        {
            if (!ModelState.IsValid)
                return null;

            T_Category entity = CategoryModel.ModelToEntity(model);
            string msg = string.Empty;
            bool isNew = true;
            if (model.cat_id <= 0)
            {
                msg = "Nouvelle catégorie ajoutée.";
                using (var context = new CategoryRepository())
                {
                    context.Add(entity);
                    context.SaveChanges();
                    Log.Info(string.Format("Edit category id={0} name={1}", entity.cat_id, entity.cat_name));
                }
            }
            else
            {
                msg = "Catégorie modifiée.";
                isNew = false;
                using (var context = new CategoryRepository())
                {
                    context.Edit(entity);
                    context.SaveChanges();
                    Log.Info(string.Format("Create category id={0} name={1}", entity.cat_id, entity.cat_name));
                }
            }

            return Json(new
                {
                    msg = msg,
                    isNew = isNew,
                    entityName = "Category",
                    id = entity.cat_id,
                    fields = new
                    {
                        cat_name = entity.cat_name
                    }
                });
        }

        /// <summary>
        /// Action to remove an object (Remove in db)
        /// </summary>
        public ActionResult Remove(int id)
        {
            using (var context = new CategoryRepository())
            {
                T_Category entity = context.GetById(id);
                Log.Info(string.Format("Remove category id={0} name={1}", entity.cat_id, entity.cat_name));

                context.Remove(entity);
                context.SaveChanges();

                return Content("Catégorie supprimée.", "text/html");
            }
        }
    }
}