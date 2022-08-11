using Challenge.BackEnd.Data;
using Challenge.BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Challenge.BackEnd.Repositories
{
    public class ReceitaRepository : IReceitaRepository
    {
        private readonly DataContext _context;
        public ReceitaRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Receita> Create(Receita receita)
        {
            _context.Receitas.Add(receita);
            await _context.SaveChangesAsync();
            return receita;
        }

        public async Task Delete(int id)
        {
            Receita receita = _context.Receitas.FirstOrDefault(x => x.Id == id);
            _context.Receitas.Remove(receita);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Receita>> Get()
        {
            return await _context.Receitas.ToListAsync();
        }

        public async Task<Receita> Get(int id)
        {
            return await _context.Receitas.FindAsync(id);
        }

        public IQueryable<Receita> Get([FromQuery] string descricao)
        {
            return _context.Receitas.Where(receita => receita.Descricao.Contains(descricao)).AsQueryable();
        }

        public IQueryable<Receita> Get(int ano, int mes)
        {
            DateTime data = new DateTime(ano, mes, 1);

            return _context.Receitas.Where(
                r => r.Data.Year == data.Year
                && r.Data.Month == data.Month);
        }
        public async Task Update(Receita receita)
        {
            _context.Entry(receita).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}
