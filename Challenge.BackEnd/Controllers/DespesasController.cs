using Challenge.BackEnd.Models;
using Challenge.BackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.BackEnd.Controllers
{
    [ApiController]
    [Route("v1/despesas")]
    public class DespesasController : ControllerBase
    {
        public readonly IDespesaRepository _despesasRepository;

        public DespesasController(IDespesaRepository despesaRepository)
        {
            _despesasRepository = despesaRepository;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IEnumerable<Despesa>> Get()
        {
            return await _despesasRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Despesa>> Get(int id)
        {
            return await _despesasRepository.Get(id);
        }

        [HttpGet]
        public async Task<ActionResult<Despesa>> Get([FromQuery] string descricao)
        {
            return Ok( _despesasRepository.Get(descricao));
        }

        [HttpGet("{ano}/{mes}")]
        public async Task<ActionResult<Despesa>> Get(int ano, int mes)
        {
            if (ModelState.IsValid)
            {
                return Ok( _despesasRepository.Get(ano, mes));
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Despesa>> Create([FromBody] Despesa despesa)
        {
            if (despesa.valor <= 0)
                 return BadRequest($"O valor informado é invalido, ele deve ser maior que 0.");
            
            DateTime minDate = DateTime.Now;
            if (despesa.Data <= minDate)
                return BadRequest($"A data informada é incorreta, ela deve ser posterior a data: {minDate}");

            var listaReceitas = await _despesasRepository.Get();
            var mes = DateTime.Now.Month;
            foreach(var r in listaReceitas)
            {
                if(r.Descricao == despesa.Descricao && despesa.Data.Month == mes)
                {
                    return BadRequest($"Já existe uma despesa com a descrição: {despesa.Descricao} no mês {mes}");
                }
            }          
            
            return await _despesasRepository.Create(despesa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Despesa>> Delete(int id)
        {
            var receita = await _despesasRepository.Get(id);
            if (receita == null)
                return NotFound();

            await _despesasRepository.Delete(id);
            return NoContent();
            

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Despesa>> Update(int id, [FromBody] Despesa despesa)
        {
            if(id != despesa.Id)
                return BadRequest();

            if (despesa.valor <= 0)
                return BadRequest($"O valor informado é invalido, ele deve ser maior que 0.");

            DateTime minDate = DateTime.Now;
            if (despesa.Data <= minDate)
                return BadRequest($"A data informada é incorreta, ela deve ser posterior a data: {minDate}");

            var listaReceitas = await _despesasRepository.Get();
            var mes = DateTime.Now.Month;
            foreach (var r in listaReceitas)
            {
                if (r.Descricao == despesa.Descricao && despesa.Data.Month == mes)
                {
                    return BadRequest($"Já existe uma despesa com a descrição: {despesa.Descricao} no mês {mes}");
                }
            }

            await _despesasRepository.Update(despesa);
            return NoContent();

        }
    }
}
