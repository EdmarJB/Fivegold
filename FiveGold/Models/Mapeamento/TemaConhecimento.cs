using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FiveGold.Models.Mapeamento
{
    public class TemaConhecimento
    {
        [Key]
        [Display(Name = "Código Tema")]
        public int IDTema { get; set; }

        [Display(Name = "Tema")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public String Nome { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.ImageUrl)]
        public String Logo { get; set; }

        [NotMapped]
        [Display(Name = "Escolher Arquivo")]
        public HttpPostedFileBase LogoFile { get; set; }

        public virtual ICollection<Conhecimento> Conhecimento { get; set; }
    }
}