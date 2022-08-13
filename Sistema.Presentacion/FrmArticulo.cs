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
using System.Drawing.Imaging;
using System.IO;

namespace Sistema.Presentacion
{
    public partial class FrmArticulo : Form
    {
        private string RutaOrigen;
        private string RutaDestino;
        private string Directorio= "C:\\SistemaCS\\imagen\\";
        private string nombreAnt;
        public FrmArticulo()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {
                dgvListado.DataSource = NArticulo.Listar();
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

                dgvListado.DataSource = NArticulo.Buscar(txtBuscar.Text);
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
            txtCodigo.Clear();
            panelCodigo.BackgroundImage = null;
            btnGuardarCodigo.Enabled = false;
            txtPrecioVenta.Clear();
            txtStock.Clear();
            txtImagen.Clear();
            picImagen.Image = null;
            txtDescripcion.Clear();
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;
            errorIcono.Clear();

            dgvListado.Columns[0].Visible = false;
            btnActivar.Visible = false;
            btnDesactivar.Visible = false;
            btnEliminar.Visible = false;
            chkSeleccionar.Checked = false;

            this.RutaDestino = "";
            this.RutaOrigen = "";


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
            dgvListado.Columns[0].Width = 100;
            dgvListado.Columns[1].Width = 50;
            dgvListado.Columns[2].Width = 100;
            dgvListado.Columns[3].Width = 100;
            dgvListado.Columns[3].HeaderText = "Categoria";
            dgvListado.Columns[4].Width = 100;
            dgvListado.Columns[4].HeaderText = "Codigo";
            dgvListado.Columns[5].Width = 150;
            dgvListado.Columns[6].Width = 100;
            dgvListado.Columns[6].HeaderText = "Precio Venta";
            dgvListado.Columns[7].Width = 60; //Stock
            dgvListado.Columns[8].Width = 200;
            dgvListado.Columns[8].HeaderText = "Descripcion";
            dgvListado.Columns[9].Width = 100;
        }

        private void CargarCategoria()
        {
            try
            {
                cboCategoria.DataSource = NCategoria.Seleccionar();
                cboCategoria.ValueMember = "idcategoria";
                cboCategoria.DisplayMember = "nombre";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarCategoria();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Archivos de Imagen (*.jpg,*jpeg,*.jpe,*.jfif, *.png) | *.jpg;*.jpeg;*.jpe;*jfif;*.png";
            if (file.ShowDialog() == DialogResult.OK)
            {
                picImagen.Image = Image.FromFile(file.FileName);
                txtImagen.Text = file.FileName.Substring(file.FileName.LastIndexOf("\\")+1);
                this.RutaOrigen = file.FileName;
            }
        }

        private void btnGenerarCodigo_Click(object sender, EventArgs e)
        {
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
            Codigo.IncludeLabel = true;
            panelCodigo.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.EAN13, StringToEncode: txtCodigo.Text,Color.Black,Color.White,400,100);
            btnGuardarCodigo.Enabled = true;
        }

