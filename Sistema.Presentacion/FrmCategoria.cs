using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema.Negocio;

namespace Sistema.Presentacion
{
    public partial class FrmCategoria : Form
    {
        private string NombreAnt;
        public FrmCategoria()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {

                dgvListado.DataSource = NCategoria.Listar();
                this.Formato();
                this.Limpiar();
                lblTotal.Text = "Total Registros: " + Convert.ToString(dgvListado.Rows.Count);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void Buscar()
        {
            try
            {

                dgvListado.DataSource = NCategoria.Buscar(txtBuscar.Text);
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
            MessageBox.Show(Mensaje,"Sistema de Ventas",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        private void MensajeOk(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Formato()
        {
            dgvListado.Columns[0].Visible = false;
            dgvListado.Columns[1].Visible = false;
            dgvListado.Columns[2].Width = 150;
            dgvListado.Columns[3].Width = 400;
            dgvListado.Columns[3].HeaderText = "Descripcion";
            dgvListado.Columns[4].Width = 100;
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (txtNombre.Text == String.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, seran remarcados");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                }
                else
                {
                    Rpta = NCategoria.Insertar(txtNombre.Text.Trim(),txtDescripcion.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se Inserto de forma correcta el Registro");
                        this.Limpiar();
                        this.Listar();
                    }
                    else 
                    {
                        this.MensajeError(Rpta);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            tabGeneral.SelectedIndex = 0;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (txtNombre.Text == String.Empty || txtId.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, seran remarcados");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                }
                else
                {
                    Rpta = NCategoria.Actualizar(Convert.ToInt32(txtId.Text),this.NombreAnt,txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se ACTUALIZO de forma correcta el Registro");
                        this.Limpiar();
                        this.Listar();
                        tabGeneral.SelectedIndex = 1;
                    }
                    else
                    {
                        this.MensajeError(Rpta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                btnActualizar.Visible = true;
                btnInsertar.Visible = false;
                txtId.Text = dgvListado.CurrentRow.Cells["Id"].Value.ToString();
                txtNombre.Text = dgvListado.CurrentRow.Cells["Nombre"].Value.ToString();
                this.NombreAnt = dgvListado.CurrentRow.Cells["Nombre"].Value.ToString();
                txtDescripcion.Text = dgvListado.CurrentRow.Cells["Descripcion"].Value.ToString();
                tabGeneral.SelectedIndex = 1;
            }
            catch(Exception)
            {
                MessageBox.Show("Seleccione desde la celda nombre","Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                dgvListado.Columns[0].Visible = true;
                btnActivar.Visible = true;
                btnDesactivar.Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                dgvListado.Columns[0].Visible = false;
                btnActivar.Visible = false;
                btnDesactivar.Visible = false;
                btnEliminar.Visible = false;
            }
        }

        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex = dgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                chkEliminar.Value =!Convert.ToBoolean(chkEliminar.Value);

            }
        }
    }
}
