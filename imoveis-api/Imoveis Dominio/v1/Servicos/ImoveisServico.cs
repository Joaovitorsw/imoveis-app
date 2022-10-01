using GerenciamentoComercio_Domain.Utils.APIMessage;
using Imoveis_Dominio.v1.Dto_s;
using Imoveis_Dominio.v1.Dto_s.Imoveis;
using Imoveis_Dominio.v1.Interfaces;
using Imoveis_Infraestrutura.Entities;
using Imoveis_Infraestrutura.v1.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Imoveis_Dominio.v1.Servicos
{
    public class ImoveisServico : IImoveisServico
    {
        private readonly IImoveisRepositorio _imoveisRepositorio;
        private readonly IConfiguration _configuration;

        public ImoveisServico(IImoveisRepositorio imoveisRepositorio,
            IConfiguration configuration)
        {
            _imoveisRepositorio = imoveisRepositorio;
            _configuration = configuration;
        }

        public async Task<APIMessage> BuscarImoveisAPI()
        {
            var json = new ChamadaAPIRequest
            {
                City = "Austin",
                State = "TX"
            };

            RestClient client = new RestClient(_configuration.GetValue<string>("endPoint:APIImoveis"));

            RestRequest request = new RestRequest("saleListings");

            request.AddParameter("application/json", JsonConvert.SerializeObject(json), ParameterType.RequestBody);

            request.AddHeader("x-rapidapi-host", "realty-mole-property-api.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "SIGN-UP-FOR-KEY");

            request.AddParameter("application/x-www-form-urlencoded;charset=UTF-8", ParameterType.HttpHeader);

            var response = await client.GetAsync(request);

            return new APIMessage(HttpStatusCode.OK, response);
        }

        public async Task<APIMessage> BuscarImoveis()
        {
            List<ImoveisResponse> imoveis = await _imoveisRepositorio.BuscarImoveis();

            if (!imoveis.Any())
            {
                return new APIMessage(HttpStatusCode.NotFound, "Nenhum imóvel foi encontrado.");
            }

            return new APIMessage(HttpStatusCode.OK, imoveis);
        }

        public async Task<APIMessage> BuscarImovelPorId(int id)
        {
            ImovelPorIdResponse imovel = await _imoveisRepositorio.BuscarImovelPorId(id);

            if (imovel == null)
            {
                return new APIMessage(HttpStatusCode.NotFound, "Imovel não encontrado.");
            }

            return new APIMessage(HttpStatusCode.OK, imovel);
        }

        public async Task<APIMessage> AdicionarImovel(ImoveisRequest request)
        {
            if (request.Valor <= 0)
            {
                return new APIMessage(HttpStatusCode.BadRequest, "O valor deve ser maior que 0.");
            }

            var novoImovel = new Imoveis
            {
                Endereco = request.Endereco,
                Tipo = request.Tipo,
                TipoContrato = request.TipoContrato,
                Valor = request.Valor
            };

            await _imoveisRepositorio.AdicionarImovel(novoImovel);

            return new APIMessage(HttpStatusCode.OK, "Imovel cadastrado com sucesso.");
        }

        public async Task<APIMessage> AtualizarImovel(ImoveisRequest request, int id)
        {
            Imoveis imovel = await _imoveisRepositorio.BuscarImovelPorIdObjeto(id);

            if (imovel == null)
            {
                return new APIMessage(HttpStatusCode.NotFound, "Imovel não encontrado.");
            }

            imovel.Endereco = request.Endereco;
            imovel.Tipo = request.Tipo;
            imovel.TipoContrato = request.TipoContrato;
            imovel.Valor = request.Valor;

            await _imoveisRepositorio.AtualizarImovel(imovel);

            return new APIMessage(HttpStatusCode.OK, "Imovel atualizado com sucesso.");
        }

        public async Task<APIMessage> RemoverImovel(int id)
        {
            Imoveis imovel = await _imoveisRepositorio.BuscarImovelPorIdObjeto(id);

            if (imovel == null)
            {
                return new APIMessage(HttpStatusCode.NotFound, "Imovel não encontrado.");
            }

            await _imoveisRepositorio.RemoverImovel(imovel);

            return new APIMessage(HttpStatusCode.OK, "Imovel removido com sucesso.");
        }
    }
}
