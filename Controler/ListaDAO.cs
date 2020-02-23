using ListaView.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaView.Controler
{
    public class ListaDAO
    {
        public readonly SqlConnection _conn;
        public readonly SqlTransaction _trans;
        public ListaDAO(SqlConnection conn, SqlTransaction trans)
        {
            _conn = conn;
            _trans = trans;
        } 
        public void InserirProduto (ListaModel lista)
        {
            string sqlInsert = @"INSERT INTO LISTA(PRODUTO, QUANTIDADE) VALUES(@produto, @quantidade)";
            SqlCommand cmd = new SqlCommand(sqlInsert, _conn, _trans);
            cmd.Parameters.AddWithValue("@produto", lista.Produto);
            cmd.Parameters.AddWithValue("@quantidade", lista.Quantidade);
            cmd.ExecuteNonQuery();
        }
        public List<ListaModel> RetornaLista()
        {
            string sql = @"SELECT ID, PRODUTO, QUANTIDADE FROM LISTA";
            SqlCommand cmd = new SqlCommand(sql, _conn, _trans);
            var lista = new List<ListaModel>();
            using(SqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    ListaModel l = new ListaModel()
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        Produto = reader["PRODUTO"].ToString(),
                        Quantidade = Convert.ToInt32(reader["QUANTIDADE"])
                    };
                    lista.Add(l);
                }
            return lista;
        }
        public void RemoverProduto(int id)
        {
            string sqlDelete = $@"DELETE FROM LISTA WHERE ID = {id}";
            SqlCommand cmd = new SqlCommand(sqlDelete, _conn, _trans);
            cmd.ExecuteNonQuery();
        }
        public bool ConsultarLista(string produto, int quantidade)
        {
            string sql = @"SELECT PRODUTO, QUANTIDADE FROM LISTA WHERE PRODUTO = @produto AND QUANTIDADE = @quantidade";
            SqlCommand cmd = new SqlCommand(sql, _conn, _trans);
            cmd.Parameters.AddWithValue("@produto", produto);
            cmd.Parameters.AddWithValue("@quantidade", quantidade);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                return reader.HasRows;
            }
                
        }
    }
}       
        
        
            
        
    

