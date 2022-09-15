using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Sistema.Negocio;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmVista_ProveedorIngreso : Form
    {
        public FrmVista_ProveedorIngreso()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {

                dgvListado.DataSource = NPersona.ListarProveedores();
                this.Formato();
                lblTotal.Text = "Total Registros: " + Convert.ToString(dgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void Buscar()
        {
            try
            {

                dgvListado.DataSource = NPersona.BuscarProveedores(txtBuscar.Text);
                this.Formato();
                lblTotal.Text = "Total Registros: " + Convert.ToString(dgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void Formato()
        {
            dgvListado.Columns[0].Visible = false;
            dgvListado.Columns[1].Width = 50;
            dgvListado.Columns[2].Width = 100;
            dgvListado.Columns[2].HeaderText = "Tipo Persona";
            dgvListado.Columns[3].Width = 170;
            dgvListado.Columns[4].Width = 100;
            dgvListado.Columns[4].HeaderText = "Documento";
            dgvListado.Columns[5].Width = 100;
            dgvListado.Columns[5].HeaderText = "Nro.Documento";
            dgvListado.Columns[6].Width = 120;
            dgvListado.Columns[6].HeaderText = "Direccion";
            dgvListado.Columns[7].Width = 100;
            dgvListado.Columns[7].HeaderText = "Telefono";
            dgvListado.Columns[8].Width = 110;
        }

        private void FrmVista_ProveedorIngreso_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Variables.IdProveedor = Convert.ToInt32(dgvListado.CurrentRow.Cells["ID"].Value);
            Variables.NombreProveedor = Convert.ToString(dgvListado.CurrentRow.Cells["Nombre"].Value);
            this.Close();
        }
    }
}
