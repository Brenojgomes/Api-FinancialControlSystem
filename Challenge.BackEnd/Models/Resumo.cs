using Challenge.BackEnd.Data;
using Microsoft.Data.SqlClient;

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
            float total = 0;
            
            foreach(var receita in receitas)
            {
                total += receita.valor;
            }
            return total;
        }

        private float GerarTotalDespesas(DateTime data)
        {
            List<Despesa> despesas = _context.Despesas.Where(r => r.Data.Year == data.Year && r.Data.Month == data.Month).ToList();
            float total = 0;

            foreach (var despesa in despesas)
            {
                total += despesa.valor;
            }
            return total;
        }

        private float GerarSaldo(DateTime data)
        {
            var receita = GerarTotalReceitas(data);
            var despesa = GerarTotalDespesas(data);
            return receita - despesa;
        }

        private string GerarDespesasPorCategoria(DateTime data)
        {
            DateTime finalDate = data.AddDays(30);

            SqlCommand cmd = new SqlCommand($"select sum(valor) as total, (Categoria) as tipo from Despesas Group By(Categoria) where Data between {data} and {finalDate}");
            cmd.ToString();
            return "";
        }

    }
}
