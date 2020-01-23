using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FiveGold.Models.Mapeamento
{
    public class CatReceita
    {
        [Key]
        [Display(Name = "Código Categoria")]
        public int IDCatReceita { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [Display(Name = "Tipo Receita")]
        public String Nome { get; set; }

        public virtual ICollection<Receita> Receita { get; set; }
    }
}