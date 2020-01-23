using System;
using System.ComponentModel.DataAnnotations;

namespace FiveGold.Models.Mapeamento
{
    public class Receita
    {
        [Key]
        [Display(Name = "Código Receita")]
        public int IDReceita { get; set; }

        [Display(Name = "Código Usuário")]
        public String UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public Decimal Valor { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime Data { get; set; }

        [MaxLength(300, ErrorMessage = "Máximo 300 caracteres")]
        [Display(Name = "Descrição")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public String Descricao { get; set; }

        public Boolean Pago { get; set; }

        [Display(Name = "Tipo Receita")]
        public int IDCatReceita { get; set; }

        [Display(Name = "Conta")]
        public int IDConta { get; set; }

        public virtual Conta Conta { get; set; }

        public virtual CatReceita CatReceita { get; set; }
    }
}