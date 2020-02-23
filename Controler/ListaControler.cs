using ListaView.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListaView.Controler
{
    public class ListaControler
    {
        private readonly SqlConnection _conn;
        public ListaControler()
        {
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConn"].ToString());
        }
        public void InserirProduto(string produto, int quantidade)
        {
            var lista = new ListaModel()
            {
                Produto = produto,
                Quantidade = quantidade
            };
            _conn.Open();
            var trans = _conn.BeginTransaction();
            try
            {
                new ListaDAO(_conn, trans).InserirProduto(lista);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }
        public List<ListaModel> RetornarProduto()
        {
            _conn.Open();
            var trans = _conn.BeginTransaction();
            try
            {
                var lista = new ListaDAO(_conn, trans).RetornaLista();
                trans.Commit();
                return lista;               
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }
        public void ExcluirProduto(int id)
        {
            _conn.Open();
            var trans = _conn.BeginTransaction();
            try
            {
                new ListaDAO(_conn, trans).RemoverProduto(id);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }
        public bool ConfereProduto(string produto, int quantidade)
        {
            _conn.Open();
            var trans = _conn.BeginTransaction();
            try
            {
                bool validaProduto = new ListaDAO(_conn, trans).ConsultarLista(produto, quantidade);
                trans.Commit();     
                return validaProduto;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
