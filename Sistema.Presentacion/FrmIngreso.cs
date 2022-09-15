using Sistema.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmIngreso : Form
    {
        private DataTable dtDetalle = new DataTable();
        public FrmIngreso()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {

                dgvListado.DataSource = NIngreso.Listar();
                this.Formato();
                this.Limpiar();
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

                dgvListado.DataSource = NIngreso.Buscar(txtBuscar.Text);
                this.Formato();
                lblTotal.Text = "Total Registros: " + Convert.ToString(dgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Limpiar()
        {
            txtBuscar.Clear();
            //txtDescripcion.Clear();
            //txtNombre.Clear();
            txtId.Clear();

            btnInsertar.Visible = true;
            errorIcono.Clear();

            dgvListado.Columns[0].Visible = false;
            btnAnular.Visible = false;
            chkSeleccionar.Checked = false;
        }
        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Formato()
        {
            dgvListado.Columns[0].Visible = false;
            dgvListado.Columns[1].Visible = false;
            dgvListado.Columns[2].Visible = false;
            dgvListado.Columns[0].Width = 100;
            dgvListado.Columns[3].Width = 150;
            dgvListado.Columns[4].Width = 150;
            dgvListado.Columns[5].Width = 100;
            dgvListado.Columns[5].HeaderText = "Documento";
            dgvListado.Columns[6].Width = 70;
            dgvListado.Columns[6].HeaderText = "Serie";
            dgvListado.Columns[7].Width = 70;
            dgvListado.Columns[7].HeaderText = "Numero";
            dgvListado.Columns[8].Width = 60;
            dgvListado.Columns[9].Width = 100;
            dgvListado.Columns[10].Width = 100;
            dgvListado.Columns[11].Width = 100;
        }

        private void CrearTabla()
        {
            this.dtDetalle.Columns.Add("idArticulo", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("Codigo", System.Type.GetType("System.String"));
            this.dtDetalle.Columns.Add("Articulo", System.Type.GetType("System.String"));
            this.dtDetalle.Columns.Add("Cantidad", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("Precio", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("Importe", System.Type.GetType("System.Decimal"));

            dgvDetalle.DataSource = this.dtDetalle;

            dgvDetalle.Columns[0].Visible = false;
            dgvDetalle.Columns[1].HeaderText = "CODIGO";
            dgvDetalle.Columns[1].Width = 100;
            dgvDetalle.Columns[2].HeaderText = "ARTICULO";
            dgvDetalle.Columns[2].Width = 300;
            dgvDetalle.Columns[3].HeaderText = "CANTIDAD";
            dgvDetalle.Columns[3].Width = 100;
            dgvDetalle.Columns[4].HeaderText = "PRECIO";
            dgvDetalle.Columns[4].Width = 100;
            dgvDetalle.Columns[5].HeaderText = "IMPORTE";
            dgvDetalle.Columns[5].Width = 100;

            dgvDetalle.Columns[1].ReadOnly = true; //codigo solo lectura
            dgvDetalle.Columns[2].ReadOnly = true; //articulo solo lectura
            dgvDetalle.Columns[5].ReadOnly = true; //importe solo lectura
        }
        private void FrmIngreso_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CrearTabla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {

            FrmVista_ProveedorIngreso vista = new FrmVista_ProveedorIngreso();
            vista.ShowDialog();
            txtIdProveedor.Text = Convert.ToString(Variables.IdProveedor);
            txtNombreProveedor.Text = Variables.NombreProveedor;
        }
    }
}
