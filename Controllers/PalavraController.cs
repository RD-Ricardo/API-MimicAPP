using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimicApp_Api.Database;
using MimicApp_Api.Dto;
using MimicApp_Api.Helpers;
using MimicApp_Api.Models;
using MimicApp_Api.Repository.Interfaces;
using Newtonsoft.Json;

namespace MimicApp_Api.Controllers
{

    [ApiController]
    [Route("{controller}")]
    public class PalavraController : Controller
    {
        private readonly IPalavraRepository _reposiory;

        private readonly IMapper _mapper;
        public PalavraController(IPalavraRepository repository, IMapper mapper)
        {
            _reposiory = repository;
            _mapper = mapper;
        }

        [HttpGet("", Name ="ObterTodas")]
        public  async Task<IActionResult> GetAllWord([FromQuery]PalavraUrlQuery query)
        {
           var wordAll = await _reposiory.GetallPalavrasAsync(query);

           if(wordAll.Result.Count() == 0)
           {
               return NotFound();
           }

          

           var lista  = _mapper.Map<PaginationList<Palavra>, PaginationList<PalavraDTO>>(wordAll);

            foreach(var palavra in lista.Result)
            {
                palavra.Link.Add(new LInkDTO("self",Url.Link("ObterPalavra", new { id = palavra.Id }),"GET"));
                // palavra.Link.Add(new LInkDTO("self",Url.Link("AtualizarPalavra", new { id = palavra.Id }),"PUT"));
                // palavra.Link.Add(new LInkDTO("self",Url.Link("ObterPalavra", new { id = palavra.Id }),"DELETE"));
            }

            lista.LInks.Add(new LInkDTO("self",Url.Link("ObterTodas", query),"GET"));

            if(wordAll.Paginacao != null)
            {
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(wordAll.Paginacao));


                if(query.PagNumero + 1 <= wordAll.Paginacao.TotalPaginas)
                {
                    var queryString = new PalavraUrlQuery()
                    {
                        PagNumero = query.PagNumero + 1,
                        PagRegistros =  query.PagRegistros
                    };

                    lista.LInks.Add(new LInkDTO("next",Url.Link("ObterTodas", queryString ),"GET"));
                }
                if(query.PagNumero - 1 > 0)
                {
                    var queryString = new PalavraUrlQuery()
                    {
                        PagNumero = query.PagNumero - 1,
                        PagRegistros =  query.PagRegistros
                    };

                    lista.LInks.Add(new LInkDTO("previ",Url.Link("ObterTodas", queryString ),"GET"));
                }
              
            }

            return Ok(lista);            
        }

        [HttpGet("{id}", Name = "ObterPalavra")]
        public async Task<IActionResult> GetByIdWord(int id)
        {
           var word = await _reposiory.GetByIdPalavraAsync(id);

           if(word == null)
           {
               return BadRequest("Palavra não encontrada");
           }

            var palavraDTO = _mapper.Map<Palavra, PalavraDTO>(word);
            palavraDTO.Link.Add(new LInkDTO("self",Url.Link("ObterPalavra", new { id = palavraDTO.Id }),"GET"));
            palavraDTO.Link.Add(new LInkDTO("update",Url.Link("AtualizarPalavra", new { id = palavraDTO.Id }),"PUT"));
            palavraDTO.Link.Add(new LInkDTO("delete",Url.Link("ObterPalavra", new { id = palavraDTO.Id }),"DELETE"));


           return Ok(palavraDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWord([FromBody] Palavra model)
        {

            if(ModelState.IsValid)
            {
               await _reposiory.CreatePalavraAsync(model);
               return Created($"/Palavra/{model.Id}", model);
            }
            
            return BadRequest("Erro na criação da Palavra");
        }

        [HttpPut("{id}", Name = "AtualizarPalavra")]
        public async Task<IActionResult> UpdateWord(int id, [FromBody]Palavra model)
        {
          
              var wordId =  await _reposiory.GetByIdPalavraAsync(id);

              if(wordId ==  null)
              {
                  return BadRequest($"Id : {id} não encotrado falha");
              }
            

            model = wordId;
            await _reposiory.UpdatePalavraAsync(model);
            return Ok("Atualizado com sucesso");
            
        }
    }
}