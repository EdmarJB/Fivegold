using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiveGold.Models.Mapeamento
{
    public class CatConta
    {
        [Key]
        [Display(Name = "Código Categoria")]
        public int IDCatConta { get; set; }

        [Display(Name = "Tipo")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public String Nome { get; set; }

        public virtual ICollection<Conta> Conta { get; set; }
    }
}