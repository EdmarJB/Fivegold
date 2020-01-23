using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace FiveGold.Models.Mapeamento
{
    public class Bandeira
    {
        [Key]
        [Display(Name = "Código Bandeira")]
        public int IDBandeira { get; set; }

        [Display(Name = "Bandeira")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public String Nome { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [NotMapped]
        [Display(Name = "Escolher Arquivo")]
        public HttpPostedFileBase LogoFile { get; set; }

        public virtual ICollection<CartaoCredito> CartaoCredito { get; set; }
    }
}