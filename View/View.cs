using ListaView.Controler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListaView
{
    public partial class View : Form
    {
        int IdGeral = 0;
        public View()
        {
            InitializeComponent();
        }
        public void LimpaCampos()
        {
            txtProduto.Text = "";
            txtQuantidade.Text = "";
        }
        
        public bool ValidaCampos()
        {
            if (string.IsNullOrEmpty(txtProduto.Text) || string.IsNullOrEmpty(txtQuantidade.Text))
            {
                return false;
            }
            return true;
        }

        public void CarregaLista()
        {
            dtLista.DataSource = new ListaControler().RetornarProduto();
            dtLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtLista.Columns[0].Visible = false; //Esconde a coluna ID
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {               
                new ListaControler().InserirProduto(txtProduto.Text, Convert.ToInt32(txtQuantidade.Text));
                MessageBox.Show("Produto incluído na lista!");
                LimpaCampos();
                CarregaLista();
                
            }
            else
            {
                MessageBox.Show("Todos os campos devem ser preenchidos");
            }

        }

        private void dtLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Deseja Remover o produto?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            
                this.IdGeral = Convert.ToInt32(dtLista.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtProduto.Text = dtLista.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtQuantidade.Text = dtLista.Rows[e.RowIndex].Cells[2].Value.ToString();
            
                        
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            new ListaControler().ExcluirProduto(IdGeral);
            MessageBox.Show("Produto removido!");
            IdGeral = 0;
            LimpaCampos();
            CarregaLista();
        }

        private void View_Load(object sender, EventArgs e)
        {
            CarregaLista();
            
        }

    }
}
