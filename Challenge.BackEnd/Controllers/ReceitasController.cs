using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Challenge.BackEnd.Data;
using Challenge.BackEnd.Models;
using Challenge.BackEnd.Repositories;

namespace Challenge.BackEnd.Controllers
{
    [ApiController]
    [Route("v1/receitas")]
    public class ReceitasController : ControllerBase
    {
        public readonly IReceitaRepository _receitaRepository;

        public ReceitasController(IReceitaRepository receitaRepository)
        {
            _receitaRepository = receitaRepository;
        }

        [HttpGet("getAll")]
        public async Task<IEnumerable<Receita>> Get()
        {
            return await _receitaRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receita>> Get(int id)
        {
            return await _receitaRepository.Get(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Receita>> Get([FromQuery] string descricao = "")
        {
            return _receitaRepository.Get(descricao);
        }

        [HttpGet("{ano}/{mes}")]
        public async Task<ActionResult<Receita>> Get(int ano, int mes)
        {
            if (ModelState.IsValid)
                return Ok(_receitaRepository.Get(ano, mes));

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Receita>> Create([FromBody] Receita receita)
        {
            if (receita.valor <= 0)
                 return BadRequest($"O valor informado é invalido, ele deve ser maior que 0.");
            
            DateTime minDate = DateTime.Now;
            if (receita.Data <= minDate)
                return BadRequest($"A data informada é incorreta, ela deve ser posterior a data: {minDate}");

            var listaReceitas = await _receitaRepository.Get();
            var mes = DateTime.Now.Month;
            foreach(var r in listaReceitas)
            {
                if(r.Descricao == receita.Descricao && receita.Data.Month == mes)
                {
                    return BadRequest($"Já existe uma receita com a descrição: {receita.Descricao} no mês {mes}");
                }
            }
            
            return await _receitaRepository.Create(receita);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Receita>> Delete(int id)
        {
            var receita = await _receitaRepository.Get(id);
            if (receita == null)
                return NotFound();

            await _receitaRepository.Delete(id);
            return NoContent();
            

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Receita>> Update(int id, [FromBody] Receita receita)
        {
            if(id != receita.Id)
                return BadRequest();

            if (receita.valor <= 0)
                return BadRequest($"O valor informado é invalido, ele deve ser maior que 0.");

            DateTime minDate = DateTime.Now;
            if (receita.Data <= minDate)
                return BadRequest($"A data informada é incorreta, ela deve ser posterior a data: {minDate}");

            var listaReceitas = await _receitaRepository.Get();
            var mes = DateTime.Now.Month;
            foreach (var r in listaReceitas)
            {
                if (r.Descricao == receita.Descricao && receita.Data.Month == mes)
                {
                    return BadRequest($"Já existe uma receita com a descrição: {receita.Descricao} no mês {mes}");
                }
            }

            await _receitaRepository.Update(receita);
            return NoContent();

        }
    }
}
