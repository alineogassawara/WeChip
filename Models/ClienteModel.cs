using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeChip.Models
{
    public class ClienteModel
    {
        [Key]
        public int IdCliente { get; set; }

        [Required, MaxLength(128)]
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Cpf { get; set; }

        public double? Credito { get; set; }

        public string Telefone { get; set; }

        public ICollection<EnderecoModel> Endereco { get; set; }

        public ICollection<OfertaModel> Ofertas { get; set; }
    }
}