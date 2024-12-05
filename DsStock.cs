using System;
using System.Data;
using System.Windows.Forms;
using TeleBerço.DsProdutosTableAdapters;
using TeleBerço.DsStockTableAdapters;
using static TeleBerço.DsProdutos;

namespace TeleBerço
{

    partial class DsStock
    {
        ArmazemTableAdapter armazemTableAdapter = new ArmazemTableAdapter();
        MovimentacoeStockTableAdapter movimentacoeStockTableAdapter = new MovimentacoeStockTableAdapter();
        StockPrTableAdapter stockPrTableAdapter = new StockPrTableAdapter();
        QuerryProdutosTableAdapter querryProdutosTableAdapter = new QuerryProdutosTableAdapter();

        public void CarregarStockPr()
        {
            StockPr.Clear();
         
            stockPrTableAdapter.FillPr_Stock(StockPr);
        }

        public void CarregarMovimentos()
        {
            MovimentacoeStock.Clear();
            movimentacoeStockTableAdapter.Fill(MovimentacoeStock);
        }

        public void UpdateStock()
        {
            armazemTableAdapter.Update(Armazem);
        }

        public void UpdateMovimentos()
        {
            movimentacoeStockTableAdapter.Update(MovimentacoeStock);
        }

        public void NovoStockRow(ProdutosRow produto)
        {
            var stock = Armazem.NewArmazemRow();

            stock.ProdutoID = produto.CodPr;
            stock.Quantidade = 0;
            Armazem.AddArmazemRow(stock);
        }

        public void NovoMovimentoRow()
        {
            var movimento = MovimentacoeStock.NewMovimentacoeStockRow();

            movimento.ProdutoID = "";
            movimento.DataMovimentacao = DateTime.Now;
            movimento.Quantidade = 0;
            movimento.TipoMovimentacao = "";
            movimento.NomeProduto = "";
            MovimentacoeStock.AddMovimentacoeStockRow(movimento);
        }

        public MovimentacoeStockRow PesquisarMov(int movID)
        {
            movimentacoeStockTableAdapter.FillByMovID(MovimentacoeStock, movID);
            if (MovimentacoeStock.Rows.Count > 0)
            {
                return MovimentacoeStock[0];
            }
            else
            {
                NovoMovimentoRow();
                return MovimentacoeStock[0];
            }

        }

        public ArmazemRow PesquisarStock(string produto, ProdutosRow produtos)
        {
            armazemTableAdapter.FillByProdutoId(Armazem, produto);
            if (Armazem.Rows.Count > 0)
            {
                return Armazem[0];
            }
            else
            {
                NovoStockRow(produtos);
                return Armazem[0];
            }
        }

        public void EliminarStock(string id)
        {

            ArmazemRow linhaSelecionada = Armazem.FindByProdutoID(id);
            if (linhaSelecionada != null)
            {
                linhaSelecionada.Delete();
                UpdateStock();
            }
            else { MessageBox.Show("Registo nao encontrado"); }


        }

        public void AtualizarStock(string produtoID, int quantidade, string tipoDocumento)
        {
            // Encontrar o registro do produto no estoque
            ArmazemRow stockRow = Armazem[0];

            string tipoEntrada = "";
            int ajusteQuantidade = 0;
            DateTime data = DateTime.Now;

            if (stockRow.ProdutoID == produtoID)
            {

                if ((tipoDocumento.Contains("FTC")) || (tipoDocumento.Contains("NDF")))
                {
                    // Para vendas e devoluções ao fornecedor, diminuir o estoque
                    ajusteQuantidade = -quantidade;
                    tipoEntrada = "S";
                }
                else if ((tipoDocumento.Contains("FTF")) || (tipoDocumento.Contains("NDC")))
                {
                    // Para compras e devoluções de clientes, aumentar o estoque
                    ajusteQuantidade = quantidade;
                    tipoEntrada = "E";
                }


                // Atualizar a quantidade no estoque
                stockRow.Quantidade += ajusteQuantidade;

                // Verificar se a quantidade não ficou negativa
                if (stockRow.Quantidade < 0)
                {
                    MessageBox.Show($"Estoque insuficiente para o produto {produtoID}.");
                    // Você pode decidir como tratar esse caso, por exemplo, impedir a operação
                    stockRow.Quantidade = 0; // Ajustar para zero para evitar estoque negativo
                }
               RegistrarMovimentacao(stockRow.ProdutoID, ajusteQuantidade, tipoEntrada, data);
               UpdateStock();
            }
            else
            {
                // Se o produto não existe no estoque, adicioná-lo
                MessageBox.Show($"Produto Não Existe Em Stock.");

            }

        }
        public void RegistrarMovimentacao(string produtoID, int quantidade, string tipoMovimento, DateTime date)
        {
            MovimentacoeStock.Clear();
            NovoMovimentoRow();
           
            MovimentacoeStockRow novaMovimentacao = MovimentacoeStock[0];

            novaMovimentacao.ProdutoID = produtoID;
            novaMovimentacao.Quantidade = quantidade;
            novaMovimentacao.TipoMovimentacao = tipoMovimento; // 'E' para entrada, 'S' para saída
            novaMovimentacao.NomeProduto = querryProdutosTableAdapter.NomeProduto(produtoID);
            novaMovimentacao.DataMovimentacao = date;

            UpdateMovimentos();
        }


    }
}
