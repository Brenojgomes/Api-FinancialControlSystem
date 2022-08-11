using Challenge.BackEnd.Data;
using Challenge.BackEnd.Models;

namespace Challenge.BackEnd.Repositories
{
    public class ResumoRepository : IResumoRepository
    {

        private DataContext _context;
        public ResumoRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Resumo> Get(int ano, int mes)
        {
            DateTime data = new DateTime(ano, mes, 1);
            Resumo resumo = new Resumo().GeradorResumo(_context, data);
            return  resumo;
        }
    }
}
