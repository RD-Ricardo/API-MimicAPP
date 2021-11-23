using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MimicApp_Api.Database;
using MimicApp_Api.Helpers;
using MimicApp_Api.Models;
using MimicApp_Api.Repository.Interfaces;

namespace MimicApp_Api.Repository
{
    public class PalavraRepository : IPalavraRepository
    {
        private readonly MimicContext _dbContext;

        public PalavraRepository( MimicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreatePalavraAsync(Palavra model)
        {
            _dbContext.Palavras.Add(model);
            await _dbContext.SaveChangesAsync();
            
        }
        public async Task<PaginationList<Palavra>> GetallPalavrasAsync(PalavraUrlQuery query)
        {
            var  words =  _dbContext.Palavras.AsNoTracking().AsQueryable();

            var lista =  new  PaginationList<Palavra>();

            if(query.Data.HasValue)
            {   
                words =  words.Where(c => c.Criacao > query.Data.Value).AsNoTracking();
            }
            if(query.PagNumero.HasValue)
            {
                var qtdTotalRegistros = words.Count();
                words = words.Skip((query.PagNumero.Value - 1 ) * query.PagRegistros.Value).Take(query.PagRegistros.Value);

                var paginacao = new Paginacao()
                {
                    NumeroPagina = query.PagNumero.Value,
                    RegistroPorPagina = query.PagRegistros.Value,
                    TotalRegistros = qtdTotalRegistros,
                    TotalPaginas = (int)Math.Ceiling( (double)qtdTotalRegistros / query.PagRegistros.Value)
                };


                lista.Paginacao = paginacao;
               
            }
            lista.Result.AddRange(await words.ToListAsync());

            return lista;
        }

        public async Task<Palavra> GetByIdPalavraAsync(int id)
        {
            var word = await _dbContext.Palavras.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if(word != null)
                  return word;

            return null;      
        }

        public async  Task RemovePalavraAsync(int id)
        {
          var result = await GetByIdPalavraAsync(id);

          if(result != null)
          {
            _dbContext.Palavras.Update(result);
            await _dbContext.SaveChangesAsync();
          }
        }

        public async Task UpdatePalavraAsync(Palavra model)
        {
            _dbContext.Palavras.Update(model);
            await _dbContext.SaveChangesAsync();
        }
    }
}