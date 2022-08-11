using Challenge.BackEnd.Models;
using Challenge.BackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.BackEnd.Controllers
{
    [ApiController]
    [Route("v1/resumo")]
    public class ResumoController : Controller
    {
        private readonly IResumoRepository _resumoRepository;
        public ResumoController(IResumoRepository resumoRepository)
        {
            _resumoRepository = resumoRepository;
        }

        [HttpGet("{ano}/{mes}")]
        public async Task<Resumo> Get (int ano, int mes)
        {
            return await _resumoRepository.Get(ano, mes);
        }
    }
}
