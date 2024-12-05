using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TeleBerço.DsProdutosTableAdapters;
using static TeleBerço.DsProdutos;

namespace TeleBerço
{
    public partial class FrmStock : Form
    {
        public FrmStock()
        {
            InitializeComponent();
        }

        DsStock dsStock = new DsStock();
        DsProdutos dsArtigos = new DsProdutos();
        public DataRow RowSelecionada{ get; private set; }
 

     
        public DataView dataViewStock { get; set; }
        public DataView dataViewMov { get; set; }


        private QuerryProdutosTableAdapter querryProdutosTableAdapter = new QuerryProdutosTableAdapter();

        private void FrmStock_Load(object sender, EventArgs e)
        {
          
            CarregarMovimentos();
            CarregarStockPr();
          
            ConfigurarControles();

        }
        private void ConfigurarControles()
        {
            try
            {
                // Configura os controles de filtro e pesquisa com base no tipo de dados atual
                cbTipoDoc.Items.AddRange(new string[] { "Entrada", "Saida" });
                cbFiltro.Items.Clear();
                cbOrdenar.Items.Clear();
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;

                dGridStock.Columns["Marca"].Visible = false;
                dGridStock.Columns["Categoria"].Visible = false;
                dGridStock.Columns["NomeProduto"].HeaderText = "Produto";
                dGridStock.Columns["CodPr"].HeaderText = "Codigo";
                dGridStock.Columns["NomeMarca"].HeaderText = "Marca";
                dGridStock.Columns["NomeCategoria"].HeaderText = "Categoria";


                //dGridProdutos.Columns["Marcas"].Visible = false;
                //dGridProdutos.Columns["Categorias"].Visible = false;

                //dGridProdutos.Columns["NomeProduto"].Visible = false;
                //dGridProdutos.Columns["PreçoVenda"].Visible = false;
                //dGridProdutos.Columns["PrecoCusto"].Visible = false;

                //dGridStock.Columns["ProdutoID"].Visible = false;

                dgridMovimentos.Columns["ProdutoID"].Visible = false;
                dgridMovimentos.Columns["NomeProduto"].HeaderText = "Produto";


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro nas configurações iniciais: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void AdicionarDetalhesProdutos()
        {
            // Adiciona colunas de Marca e Categoria se não existirem
            try
            {
                foreach (DataRow row in dsStock.StockPr.Rows)
                {
                    // Preenche a coluna Marca
                    if (row["Marca"] != DBNull.Value)
                        row["NomeMarca"] = querryProdutosTableAdapter.NomeMarca((int)row["Marca"]);

                    // Preenche a coluna Categoria
                    if (row["Categoria"] != DBNull.Value)
                        row["NomeCategoria"] = querryProdutosTableAdapter.NomeCategoria(row["Categoria"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Marcas", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CarregarStockPr()
        {
           
            dsStock.CarregarStockPr();
            AdicionarDetalhesProdutos();
            dataViewStock = new DataView(dsStock.StockPr);
            dGridStock.DataSource = dataViewStock;
        }

        private void CarregarMovimentos()
        {
            dsStock.CarregarMovimentos();
            AdicionarDetalhes();
            dataViewMov = new DataView(dsStock.MovimentacoeStock);
            dgridMovimentos.DataSource = dataViewMov;
        }


        private void AplicarFiltroPorDatas(DateTime dataInicio, DateTime dataFim)
        {
            string campoData = cbFiltro.SelectedItem.ToString().Replace(" ", "");
            dataViewMov.RowFilter = $"{campoData} >= '{dataInicio:yyyy-MM-dd}' AND {campoData} <= '{dataFim:yyyy-MM-dd}'";
        }

        private void AplicarFiltroPorData(DateTime data)
        {
            string campoData = cbFiltro.SelectedItem.ToString().Replace(" ", "");
            dataViewMov.RowFilter = $"{campoData} = '{data:yyyy-MM-dd}'";
        }

        private void SelecionarLinhaAtual(DataGridView dataGrid)
        {
            try
            {
                if (dataGrid.CurrentRow != null && dataGrid.CurrentRow.DataBoundItem != null)
                {
                    RowSelecionada = ((DataRowView)dataGrid.CurrentRow.DataBoundItem).Row;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao selecionar item: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    AplicarFiltroPorData(dateTimePicker2.Value.Date);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Pesquisar, " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dateTimePicker2.Value > dateTimePicker1.Value)
                {
                    MessageBox.Show("A data inicial não pode ser maior que a data final.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AplicarFiltroPorDatas(dateTimePicker2.Value.Date, dateTimePicker1.Value.Date);
            }
        }   

        private void AdicionarDetalhes()
        {
            try
            {
                foreach (DataRow row in dsStock.MovimentacoeStock.Rows)
                {
                    // Preenche a coluna Categoria
                    if (row["ProdutoID"] != DBNull.Value)
                        row["NomeProduto"] = querryProdutosTableAdapter.NomeProduto(row["ProdutoID"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar detalhes", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

       
        public void CarregarStockSelecionado(ProdutosRow produto)
        {
            if (RowSelecionada != null)
            {
                PreencherStock(produto);
            }

        }
        public void PreencherStock(ProdutosRow produto)
        {
            
            var stockRow = dsStock.PesquisarStock(TxtCodigo.Text,produto);
            if (stockRow.ProdutoID != "")
            {
                TxtCodigo.Text = stockRow.ProdutoID;
                TxtNome.Text = stockRow.Quantidade.ToString();
            }
            else
            {
                Limpar();
            }
        }

        private bool ValidarPreenchimento()
        {
            return !string.IsNullOrWhiteSpace(TxtCodigo.Text) &&
                               !string.IsNullOrWhiteSpace(TxtNome.Text) &&
                                !string.IsNullOrWhiteSpace(txtAddStock.Text) &&
                                 !string.IsNullOrWhiteSpace(cbTipoDoc.Text);
        }

        private void PesquisarProdutos(string termo)
        {
            // Pesquisa em várias colunas
            dataViewStock.RowFilter = $"NomeProduto LIKE '%{termo}%' OR Marca LIKE '%{termo}%' OR Categoria LIKE '%{termo}%'OR IMEI LIKE '%{termo}%' OR Tipo LIKE '%{termo}%'";
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {

        }
        //fill da grid consoante os filtros
        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (cbOrdenar.SelectedItem == null || cbFiltro.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione um campo e um valor para filtrar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string campo = cbOrdenar.SelectedItem.ToString();
            string valor = cbFiltro.SelectedItem.ToString();

            AplicarFiltro(campo, valor);
        }

        private void cbOrdenar_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbFiltro.Items.Clear();

            if (cbOrdenar.SelectedItem == null)
            {
                return;
            }

            string campo = cbOrdenar.SelectedItem.ToString();

           
                    if (campo == "Data")
                    {
                        cbFiltro.Items.AddRange(new string[] { "Data Entrega", "Data Rececao" }); // Ajuste conforme necessário

                        dateTimePicker1.Visible = true;
                        dateTimePicker2.Visible = true;
                    }
                    else if (campo == "Tipo Doc")
                    {
                        //dsDocumentos.CarregaTipoDoc();
                        //var tiposDoc = dsDocumentos.TipoDocumentos.AsEnumerable().Select(t => t.Descricao).ToArray();
                        //cbFiltro.Items.AddRange(tiposDoc);
                    }
                    else if (campo == "Estado")
                    {
                        cbFiltro.Items.AddRange(new string[] { "Pronto", "Em Preparacao", "Cancelado", "Em Espera" }); // Ajuste conforme necessário
                    }
                                         

            cbFiltro.Enabled = cbFiltro.Items.Count > 0;
        }

        private void AplicarFiltro(string campo, string valor)
        {
            dataViewMov.RowFilter = $"{campo}  = '{valor}'";

        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {

            if ((string.IsNullOrEmpty(txtPesquisa.Text)) && (cbFiltro.Text == ""))
            {
                // Se o texto for apagado, recarrega os dados originais
                btnRefresh_Click(sender, e);
            }

        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Limpa filtros e recarrega os dados
            txtPesquisa.Text = string.Empty;
            cbOrdenar.Text = "";
            cbFiltro.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;


            CarregarMovimentos();       
            CarregarStockPr();

        }


        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (ValidarPreenchimento())
            {
                var stock = dsStock.Armazem[0];

                int valorAntigo = stock.Quantidade;
                int Valor = int.Parse(txtAddStock.Text);
                int valorNovo = 0;

                if (cbTipoDoc.Text == "Entrada")
                {
                    valorNovo = valorAntigo + Valor;
                }
                else
                {
                    valorNovo = valorAntigo - Valor;
                }
                stock.ProdutoID = TxtCodigo.Text;
                stock.Quantidade = valorNovo;
            }
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            Limpar();
            SelecionarLinhaAtual(dGridStock);
            if (RowSelecionada is ProdutosRow produtoRow)
            {
                dsStock.NovoStockRow(produtoRow);
                TxtCodigo.Text = dsStock.Armazem[0].ProdutoID;
            }
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Limpar()
        {

            TxtCodigo.Clear();
            TxtNome.Clear();
            cbTipoDoc.Text = "";
            txtAddStock.Clear();
        }

    }
}
