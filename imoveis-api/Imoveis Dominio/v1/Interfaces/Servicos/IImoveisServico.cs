using GerenciamentoComercio_Domain.Utils.APIMessage;
using Imoveis_Dominio.v1.Dto_s;
using Imoveis_Dominio.v1.Dto_s.Imoveis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imoveis_Dominio.v1.Interfaces
{
    public interface IImoveisServico 
    {
        Task<APIMessage> BuscarImoveis();
        Task<APIMessage> BuscarImoveisAPI();
        Task<APIMessage> BuscarImovelPorId(int id);
        Task<APIMessage> AdicionarImovel(ImoveisRequest request);
        Task<APIMessage> AtualizarImovel(ImoveisRequest request, int id);
        Task<APIMessage> RemoverImovel(int id);
    }
}
