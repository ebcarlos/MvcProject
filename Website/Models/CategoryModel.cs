using Common.BaseClass;
using DataAccess;
using DataAccess.Repository;
using System.ComponentModel.DataAnnotations;

namespace Softox.Models
{
    public class CategoryModel : BaseModel<CategoryModel, T_Category>
    {
        public int cat_id { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Nom")]
        public string cat_name { get; set; }
    }
}