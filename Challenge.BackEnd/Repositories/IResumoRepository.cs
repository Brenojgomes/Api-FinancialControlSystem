using Challenge.BackEnd.Data;
using Challenge.BackEnd.Models;

namespace Challenge.BackEnd.Repositories
{
    public interface IResumoRepository
    {
        Task<Resumo> Get(int ano, int mes);  
    }
}
