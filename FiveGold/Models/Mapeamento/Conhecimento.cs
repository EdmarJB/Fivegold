using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FiveGold.Models.Mapeamento
{
    public class Conhecimento
    {
        [Key]
        [Display(Name = "Código Conhecimento")]
        public int IDConhecimento { get; set; }

        [Display(Name = "Usuário")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public String UserName { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public String NomeCompleto { get; set; }

        [Display(Name = "Data do Post")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataPost { get; set; }

        [Display(Name = "Título")]
        [MaxLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        public String Titulo { get; set; }

        [Display(Name = "Texto")]
        public String Texto { get; set; }

        [Display(Name = "Tema")]
        [Required(ErrorMessage = "O campo é obrigatório")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione um tema")]
        public int IDTema { get; set; }

        [Display(Name = "Validação")]
        public Boolean validacao { get; set; }

        //[Display(Name = "Imagem")]
        //[DataType(DataType.ImageUrl)]
        //public string Imagem { get; set; }

        //[NotMapped]
        //public HttpPostedFileBase ImagemFile { get; set; }

        //[Display(Name = "Categoria conhecimento")]
        //[Required(ErrorMessage = "O campo é obrigatório")]
        //[Range(1, double.MaxValue, ErrorMessage = "Selecione uma categoria")]
        //public int IDCatConhecimento { get; set; }

        //public virtual CatConhecimento CatConhecimento { get; set; }

        public virtual TemaConhecimento TemaConhecimento { get; set; }
    }
}