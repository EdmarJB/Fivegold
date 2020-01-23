using System;
using System.ComponentModel.DataAnnotations;

namespace FiveGold.Models.Mapeamento
{
    public class Transferencia
    {
        [Key]
        [Display(Name = "Código Transferência")]
        public int IdTransferencia { get; set; }

        [Display(Name = "Código Usuário")]
        public String UserId { get; set; }

        [Display(Name = "Conta de Origem")]
        public int IDOrigem { get; set; }

        [Display(Name = "Conta de Destino")]
        public int IDDestino { get; set; }
        public Decimal Valor { get; set; }

        [Display(Name = "Data da Transferência")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataTransferencia { get; set; }
    }
}