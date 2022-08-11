using Challenge.BackEnd.Models;

namespace Challenge.BackEnd.Repositories
{
    public interface IDespesaRepository
    {
        Task<IEnumerable<Despesa>> Get();

        Task<Despesa> Get(int id);
        
        IQueryable<Despesa> Get(string descricao);

        IQueryable<Despesa> Get(int ano, int mes);

        Task<Despesa> Create(Despesa Despesa);

        Task Update(Despesa despesa);

        Task Delete(int id);

    }
}
