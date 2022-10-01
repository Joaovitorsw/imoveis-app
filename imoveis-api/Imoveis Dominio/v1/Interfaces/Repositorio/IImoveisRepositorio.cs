using Imoveis_Dominio.v1.Dto_s;
using Imoveis_Infraestrutura.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imoveis_Infraestrutura.v1.Interfaces
{
    public interface IImoveisRepositorio
    {
        Task <List<ImoveisResponse>> BuscarImoveis();
        Task<ImovelPorIdResponse> BuscarImovelPorId(int id);
        Task AdicionarImovel(Imoveis imovel);
        Task<Imoveis> BuscarImovelPorIdObjeto(int id);
        Task AtualizarImovel(Imoveis imovel);
        Task RemoverImovel(Imoveis imovel);
    }
}
