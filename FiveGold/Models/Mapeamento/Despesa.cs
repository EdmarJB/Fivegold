using System;
using System.ComponentModel.DataAnnotations;

namespace FiveGold.Models.Mapeamento
{
    public class Despesa
    {
        [Key]
        [Display(Name = "Código Despesa")]
        public int IDDespesa { get; set; }

        [Display(Name = "Código Usuário")]
        public String UserId { get; set; }

        public Decimal Valor { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime Data { get; set; }

        [Display(Name = "Descrição")]
        public String Descricao { get; set; }
        public Boolean Pago { get; set; }

        [Display(Name = "Tipo Despesa")]
        public int IDCatDespesa { get; set; }

        [Display(Name = "Conta")]
        public int IDConta { get; set; }

        public virtual CatDespesa CatDespesa { get; set; }
        public virtual Conta Conta { get; set; }
    }
}