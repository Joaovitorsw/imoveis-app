using System;
using System.Collections.Generic;

namespace Imoveis_Infraestrutura.Entities
{
    public partial class Imoveis
    {
        public int Id { get; set; }
        public string Endereco { get; set; } = null!;
        public string? Tipo { get; set; }
        public string? TipoContrato { get; set; }
        public decimal? Valor { get; set; }
    }
}
