using Common.BaseClass;
using Common.Tools;
using DataAccess.Repository;
using DataAccess.Status;
using System;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public int StatusSelected { get; set; }
        public SelectList StatusList { get; set; }

        public UserModel()
        {
            StatusList = new SelectList(StatusHelper.GetList<UserStatus>(), "Key", "Value");
        }

        public static new T_User ModelToEntity(UserModel model)
        {
            T_User entity = BaseModel<UserModel, T_User>.ModelToEntity(model);
            entity.user_status = (byte)model.StatusSelected;

            return entity;
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
}