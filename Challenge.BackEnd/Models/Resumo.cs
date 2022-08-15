using Challenge.BackEnd.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Challenge.BackEnd.Models
{
    public class Resumo
    {
        public float TotalReceitas { get; set; }
        public float TotalDespesas { get; set; }
        public float Saldo { get; set; }
        public string DespesasPorCategoria { get; set; }

        private DataContext _context;
        public Resumo GeradorResumo(DataContext context,DateTime data)
        {
            _context = context;
            Resumo resumo = new Resumo();
            resumo.TotalReceitas = GerarTotalReceitas(data);
            resumo.TotalDespesas = GerarTotalDespesas(data);
            resumo.Saldo = GerarSaldo(data);
            resumo.DespesasPorCategoria = GerarDespesasPorCategoria(data);
            return resumo;
          
        }

        private float GerarTotalReceitas(DateTime data)
        {
            List<Receita> receitas = _context.Receitas.Where(r => r.Data.Year == data.Year && r.Data.Month == data.Month).ToList();
            
            return receitas.Sum(receita => receita.valor);
        }

        private float GerarTotalDespesas(DateTime data)
        {
            List<Despesa> despesas = _context.Despesas.Where(r => r.Data.Year == data.Year && r.Data.Month == data.Month).ToList();

            return despesas.Sum(despesa => despesa.valor);
        }

        private float GerarSaldo(DateTime data)
        {
            var receita = GerarTotalReceitas(data);
            var despesa = GerarTotalDespesas(data);
            return receita - despesa;
        }

        private string GerarDespesasPorCategoria(DateTime data)
        {          
            List<Despesa> despesas = _context.Despesas.Where(r => r.Data.Year == data.Year && r.Data.Month == data.Month).ToList();
              
            var outras = despesas.Where(d => d.Categoria == Categoria.Outras).Sum(d => d.valor);
            var alimentacao = despesas.Where(d => d.Categoria == Categoria.Alimentacao).Sum(d => d.valor);
            var saude = despesas.Where(d => d.Categoria == Categoria.Saude).Sum(d => d.valor);
            var moradia = despesas.Where(d => d.Categoria == Categoria.Moradia).Sum(d => d.valor);
            var transporte = despesas.Where(d => d.Categoria == Categoria.Transporte).Sum(d => d.valor);
            var educacao = despesas.Where(d => d.Categoria == Categoria.Educacao).Sum(d => d.valor);
            var lazer = despesas.Where(d => d.Categoria == Categoria.Lazer).Sum(d => d.valor);
            var imprevisto = despesas.Where(d => d.Categoria == Categoria.Imprevisto).Sum(d => d.valor);

            return $" Outros: {outras} | Alimentacao: {alimentacao} | Saude: {saude} | Moradia: {moradia} | Transporte: {transporte} | Educação: {educacao} | Lazer: {lazer} | Imprevisto: {imprevisto}";
        }

    }
}
