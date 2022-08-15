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
        private string EmailAnt;
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
            txtNombre.Clear();
            txtId.Clear();
            txtNumDocumento.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtClave.Clear();
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
            dgvListado.Columns[10].Width = 120;
        }
        private void CargarRol()
        {
            cboRol.DataSource = NRol.Listar();
            cboRol.ValueMember = "idrol";
            cboRol.DisplayMember = "nombre";
        }
        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarRol();
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
                if (cboRol.Text == String.Empty || txtNombre.Text == String.Empty || txtEmail.Text == String.Empty || txtClave.Text == String.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, seran remarcados");
                    errorIcono.SetError(cboRol, "Seleccione un rol");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                    errorIcono.SetError(txtEmail, "Ingrese un email");
                    errorIcono.SetError(txtClave, "Ingrese una clave de acceso");

                }
                else
                {
                    Rpta = NUsuario.Insertar(Convert.ToInt32(cboRol.SelectedValue),txtNombre.Text.Trim(), Convert.ToString(cboTipoDocumento.Text),
                        txtNumDocumento.Text.Trim(),txtDireccion.Text.Trim(),txtTelefono.Text.Trim(),txtEmail.Text.Trim(),txtClave.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se Inserto de forma correcta el Registro");
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
                cboRol.Text = Convert.ToString(dgvListado.CurrentRow.Cells["idrol"].Value);
                txtNombre.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Nombre"].Value);
                cboTipoDocumento.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Tipo_documento"].Value);
                txtNumDocumento.Text = Convert.ToString(dgvListado.CurrentRow.Cells["num_documento"].Value);
                txtDireccion.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Direccion"].Value);
                txtTelefono.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Telefono"].Value);
                this.EmailAnt = Convert.ToString(dgvListado.CurrentRow.Cells["Email"].Value);
                txtEmail.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Email"].Value);
                tabGeneral.SelectedIndex = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione desde la celda nombre." + "| Error: " + ex.Message +" ==> " + ex.StackTrace);
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            //2204168
            try
            {
                string Rpta = "";
                if (txtId.Text==String.Empty ||cboRol.Text == String.Empty || txtNombre.Text == String.Empty || txtEmail.Text == String.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, seran remarcados");
                    errorIcono.SetError(cboRol, "Seleccione un rol");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                    errorIcono.SetError(txtEmail, "Ingrese un email");
                    
                }
                else
                {
                    Rpta = NUsuario.Actualizar(Convert.ToInt32(txtId.Text),Convert.ToInt32(cboRol.SelectedValue), txtNombre.Text.Trim(), Convert.ToString(cboTipoDocumento.Text),
                        txtNumDocumento.Text.Trim(), txtDireccion.Text.Trim(), txtTelefono.Text.Trim(), this.EmailAnt,txtEmail.Text.Trim(), txtClave.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se Actualizo de forma correcta el Registro");
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

        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);

            }
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
                            Rpta = NUsuario.Eliminar(Convert.ToInt32(Codigo));
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se elimino el registro: " + Convert.ToString(row.Cells[4].Value)); //Nombre esta en la celda 4 del SP SQL
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

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Desea DESACTIVAR los registros", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string Rpta = "";
                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Rpta = NUsuario.Desactivar(Convert.ToInt32(Codigo));
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se desactivo el registro: " + Convert.ToString(row.Cells[4].Value));
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

        private void btnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Desea ACTIVAR los registros", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string Rpta = "";
                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Rpta = NUsuario.Activar(Convert.ToInt32(Codigo));
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Activo el registro: " + Convert.ToString(row.Cells[4].Value));
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
    }
}
