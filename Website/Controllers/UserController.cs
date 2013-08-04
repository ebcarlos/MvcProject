using BusinessLogic.Repository;
using Common.Tools;
using DataAccess.Repository;
using DataAccess.Status;
using Softox.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Softox.Controllers
{
    public class UserController : Controller
    {
        private UserRepository _context;

        public UserController()
        {
            _context = new UserRepository();
        }

        public ActionResult Index()
        {
            ViewBag.Entities = _context.Get().OrderBy(x => x.user_name).ToList();
            return View();
        }

        public ActionResult Remove(int id)
        {
            T_User entity = _context.GetById(id);
            _context.Remove(entity);
            _context.SaveChanges();
            Log.Info(string.Format("Remove user id={0} name={1}", entity.user_id, entity.user_name));

            return Content("Utilisateur supprimé.", "text/html");
        }

        public ActionResult Add()
        {
            return PartialView("Manager", new UserModel());
        }

        public ActionResult Edit(int id)
        {
            return PartialView("Manager", UserModel.EntityToModel(_context.GetById(id)));
        }

        [HttpPost]
        public ActionResult SaveChanges(UserModel model)
        {
            if (!ModelState.IsValid)
                return null;

            T_User entity = UserModel.ModelToEntity(model);

            // Save add action
            if (model.user_id <= 0)
            {
                entity.user_password = "soft";
                entity.user_date_subscription = DateTime.Now;

                _context.Add(entity);
                _context.SaveChanges();
                Log.Info(string.Format("Edit user id={0} name={1}", entity.user_id, entity.user_name));

                return GenerateJson(entity, true, "Nouveau utilisateur ajouté.");
            }
            // Save edit action
            else
            {
                _context.Edit(entity);
                _context.SaveChanges();
                Log.Info(string.Format("Create user id={0} name={1}", entity.user_id, entity.user_name));

                return GenerateJson(entity, false, "Utilisateur modifié.");
            }
        }

        private ActionResult GenerateJson(T_User entity, bool isNew, string msg)
        {
            return Json(new
                {
                    entityName = "User",
                    id = entity.user_id,
                    fields = new
                    {
                        user_name = entity.user_name,
                        user_status = StatusHelper.GetName<UserStatus>(entity.user_status),
                        user_desc = entity.user_desc,
                        user_date_subscription = entity.user_date_subscription.ToShortDateString()
                    },
                    isNew = isNew,
                    msg = msg
                });
        }
    }
}