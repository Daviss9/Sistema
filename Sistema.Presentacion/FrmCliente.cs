using Sistema.Negocio;
using System;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmCliente : Form
    {
        private string nombreAnt;       
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {

                dgvListado.DataSource = NPersona.ListarClientes();
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

                dgvListado.DataSource = NPersona.BuscarClientes(txtBuscar.Text);
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
            txtNombre.Clear();
            txtId.Clear();
            txtNumDocumento.Clear();
            txtDireccion.Clear();
            txtEmail.Clear();
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;
            errorIcono.Clear();

            dgvListado.Columns[0].Visible = false;
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
                    Rpta = NPersona.Insertar("CLIENTE",txtNombre.Text.Trim(), Convert.ToString(cboTipoDocumento.Text),
                        txtNumDocumento.Text.Trim(), txtDireccion.Text.Trim(), txtTelefono.Text.Trim(), txtEmail.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se Inserto el Cliente de forma correcta el Registro");
                        this.Listar();
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
                btnInsertar.Visible = false;
                btnActualizar.Visible = true;
                txtId.Text = Convert.ToString(dgvListado.CurrentRow.Cells["ID"].Value);
                this.nombreAnt = Convert.ToString(dgvListado.CurrentRow.Cells["Nombre"].Value);
                txtNombre.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Nombre"].Value);
                cboTipoDocumento.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Tipo_documento"].Value);
                txtNumDocumento.Text = Convert.ToString(dgvListado.CurrentRow.Cells["num_documento"].Value);
                txtDireccion.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Direccion"].Value);
                txtTelefono.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Telefono"].Value);
                txtEmail.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Email"].Value);
                tabGeneral.SelectedIndex = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione desde la celda nombre." + "| Error: " + ex.Message + " ==> " + ex.StackTrace);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (txtId.Text==string.Empty || txtNombre.Text == String.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, seran remarcados");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");

                }
                else
                {
                    Rpta = NPersona.Actualizar(Convert.ToInt32(txtId.Text),"CLIENTE",this.nombreAnt, txtNombre.Text.Trim(), Convert.ToString(cboTipoDocumento.Text),
                        txtNumDocumento.Text.Trim(), txtDireccion.Text.Trim(), txtTelefono.Text.Trim(), txtEmail.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se Inserto el Proveedor de forma correcta el Registro");
                        this.Listar();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            tabGeneral.SelectedIndex = 0;
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                dgvListado.Columns[0].Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                dgvListado.Columns[0].Visible = false;
                btnEliminar.Visible = false;
            }
        }

        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Desea eliminar los registros", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string Rpta = "";
                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Rpta = NPersona.Eliminar(Convert.ToInt32(Codigo));
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se elimino el registro: " + Convert.ToString(row.Cells[3].Value)); //Nombre esta en la celda 3 del SP SQL
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }
                        }
                    }
                    this.Listar();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            this.Listar();
        }
    }
}
