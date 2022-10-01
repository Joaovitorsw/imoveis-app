using GerenciamentoComercio_Domain.Utils.APIMessage;
using Imoveis_Dominio.v1.Dto_s.Imoveis;
using Imoveis_Dominio.v1.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Imoveis_API.v1.Controllers
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("v{version:apiVersion}/imoveis/")]
    [ApiController]
    public class ImoveisController : ControllerBase
    {
        private readonly IImoveisServico _imoveisServico;

        public ImoveisController(IImoveisServico imoveisServico)
        {
            _imoveisServico = imoveisServico;
        }

        [HttpGet("chamada-api")]
        public async Task<IActionResult> BuscarImoveisAPI()
        {
            APIMessage imoveis = await _imoveisServico.BuscarImoveisAPI();

            return StatusCode((int)imoveis.StatusCode, imoveis.ContentObj);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarImoveis()
        {
            APIMessage imoveis = await _imoveisServico.BuscarImoveis();

            return StatusCode((int)imoveis.StatusCode, imoveis.ContentObj);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarImovelPorId(int id)
        {
            APIMessage imovel = await _imoveisServico.BuscarImovelPorId(id);

            return StatusCode((int)imovel.StatusCode, imovel.ContentObj);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarImovel(ImoveisRequest request)
        {
            APIMessage novoImovel = await _imoveisServico.AdicionarImovel(request);

            return StatusCode((int)novoImovel.StatusCode, novoImovel.ContentObj);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarImovel(int id, ImoveisRequest request)
        {
            APIMessage atualizar = await _imoveisServico.AtualizarImovel(request, id);

            return StatusCode((int)atualizar.StatusCode, atualizar.ContentObj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverImovel(int id)
        {
            APIMessage remover = await _imoveisServico.RemoverImovel(id);

            return StatusCode((int)remover.StatusCode, remover.ContentObj);
        }
    }
}
