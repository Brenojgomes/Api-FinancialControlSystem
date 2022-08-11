using Challenge.BackEnd.Data;
using Challenge.BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.BackEnd.Repositories
{
    public class DespesaRepository : IDespesaRepository
    {
        private readonly DataContext _context;
        public DespesaRepository(DataContext Context)
        {
            _context = Context;
        }

        public async Task<Despesa> Create(Despesa Despesa)
        {
            _context.Despesas.Add(Despesa);
            await _context.SaveChangesAsync();
            return Despesa;
        }

        public async Task Delete(int id)
        {
            Despesa despesa = _context.Despesas.FirstOrDefault(despesa => despesa.Id == id);
            _context.Despesas.Remove(despesa);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Despesa>> Get()
        {
            return await _context.Despesas.ToListAsync();
        }

        public async Task<Despesa> Get(int id)
        {
            return await _context.Despesas.FindAsync(id);
        }

        public  IQueryable<Despesa> Get(string descricao)
        {
            return  _context.Despesas.Where(d => d.Descricao.Contains(descricao));

        }

        public IQueryable<Despesa> Get (int ano, int mes)
        {         
            DateTime data  =  new DateTime(ano,mes,1);
            
            return _context.Despesas.Where(
                d => d.Data.Year == data.Year
                && d.Data.Month == data.Month);
           
        }

        public async Task Update(Despesa despesa)
        {
            _context.Entry(despesa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
