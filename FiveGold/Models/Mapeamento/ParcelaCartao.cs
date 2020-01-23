using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiveGold.Models.Mapeamento
{
    public class ParcelaCartao
    {
        [Key]
        public int IDParcelacartao { get; set; }
        public int IDCartao { get; set; }
        public DateTime DataParcela { get; set; }
        public Boolean Pago { get; set; }
    }
}