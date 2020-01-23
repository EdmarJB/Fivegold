using System;
using System.ComponentModel.DataAnnotations;

namespace FiveGold.Models.Mapeamento
{
    public class PagarFatura
    {
        [Key]
        public int Id { get; set; }
        public Decimal Valor { get; set; }
        [Display(Name = "Conta")]
        public int IDConta { get; set; }
        public int IDCartao { get; set; }
        public int IDCatDespesa { get; set; }
        public DateTime DataInicial { get; set; }


        public virtual Conta Conta { get; set; }
        public virtual CatDespesa CatDespesa { get; set; }
    }
}