        private void btnGuardarCodigo_Click(object sender, EventArgs e)
        {
            Image imgFinal = (Image)panelCodigo.BackgroundImage.Clone();
            SaveFileDialog DialogoGuardar = new SaveFileDialog();
            DialogoGuardar.AddExtension = true;
            DialogoGuardar.Filter = "Formato PNG (*.png)|*.png";
            DialogoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(DialogoGuardar.FileName))
            {
                imgFinal.Save(DialogoGuardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if ( cboCategoria.Text == string.Empty || txtNombre.Text == String.Empty || txtPrecioVenta.Text == String.Empty || txtStock.Text == String.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, seran remarcados");
                    errorIcono.SetError(cboCategoria, "Seleccione una categoria");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                    errorIcono.SetError(txtPrecioVenta, "Ingrese el precio de Venta");
                    errorIcono.SetError(txtStock, "Ingrese el stock del Producto");
                }
                else
                {
                    Rpta = NArticulo.Insertar(Convert.ToInt32(cboCategoria.SelectedValue),txtCodigo.Text.Trim(),
                        txtNombre.Text.Trim(),Convert.ToDecimal(txtPrecioVenta.Text),
                        Convert.ToInt32(txtStock.Text), txtDescripcion.Text.Trim(),txtImagen.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se Inserto de forma correcta el Registro");
                        if (txtImagen.Text != string.Empty)
                        {
                            this.RutaDestino = this.Directorio + txtImagen.Text;
                            File.Copy(this.RutaOrigen,this.RutaDestino);
                        }
                        //this.Limpiar();Listar llama a Limpiar
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

        private void btnGenerarCodigo_Click_1(object sender, EventArgs e)
        {
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
            Codigo.IncludeLabel = true;
            //Codigo EAN13, es obligatorio 13 digitos
            if (txtCodigo.Text.Length == 13)
            {
                panelCodigo.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.EAN13, StringToEncode: txtCodigo.Text, Color.Black, Color.White, 400, 100);
                btnGuardarCodigo.Enabled = true;
            }
            else
            {
                this.MensajeError("Ingrese codigo de barra (13 Digitos)");
            }
            
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                btnActualizar.Visible = true;
                btnInsertar.Visible = false;
                txtId.Text = Convert.ToString(dgvListado.CurrentRow.Cells["ID"].Value);
                cboCategoria.SelectedValue = Convert.ToString(dgvListado.CurrentRow.Cells["idcategoria"].Value);
                txtCodigo.Text= Convert.ToString(dgvListado.CurrentRow.Cells["Codigo"].Value);
                this.nombreAnt = Convert.ToString(dgvListado.CurrentRow.Cells["Nombre"].Value);
                txtNombre.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Nombre"].Value);
                txtPrecioVenta.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Precio_Venta"].Value);
                txtStock.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Stock"].Value);
                txtDescripcion.Text = Convert.ToString(dgvListado.CurrentRow.Cells["Descripcion"].Value);
                string Imagen;
                Imagen = Convert.ToString(dgvListado.CurrentRow.Cells["Imagen"].Value);
                if (Imagen != string.Empty)
                {
                    picImagen.Image = Image.FromFile(this.Directorio + Imagen);
                    txtImagen.Text = Imagen;
                }
                else 
                {
                    picImagen.Image = null;
                    txtImagen.Text = "";
                }
                tabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione desde la celda nombre." + "| Error: "+ ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (txtId.Text == string.Empty || cboCategoria.Text == string.Empty || txtNombre.Text == string.Empty || txtPrecioVenta.Text == string.Empty || txtStock.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados.");
                    errorIcono.SetError(cboCategoria, "Seleccione una categoría.");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre.");
                    errorIcono.SetError(txtPrecioVenta, "Ingrese un precio.");
                    errorIcono.SetError(txtStock, "Ingrese un stock inicial.");
                }
                else
                {
                    Rpta = NArticulo.Actualizar(Convert.ToInt32(txtId.Text), Convert.ToInt32(cboCategoria.SelectedValue), txtCodigo.Text.Trim(), this.nombreAnt, txtNombre.Text.Trim(), Convert.ToDecimal(txtPrecioVenta.Text), Convert.ToInt32(txtStock.Text), txtDescripcion.Text.Trim(), txtImagen.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se actualizó de forma correcta el registro");
                        if (txtImagen.Text != string.Empty && this.RutaOrigen != string.Empty)
                        {
                            this.RutaDestino = this.Directorio + txtImagen.Text;
                            File.Copy(this.RutaOrigen, this.RutaDestino);
                        }
                        this.Listar();
                        tabGeneral.SelectedIndex = 0;
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
                    string Imagen = "";
                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Imagen = Convert.ToString(row.Cells[9].Value);
                            Rpta = NArticulo.Eliminar(Convert.ToInt32(Codigo));
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se elimino el registro: " + Convert.ToString(row.Cells[5].Value));
                                File.Delete(this.Directorio + Imagen);
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
                            Rpta = NArticulo.Desactivar(Convert.ToInt32(Codigo));
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se desactivo el registro: " + Convert.ToString(row.Cells[5].Value));
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
                            Rpta = NArticulo.Activar(Convert.ToInt32(Codigo));
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se desactivo el registro: " + Convert.ToString(row.Cells[5].Value));
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
