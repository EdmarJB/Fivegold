using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace FiveGold.Models.Mapeamento
{
    public class Usuario
    {
        [Key]
        public String UserId { get; set; }

        [Required]
        [Display(Name = "Nome Completo")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public String NomeCompleto { get; set; }

        [NotMapped]
        public String Nome => NomeCompleto.Split(' ').FirstOrDefault();

        //NomeCompleto.Split(' ').FirstOrDefault();

        public String NomeUser()
        {
            return Nome;
        }

        [NotMapped]
        public String IdUsuarioLogado => UserId;

        public static Usuario Get
        {
            get
            {
                var user = HttpContext.Current.User;
                if(user == null)
                {
                    //return null;
                }
                else if (String.IsNullOrEmpty(user.Identity.GetUserId()))
                {
                    return null;
                }

                var jUser = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.UserData).Value;

                return JsonConvert.DeserializeObject<Usuario>(jUser);
            }
        }
    }
}