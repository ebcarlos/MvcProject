using BusinessLogic.Repository;
using Common.BaseClass;
using Common.Tools;
using DataAccess;
using DataAccess.Repository;
using DataAccess.Status;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Softox.Models
{
    public class UserModel : BaseModel<UserModel, T_User>
    {
        public int user_id { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Pseudo")]
        public string user_name { get; set; }

        public string user_password { get; set; }

        [Display(Name = "Description")]
        public string user_desc { get; set; }

        [Display(Name = "Date d'inscription")]
        public Nullable<DateTime> user_date_subscription { get; set; }

        [Display(Name = "Status")]
        public byte user_status { get; set; }

        public SelectList user_status_list { get; set; }

        public UserModel()
        {
            using (var context = new UserRepository())
            {
                user_status_list = new SelectList(StatusHelper.GetList<UserStatus>(), "Item2", "Item1");
            }
        }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Pseudo")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Se souvenir de moi?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [Display(Name = "Pseudo")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Le champ {0} est requis.")]
        [StringLength(100, ErrorMessage = "Le {0} doit être composé d'au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation mot de passe")]
        // [Compare("Password", ErrorMessage = "Le mot de passe et la confirmation ne sont pas identique.")]
        public string ConfirmPassword { get; set; }
    }
}