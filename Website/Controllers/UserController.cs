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
        private UserRepository _repository;

        public UserController()
        {
            _repository = new UserRepository();
        }

        public ActionResult Index()
        {
            ViewBag.Entities = _repository.Get().OrderBy(x => x.user_name).ToList();
            return View();
        }

        public ActionResult Remove(int id)
        {
            T_User entity = _repository.GetById(id);
            _repository.Remove(entity);
            _repository.SaveChanges();
            Log.Info(string.Format("Remove user id={0} name={1}", entity.user_id, entity.user_name));

            return Content("Utilisateur supprimé.", "text/html");
        }

        public ActionResult Add()
        {
            return PartialView("Manager", new UserModel());
        }

        public ActionResult Edit(int id)
        {
            return PartialView("Manager", UserModel.EntityToModel(_repository.GetById(id)));
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
                _repository.Add(entity);
                _repository.SaveChanges();
                Log.Info(string.Format("Edit user id={0} name={1}", entity.user_id, entity.user_name));

                return GenerateJson(entity, true, "Nouveau utilisateur ajouté.");
            }
            // Save edit action
            else
            {
                _repository.Edit(entity);
                _repository.SaveChanges();
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
                        user_name = entity.user_name
                    },
                    isNew = isNew,
                    msg = msg
                });
        }
    }
}