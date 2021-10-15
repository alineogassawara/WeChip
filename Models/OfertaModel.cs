using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeChip.Models
{
    [Table("Oferta")]
    public class OfertaModel
    {
        [Key]
        public int IdOferta { get; set; }

        public DateTime? DataOferta { get; set; }

        public DateTime? DataEntrega { get; set; }

        public double? ValorTotal { get; set; }

        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public ClienteModel Cliente { get; set;}

        public EnderecoModel EnderecoEntrega { get; set; }

        public ICollection<ItemEscolhidoModel> ItemEscolhido { get; set; }
    }
}