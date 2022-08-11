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

namespace Sistema.Presentacion
{
    public partial class FrmArticulo : Form
    {
        private string RutaOrigen;
        private string RutaDestino;
        private string Directorio= "C:\\SistemaCS\\imagen\\";
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
            panelCodigo.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.EAN13,txtCodigo.Text,Color.Black,Color.White,400,100);
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
    }
}
