using BusinessLogic.Repository;
using Common.Tools;
using DataAccess;
using DataAccess.Repository;
using Softox.Models;
using System.Linq;
using System.Web.Mvc;

namespace Softox.Controllers
{
    public class TopController : Controller
    {
        /// <summary>
        /// Index of the page
        /// </summary>
        public ActionResult Index()
        {
            using (var context = new TopRepository())
            {
                ViewBag.Entities = context.Get().OrderBy(x => x.top_title).ToList();
                return View();
            }
        }

        /// <summary>
        /// Action to load the view where you can add an object
        /// </summary>
        public ActionResult Add()
        {
            return PartialView("Manager", new TopModel());
        }

        /// <summary>
        /// Action to load the view where you can edit an object
        /// </summary>
        public ActionResult Edit(int id)
        {
            using (var context = new TopRepository())
            {
                return PartialView("Manager", TopModel.EntityToModel(context.GetById(id)));
            }
        }

        /// <summary>
        /// Action to save an object (Add or Edit it in the db)
        /// </summary>
        [HttpPost]
        public ActionResult SaveChanges(TopModel model)
        {
            if (!ModelState.IsValid)
                return null;

            T_Top entity = TopModel.ModelToEntity(model);
            string msg = string.Empty;
            bool isNew = true;
            if (model.top_id <= 0)
            {
                msg = "Nouveau classement ajouté.";
                using (var context = new TopRepository())
                {
                    context.Add(entity);
                    context.SaveChanges();
                    Log.Info(string.Format("Edit top id={0} name={1}", entity.top_id, entity.top_title));
                }
            }
            else
            {
                msg = "Classement modifié.";
                isNew = false;
                using (var context = new TopRepository())
                {
                    context.Edit(entity);
                    context.SaveChanges();
                    Log.Info(string.Format("Create top id={0} name={1}", entity.top_id, entity.top_title));
                }
            }

            return Json(new
            {
                msg = msg,
                isNew = isNew,
                entityName = "Top",
                id = entity.top_id,
                fields = new
                {
                    top_title = entity.top_title
                }
            });
        }

        /// <summary>
        /// Action to remove an object (Remove in db)
        /// </summary>
        public ActionResult Remove(int id)
        {
            using (var context = new TopRepository())
            {
                T_Top entity = context.GetById(id);
                Log.Info(string.Format("Remove category id={0} name={1}", entity.top_id, entity.top_title));

                context.Remove(entity);
                context.SaveChanges();

                return Content("Classement supprimé.", "text/html");
            }
        }
    }
}