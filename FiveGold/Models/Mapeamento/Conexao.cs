using FiveGold.Properties;
using System.Data.SqlClient;

namespace FiveGold.Models.Mapeamento
{
    public class Conexao
    {
        public SqlConnection conn = null;

        public Conexao()
        {
            conn = new SqlConnection(Settings.Default.strConexao);
        }
    }
}