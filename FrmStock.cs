using System;
using System.Data;
using System.Windows.Forms;
using TeleBerço.DsProdutosTableAdapters;
using static TeleBerço.DsStock;

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
        public DataRow RowSelecionada { get; private set; }



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
                cbFiltro.Enabled = true;
                cbOrdenar.Items.Clear();


                dGridStock.Columns["Marca"].Visible = false;
                dGridStock.Columns["Categoria"].Visible = false;
                dGridStock.Columns["NomeProduto"].HeaderText = "Produto";
                dGridStock.Columns["CodPr"].HeaderText = "Codigo";
                dGridStock.Columns["NomeMarca"].HeaderText = "Marca";
                dGridStock.Columns["NomeCategoria"].HeaderText = "Categoria";
                dGridStock.Columns["NomeCategoria"].DisplayIndex = 1;
                dGridStock.Columns["NomeMarca"].DisplayIndex = 2;
                dGridStock.Columns["CodPr"].DisplayIndex = 3;
                dGridStock.Columns["NomeProduto"].DisplayIndex = 4;


                dgridMovimentos.Columns["ProdutoID"].Visible = false;
                dgridMovimentos.Columns["NomeProduto"].DisplayIndex = 1;
                dgridMovimentos.Columns["NomeProduto"].HeaderText = "Produto";

                cbOrdenar.Items.AddRange(new string[] { "Data", "Tipo", "Produto" });

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
            DateTime inicioDoDia = dataInicio.Date;

            DateTime fimDoDia = dataFim.Date;
            // mesma data, hora zero


            dataViewMov.RowFilter = string.Format("{0} >= #{1:MM/dd/yyyy}# AND {0} < #{2:MM/dd/yyyy}#",
                                               "DataMovimentacao", inicioDoDia, fimDoDia);

        }

        private void AplicarFiltroPorData(DateTime data)
        {
            DateTime inicioDoDia = data.Date;        // mesma data, hora zero
            DateTime fimDoDia = inicioDoDia.AddDays(1);

            dataViewMov.RowFilter = string.Format("{0} >= #{1:MM/dd/yyyy}# AND {0} < #{2:MM/dd/yyyy}#",
                                                  "DataMovimentacao", inicioDoDia, fimDoDia);

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
        private void dateTimePicker2_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    AplicarFiltroPorData(dateTimePicker2.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Pesquisar, " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dateTimePicker1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dateTimePicker2.Value > dateTimePicker1.Value)
                {
                    MessageBox.Show("A data inicial não pode ser maior que a data final.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AplicarFiltroPorDatas(dateTimePicker2.Value, dateTimePicker1.Value);
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

        private void btnPesquisar_Click_1(object sender, EventArgs e)
        {

        }
        //fill da grid consoante os filtros
        private void btnAplicar_Click_1(object sender, EventArgs e)
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

        private void cbOrdenar_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cbFiltro.Items.Clear();

            if (cbOrdenar.SelectedItem == null)
            {
                return;
            }
            string campo = cbOrdenar.SelectedItem.ToString();

            if (campo == "Data")
            {
                cbFiltro.Items.AddRange(new string[] { "Dia", "Periodo" }); // Ajuste conforme necessário               

            }
            else if (campo == "Tipo")
            {
                cbFiltro.Items.AddRange(new string[] { "E", "S" }); // Ajuste conforme necessário
            }
            cbFiltro.Text = "";
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

        private void BtnGravar_Click(object sender, EventArgs e)
        {
            if (ValidarPreenchimento())
            {
                ArmazemRow stock = dsStock.Armazem[0];

                int valorAntigo = stock.Quantidade;
                int valor = int.Parse(txtAddStock.Text);
                int valorNovo = 0;
                string tipoDoc = "";
                if (cbTipoDoc.Text == "Entrada")
                {
                    valorNovo = valorAntigo + valor;
                    tipoDoc = "FTF";
                }
                else
                {
                    valorNovo = valorAntigo - valor;
                    tipoDoc = "FTC";

                }
                stock.ProdutoID = TxtCodigo.Text;

                dsStock.AtualizarStock(stock.ProdutoID, valor, tipoDoc);
                btnRefresh_Click(sender, e);
                Limpar();
            }
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            Limpar();
            SelecionarLinhaAtual(dGridStock);
            if (RowSelecionada is StockPrRow row)
            {

                ArmazemRow stock = dsStock.PesquisarStock(row.CodPr);
                TxtCodigo.Text = dsStock.Armazem[0].ProdutoID;
                TxtNome.Text = dsStock.Armazem[0].Quantidade.ToString();
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

        private void cbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFiltro.Text == "Dia")
            {
                dateTimePicker2.Visible = true;
            }
            else if (cbFiltro.Text == "Periodo")
            {
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
            }
            else
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
            }
        }
    }
}
