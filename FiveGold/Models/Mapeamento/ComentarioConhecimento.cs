using System;
using System.ComponentModel.DataAnnotations;

namespace FiveGold.Models.Mapeamento
{
    public class ComentarioConhecimento
    {
        [Key]
        [Display(Name = "Código Comentário")]
        public int IDComentario { get; set; }

        [Display(Name = "Código Post")]
        public int IDConhecimento { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public String NomeCompleto { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataComentario { get; set; }

        [MaxLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public String TextoComentario { get; set; }
    }
}