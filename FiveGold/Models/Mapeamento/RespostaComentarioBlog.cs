using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiveGold.Models.Mapeamento
{
    public class RespostaComentarioBlog
    {
        [Key]
        [Display(Name = "Código Resposta")]
        public int IDResposta { get; set; }

        [Display(Name = "Código Comentário")]
        public int IDComentario { get; set; }

        [Display(Name = "Nome do Usuário")]
        [MaxLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public String NomeUsuario { get; set; }

        [Display(Name = "Data da Resposta")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataResposta { get; set; }

        [Display(Name = "Resposta")]
        [MaxLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public String TextoResposta { get; set; }
    }
}