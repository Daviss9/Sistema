using Sistema.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmUsuario : Form
    {
        public FrmUsuario()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {

                dgvListado.DataSource = NUsuario.Listar();
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

                dgvListado.DataSource = NUsuario.Buscar(txtBuscar.Text);
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
            txtDescripcion.Clear();
            txtNombre.Clear();
            txtId.Clear();
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;
            errorIcono.Clear();

            dgvListado.Columns[0].Visible = false;
            btnActivar.Visible = false;
            btnDesactivar.Visible = false;
            btnEliminar.Visible = false;
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
            dgvListado.Columns[2].Visible = false;
            dgvListado.Columns[1].Width = 50;
            dgvListado.Columns[3].Width = 100;
            dgvListado.Columns[4].Width = 170;
            dgvListado.Columns[5].Width = 100;
            dgvListado.Columns[5].HeaderText = "Documento";
            dgvListado.Columns[6].Width = 100;
            dgvListado.Columns[6].HeaderText = "Numero Doc";
            dgvListado.Columns[7].Width = 100;
            dgvListado.Columns[7].HeaderText = "Direccion";
            dgvListado.Columns[8].Width = 100;
            dgvListado.Columns[8].HeaderText = "Telefono";
            dgvListado.Columns[9].Width = 120;
        }
        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }
    }
}
