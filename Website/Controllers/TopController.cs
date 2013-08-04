using BusinessLogic.Repository;
using Common.Tools;
using DataAccess.Repository;
using Softox.Models;
using System.Linq;
using System.Web.Mvc;

namespace Softox.Controllers
{
    public class TopController : Controller
    {
        private TopRepository _context;

        public TopController()
        {
            _context = new TopRepository();
        }

        public ActionResult Index()
        {
            ViewBag.Entities = _context.Get().OrderBy(x => x.top_title).ToList();
            return View();
        }

        public ActionResult Remove(int id)
        {
            T_Top entity = _context.GetById(id);
            _context.Remove(entity);
            _context.SaveChanges();
            Log.Info(string.Format("Remove top id={0} name={1}", entity.top_id, entity.top_title));

            return Content("Classement supprimé.", "text/html");
        }

        public ActionResult Add()
        {
            return PartialView("Manager", new TopModel());
        }

        public ActionResult Edit(int id)
        {
            return PartialView("Manager", TopModel.EntityToModel(_context.GetById(id)));
        }

        [HttpPost]
        public ActionResult SaveChanges(TopModel model)
        {
            if (!ModelState.IsValid)
                return null;

            T_Top entity = TopModel.ModelToEntity(model);

            // Save add action
            if (model.top_id <= 0)
            {
                _context.Add(entity);
                _context.SaveChanges();
                Log.Info(string.Format("Edit top id={0} name={1}", entity.top_id, entity.top_title));

                return GenerateJson(entity, true, "Nouveau classement ajouté.");
            }
            // Save edit action
            else
            {
                _context.Edit(entity);
                _context.SaveChanges();
                Log.Info(string.Format("Create top id={0} name={1}", entity.top_id, entity.top_title));

                return GenerateJson(entity, false, "Classement modifié.");
            }
        }

        private ActionResult GenerateJson(T_Top entity, bool isNew, string msg)
        {
            return Json(new
                {
                    entityName = "Top",
                    id = entity.top_id,
                    fields = new
                    {
                        top_title = entity.top_title
                    },
                    isNew = isNew,
                    msg = msg
                });
        }
    }
}