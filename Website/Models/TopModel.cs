using BusinessLogic.Repository;
using Common.BaseClass;
using DataAccess;
using DataAccess.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Softox.Models
{
    public class TopModel : BaseModel<TopModel, T_Top>
    {
        public int top_id { get; set; }

        public int top_fk_cat_id { get; set; }

        public int top_fk_user_id { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Titre")]
        public string top_title { get; set; }

        [Display(Name = "Description")]
        public string top_description { get; set; }

        [Display(Name = "Source")]
        public string top_source { get; set; }

        [Display(Name = "Date de création")]
        public DateTime top_date_creation { get; set; }

        [Display(Name = "Date de dernière modification")]
        public Nullable<DateTime> top_date_modification { get; set; }

        [Display(Name = "Nombre de vue")]
        public int top_view_count { get; set; }

        [Display(Name = "Status")]
        public byte top_status { get; set; }

        [Display(Name = "Catégorie")]
        public SelectList CategoryList { get; set; }

        [Required]
        public int CategorySelected { get; set; }

        public TopModel()
        {
            var context = new CategoryRepository();
            CategoryList = new SelectList(context.Get().OrderBy(x => x.cat_name).ToList(), "cat_id", "cat_name");
            CategorySelected = 0;
        }

        public static new T_Top ModelToEntity(TopModel model)
        {
            T_Top entity = BaseModel<TopModel, T_Top>.ModelToEntity(model);

            //    public int top_fk_cat_id { get; set; }
            //    public int top_fk_user_id { get; set; }

            return entity;
        }
    }
}