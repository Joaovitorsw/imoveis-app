using Imoveis_Dominio.v1.Dto_s;
using Imoveis_Infraestrutura.Entities;
using Imoveis_Infraestrutura.v1.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imoveis_Infraestrutura.v1.Repositorios
{
    public class ImoveisRepositorio : IImoveisRepositorio
    {
        private readonly LOCADORA_IMOVEISContext _context;

        public ImoveisRepositorio(LOCADORA_IMOVEISContext context)
        {
            _context = context;
        }

        public async Task<List<ImoveisResponse>> BuscarImoveis()
        {
            return await _context.Imoveis
                .Select(x => new ImoveisResponse
                {
                    Endereco = x.Endereco,
                    Id = x.Id,
                    Tipo = x.Tipo,
                    TipoContrato = x.TipoContrato,
                    Valor = x.Valor,
                }).ToListAsync();
        }

        public async Task<ImovelPorIdResponse> BuscarImovelPorId(int id)
        {
            return await _context.Imoveis
                .Where(x => x.Id == id)
                .Select(x => new ImovelPorIdResponse
                {
                    Endereco = x.Endereco,
                    Tipo = x.Tipo,
                    TipoContrato = x.TipoContrato,
                    Valor = x.Valor,
                }).FirstOrDefaultAsync();
        }

        public async Task<Imoveis> BuscarImovelPorIdObjeto(int id)
        {
            return await _context.Imoveis
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AdicionarImovel(Imoveis imovel)
        {
            await _context.AddAsync(imovel);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarImovel(Imoveis imovel)
        {
            _context.Update(imovel);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverImovel(Imoveis imovel)
        {
            _context.Remove(imovel);
            await _context.SaveChangesAsync();
        }
    }
}
