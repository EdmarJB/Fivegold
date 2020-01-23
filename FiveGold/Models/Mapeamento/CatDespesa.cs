using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiveGold.Models.Mapeamento
{
    public class CatDespesa
    {
        [Key]
        [Display(Name = "Código Categoria")]
        public int IDCatDespesa { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [Display(Name = "Tipo Despesa")]
        public String Nome { get; set; }

        public virtual ICollection<Despesa> Despesa { get; set; }
        public virtual ICollection<PagarFatura> PagarFatura{ get; set; }
    }
}