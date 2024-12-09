﻿namespace TeleBerço
{
    partial class FrmCat_Marca
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnNovo = new System.Windows.Forms.Button();
            this.BtnSair = new System.Windows.Forms.Button();
            this.BtnGravar = new System.Windows.Forms.Button();
            this.PainelCliente = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCodigo = new System.Windows.Forms.TextBox();
            this.TxtNome = new System.Windows.Forms.TextBox();
            this.LabelMarca = new System.Windows.Forms.Label();
            this.BtnEliminar = new System.Windows.Forms.Button();
            this.PainelCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnNovo
            // 
            this.BtnNovo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnNovo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BtnNovo.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.BtnNovo.FlatAppearance.BorderSize = 2;
            this.BtnNovo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.BtnNovo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNovo.ForeColor = System.Drawing.Color.DodgerBlue;
            this.BtnNovo.Location = new System.Drawing.Point(15, 19);
            this.BtnNovo.Margin = new System.Windows.Forms.Padding(4);
            this.BtnNovo.Name = "BtnNovo";
            this.BtnNovo.Size = new System.Drawing.Size(108, 33);
            this.BtnNovo.TabIndex = 50;
            this.BtnNovo.Text = "Novo";
            this.BtnNovo.UseCompatibleTextRendering = true;
            this.BtnNovo.UseVisualStyleBackColor = false;
            this.BtnNovo.Click += new System.EventHandler(this.BtnNovo_Click);
            // 
            // BtnSair
            // 
            this.BtnSair.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnSair.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BtnSair.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.BtnSair.FlatAppearance.BorderSize = 2;
            this.BtnSair.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.BtnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSair.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnSair.Location = new System.Drawing.Point(365, 20);
            this.BtnSair.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSair.Name = "BtnSair";
            this.BtnSair.Size = new System.Drawing.Size(108, 32);
            this.BtnSair.TabIndex = 49;
            this.BtnSair.Text = "Sair";
            this.BtnSair.UseCompatibleTextRendering = true;
            this.BtnSair.UseVisualStyleBackColor = false;
            this.BtnSair.Click += new System.EventHandler(this.BtnSair_Click);
            // 
            // BtnGravar
            // 
            this.BtnGravar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnGravar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BtnGravar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.BtnGravar.FlatAppearance.BorderSize = 2;
            this.BtnGravar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.BtnGravar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGravar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGravar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.BtnGravar.Location = new System.Drawing.Point(131, 20);
            this.BtnGravar.Margin = new System.Windows.Forms.Padding(4);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(108, 32);
            this.BtnGravar.TabIndex = 48;
            this.BtnGravar.Text = "Gravar";
            this.BtnGravar.UseCompatibleTextRendering = true;
            this.BtnGravar.UseVisualStyleBackColor = false;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // PainelCliente
            // 
            this.PainelCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PainelCliente.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PainelCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.PainelCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PainelCliente.Controls.Add(this.label1);
            this.PainelCliente.Controls.Add(this.TxtCodigo);
            this.PainelCliente.Controls.Add(this.TxtNome);
            this.PainelCliente.Controls.Add(this.LabelMarca);
            this.PainelCliente.Location = new System.Drawing.Point(15, 61);
            this.PainelCliente.Margin = new System.Windows.Forms.Padding(4);
            this.PainelCliente.Name = "PainelCliente";
            this.PainelCliente.Size = new System.Drawing.Size(915, 114);
            this.PainelCliente.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Categorias";
            // 
            // TxtCodigo
            // 
            this.TxtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCodigo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCodigo.Location = new System.Drawing.Point(164, 49);
            this.TxtCodigo.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.TxtCodigo.Name = "TxtCodigo";
            this.TxtCodigo.Size = new System.Drawing.Size(101, 29);
            this.TxtCodigo.TabIndex = 1;
            // 
            // TxtNome
            // 
            this.TxtNome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtNome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNome.Location = new System.Drawing.Point(278, 49);
            this.TxtNome.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.TxtNome.Name = "TxtNome";
            this.TxtNome.Size = new System.Drawing.Size(507, 29);
            this.TxtNome.TabIndex = 2;
            // 
            // LabelMarca
            // 
            this.LabelMarca.AutoSize = true;
            this.LabelMarca.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelMarca.Location = new System.Drawing.Point(17, 16);
            this.LabelMarca.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelMarca.Name = "LabelMarca";
            this.LabelMarca.Size = new System.Drawing.Size(67, 18);
            this.LabelMarca.TabIndex = 1;
            this.LabelMarca.Text = "Marcas";
            // 
            // BtnEliminar
            // 
            this.BtnEliminar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnEliminar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BtnEliminar.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.BtnEliminar.FlatAppearance.BorderSize = 2;
            this.BtnEliminar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.BtnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEliminar.ForeColor = System.Drawing.Color.Red;
            this.BtnEliminar.Location = new System.Drawing.Point(248, 20);
            this.BtnEliminar.Margin = new System.Windows.Forms.Padding(5);
            this.BtnEliminar.Name = "BtnEliminar";
            this.BtnEliminar.Size = new System.Drawing.Size(108, 32);
            this.BtnEliminar.TabIndex = 51;
            this.BtnEliminar.Text = "Eliminar";
            this.BtnEliminar.UseCompatibleTextRendering = true;
            this.BtnEliminar.UseVisualStyleBackColor = false;
            this.BtnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);
            // 
            // FrmCat_Marca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(936, 190);
            this.Controls.Add(this.BtnEliminar);
            this.Controls.Add(this.BtnNovo);
            this.Controls.Add(this.BtnSair);
            this.Controls.Add(this.BtnGravar);
            this.Controls.Add(this.PainelCliente);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmCat_Marca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Adicionar";
            this.Load += new System.EventHandler(this.FrmCat_Marca_Load);
            this.PainelCliente.ResumeLayout(false);
            this.PainelCliente.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnNovo;
        internal System.Windows.Forms.Button BtnSair;
        internal System.Windows.Forms.Button BtnGravar;
        internal System.Windows.Forms.Panel PainelCliente;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox TxtCodigo;
        internal System.Windows.Forms.TextBox TxtNome;
        internal System.Windows.Forms.Label LabelMarca;
        internal System.Windows.Forms.Button BtnEliminar;
    }
}