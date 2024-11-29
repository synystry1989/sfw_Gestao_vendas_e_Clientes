﻿using TeleBerço.DsClientesTableAdapters;
using TeleBerço;
using static TeleBerço.DsDocumentos;
using System.Data;
using System;





namespace TeleBerço
{
    partial class DsClientes
    {
        public ClientesTableAdapter adpClientes = new ClientesTableAdapter();

        public FornecedoresTableAdapter FornecedoresTableAdapter = new FornecedoresTableAdapter();
        public void CarregaClientes()
        {
            Clientes.Clear();
            adpClientes.Fill(Clientes);
        }
        public void UpdateClientes()
        {
            adpClientes.Update(Clientes);
        }
        public void CarregaFornecedores()
        {
            Fornecedores.Clear();
            FornecedoresTableAdapter.Fill(Fornecedores);
        }
        public void UpdateFornecedores()
        {
            FornecedoresTableAdapter.Update(Fornecedores);
        }

        public string DaUltimoCodCliente()
        {
            return adpClientes.UltmCodCl();
        }

        public void NovoFornecedor()
        {
            if (Fornecedores.Rows.Count == 0)
            {
                FornecedoresRow novoCliente = (FornecedoresRow)Fornecedores.NewRow();

                novoCliente.FornecedorID = DaProxNrFornecedor();
                novoCliente.Nome = "";
                novoCliente.Contato = "";
                novoCliente.Site = "";
                novoCliente.Categoria = "";


                Fornecedores.AddFornecedoresRow(novoCliente);
            }
        }

        public void NovoCliente()
        {
            if (Clientes.Rows.Count == 0)
            {
                ClientesRow novoCliente = (ClientesRow)Clientes.NewRow();

                novoCliente.CodCl = DaProxNrCliente();
                novoCliente.Nome = "";
                novoCliente.Telefone = "";
                novoCliente.Email = "";


                Clientes.AddClientesRow(novoCliente);
            }
        }
        public string DaProxNrCliente()
        {

            string codCl = DaUltimoCodCliente();
            int valor = 0;
            if (codCl != null)
            {
                //retiramos o CL ao cod incrementamos 
                valor = int.Parse(codCl.Substring(2));
                valor++;
                return $"CL{valor:000}";
            }
            else
            //se nao existir 
            {
                valor = 001;
                return $"CL{valor:000}";
            }
        }
        public string DaUltmtNrFornecedor()
        {
            var resultado = FornecedoresTableAdapter.UltimoCodFornecedor();
            if (resultado != null)
            {
                return resultado.ToString();
            }
            else
            {
                return "FN000"; // Ou outro valor padrão adequado
            }
        }

        public string DaProxNrFornecedor()
        {
            string codFn = DaUltmtNrFornecedor();
            int valor = 0;
            if (codFn != null)
            {
                //retiramos o CL ao cod incrementamos 
                valor = int.Parse(codFn.Substring(2));
                valor++;
                return $"FN{valor:000}";
            }
            else
            //se nao existir 
            {
                
                return codFn;
            }
        }

        public ClientesRow PesquisaCliente(string codCl)
        {
            adpClientes.FillByCodCl(Clientes, codCl);

            if (Clientes.Rows.Count > 0)
            {
                return Clientes[0];
            }
            NovoCliente();
            return Clientes[0];
        }

        public FornecedoresRow PesquisaFornecedor(string codFn)
        {
            FornecedoresTableAdapter.FillById(Fornecedores, codFn);

            if (Clientes.Rows.Count > 0)
            {
                return Fornecedores[0];
            }
            NovoFornecedor();
            return Fornecedores[0];
        }

        public string CarregaNomeCliente(string codCl)
        {
            return adpClientes.NomeCliente(codCl);
        }

        public void EliminarCliente(string id)
        {
            ClientesRow linhaSelecionada = Clientes.FindByCodCl(id);

            linhaSelecionada?.Delete();

            UpdateClientes();
        }

        public void EliminarFornecedor(string id)
        {
            FornecedoresRow linhaSelecionada = Fornecedores.FindByFornecedorID(id);

            linhaSelecionada?.Delete();

            UpdateFornecedores();
        }
    }

}


