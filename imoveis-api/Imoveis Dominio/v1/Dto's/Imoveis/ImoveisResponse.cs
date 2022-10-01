using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imoveis_Dominio.v1.Dto_s
{
    public class ImoveisResponse
    {
        public int Id { get; set; }
        public string Endereco { get; set; } 
        public string? Tipo { get; set; }
        public string? TipoContrato { get; set; }
        public decimal? Valor { get; set; }
    }
}
