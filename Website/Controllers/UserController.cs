using BusinessLogic.Repository;
using Common.Tools;
using DataAccess;
using DataAccess.Repository;
using Softox.Models;
using System.Linq;
using System.Web.Mvc;

namespace Softox.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// Index of the page
        /// </summary>
        public ActionResult Index()
        {
            using (var context = new UserRepository())
            {
                ViewBag.Entities = context.Get().OrderBy(x => x.user_name).ToList();
                return View();
            }
        }

        /// <summary>
        /// Action to load the view where you can add an object
        /// </summary>
        public ActionResult Add()
        {
            return PartialView("Manager", new UserModel());
        }

        /// <summary>
        /// Action to load the view where you can edit an object
        /// </summary>
        public ActionResult Edit(int id)
        {
            using (var context = new UserRepository())
            {
                return PartialView("Manager", UserModel.EntityToModel(context.GetById(id)));
            }
        }

        /// <summary>
        /// Action to save an object (Add or Edit it in the db)
        /// </summary>
        [HttpPost]
        public ActionResult SaveChanges(UserModel model)
        {
            if (!ModelState.IsValid)
                return null;

            T_User entity = UserModel.ModelToEntity(model);
            string msg = string.Empty;
            bool isNew = true;
            if (model.user_id <= 0)
            {
                msg = "Nouvel utilisateur ajouté.";
                using (var context = new UserRepository())
                {
                    context.Add(entity);
                    context.SaveChanges();
                    Log.Info(string.Format("Edit user id={0} name={1}", entity.user_id, entity.user_name));
                }
            }
            else
            {
                msg = "Utilisateur modifié.";
                isNew = false;
                using (var context = new UserRepository())
                {
                    context.Edit(entity);
                    context.SaveChanges();
                    Log.Info(string.Format("Create user id={0} name={1}", entity.user_id, entity.user_name));
                }
            }

            return Json(new
            {
                msg = msg,
                isNew = isNew,
                entityName = "User",
                id = entity.user_id,
                fields = new
                {
                    cat_name = entity.user_name
                }
            });
        }

        /// <summary>
        /// Action to remove an object (Remove in db)
        /// </summary>
        public ActionResult Remove(int id)
        {
            using (var context = new UserRepository())
            {
                T_User entity = context.GetById(id);
                Log.Info(string.Format("Remove user id={0} name={1}", entity.user_id, entity.user_name));

                context.Remove(entity);
                context.SaveChanges();

                return Content("Utilisateur supprimé.", "text/html");
            }
        }
    }
}