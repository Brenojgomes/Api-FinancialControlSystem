using Challenge.BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.BackEnd.Repositories
{
    public interface IReceitaRepository
    {
        Task<IEnumerable<Receita>> Get();

        Task<Receita> Get(int id);
        
        IQueryable<Receita> Get([FromQuery] string descricao = "");

        IQueryable<Receita> Get(int ano, int mes);

        Task<Receita> Create(Receita receita);

        Task Update(Receita receita);

        Task Delete(int id);
    }
}
