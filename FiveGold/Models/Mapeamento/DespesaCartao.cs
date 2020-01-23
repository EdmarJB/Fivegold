using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiveGold.Models.Mapeamento
{
    public class DespesaCartao
    {
        [Key]
        [Display(Name = "Código Despesa Cartão")]
        public int IDDespCartao { get; set; }

        [Display(Name = "Código Usuário")]
        public String UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public Decimal Valor { get; set; }

        [Display(Name = "Data Compra")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataCompra { get; set; }

        [MaxLength(300, ErrorMessage = "Máximo 300 caracteres")]
        [Display(Name = "Descrição")]
        public String Descricao { get; set; }

        [Display(Name = "Primeira Parcela")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataPrimeiraParcela { get; set; }

        [Display(Name = "Última Parcela")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataUltimaParcela { get; set; }

        [Display(Name = "Qtd. Parcelas")]
        public int QtdParcela { get; set; }

        [Display(Name = "Valor Parcela")]
        public Double ValorParcela { get; set; }

        [NotMapped]
        public int Parcela { get; set; }
        public Boolean Pago { get; set; }

        [Display(Name = "Tipo Despesa")]
        public int IDCatDespesa { get; set; }

        [Display(Name = "Cartão")]
        public int IDCartao { get; set; }

        public virtual CatDespesa CatDespesa { get; set; }

        public virtual CartaoCredito CartaoCredito { get; set; }
    }
}