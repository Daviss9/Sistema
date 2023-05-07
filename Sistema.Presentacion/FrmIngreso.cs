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
            txtId.Clear();
            txtCodigo.Clear();
            txtIdProveedor.Clear();
            txtNombreProveedor.Clear();
            txtSerieComprobante.Clear();
            txtNumComprobante.Clear();
            dtDetalle.Clear();
            txtSubTotal.Text = "0.00";
            txtTotalImpuesto.Text = "0.00";
            txtTotal.Text = "0.00";

            
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

        private void FormatoArticulos()
        {
            dgvArticulos.Columns[1].Visible = false;
            dgvArticulos.Columns[1].Width = 50;
            dgvArticulos.Columns[2].Width = 100;
            dgvArticulos.Columns[2].HeaderText = "Categoria";
            dgvArticulos.Columns[3].Width = 100;
            dgvArticulos.Columns[3].HeaderText = "Codigo";
            dgvArticulos.Columns[4].Width = 150;
            dgvArticulos.Columns[5].Width = 100;
            dgvArticulos.Columns[5].HeaderText = "Precio Venta";
            dgvArticulos.Columns[6].Width = 60;
            dgvArticulos.Columns[7].Width = 200;
            dgvArticulos.Columns[7].HeaderText = "Descripcion";
            dgvArticulos.Columns[8].Width = 100;
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
            //Una vez Cerrada la vista buscar proveedor, rellena las variables
            txtIdProveedor.Text = Convert.ToString(Variables.IdProveedor);
            //txtIdProveedor.Text = Convert.ToString(Variables.IdProveedor);
            txtNumeroRuc.Text = Variables.NumeroRuc;
            txtNombreProveedor.Text = Variables.NombreProveedor;
            //txtNombreProveedor.Text = Variables.NombreProveedor;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                { 
                    //Evaluo si el usuario presiona Enter
                    DataTable Tabla = new DataTable();
                    Tabla = NArticulo.BuscarCodigo(txtCodigo.Text.Trim());
                    if (Tabla.Rows.Count <= 0)
                    {
                        this.MensajeOk("No existe el codigo de Barras");
                    }
                    else
                    {
                        //Agregar Detalle
                        this.AgregarDetalle(
                            //id
                            Convert.ToInt32(Tabla.Rows[0][0]),
                            //Codigo
                            Convert.ToString(Tabla.Rows[0][1]),
                            //Nombre
                            Convert.ToString(Tabla.Rows[0][2]),
                            //Precio
                            Convert.ToDecimal(Tabla.Rows[0][3])
                            );
                    }   

                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
        }
        private void AgregarDetalle(int IdArticulo,string Codigo,string Nombre,decimal Precio)
        {
            bool Agregar = true;

            foreach (DataRow FilaTemp in dtDetalle.Rows)
            {
                if (Convert.ToInt32(FilaTemp["idarticulo"]) == IdArticulo)
                {
                    Agregar = false;
                    this.MensajeError("El Articulo ya ha sido agregado");
                }
            }

            if (Agregar)
            {
                DataRow Fila = dtDetalle.NewRow();
                Fila["idarticulo"] = IdArticulo;
                Fila["codigo"] = Codigo;
                Fila["articulo"] = Nombre;
                Fila["cantidad"] = 1;
                Fila["precio"] = Precio;
                Fila["importe"] = Precio;
                this.dtDetalle.Rows.Add(Fila);
                this.CalcularTotales();
            }
        }

        private void CalcularTotales()
        {
            decimal Total = 0;
            decimal SubTotal = 0;
            if (dgvDetalle.Rows.Count == 0)
            {
                Total = 0;
            }
            else
            {
                foreach (DataRow FilaTemp in dtDetalle.Rows)
                {
                    Total = Total + Convert.ToDecimal(FilaTemp["importe"]);
                }
            }
            SubTotal = Total/(1+Convert.ToDecimal(txtImpuesto.Text));
            txtTotal.Text = Total.ToString("#0.00#");
            txtSubTotal.Text = SubTotal.ToString("#0.00#");
            txtTotalImpuesto.Text = (Total-SubTotal).ToString("#0.00#");
        }
        private void btnVerArticulo_Click(object sender, EventArgs e)
        {
            pnlArticulo.Visible = true;
        }

        private void btnCerrarArticulo_Click(object sender, EventArgs e)
        {
            pnlArticulo.Visible=false;
        }

        private void btnFiltrarArticulos_Click(object sender, EventArgs e)
        {
            try
            {
                dgvArticulos.DataSource = NArticulo.Buscar(txtBuscarArticulo.Text.Trim());
                this.FormatoArticulos();
                lblTotalArticulos.Text = "Total Registros: " + Convert.ToString(dgvArticulos.Rows.Count);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int IdArticulo;
            string Codigo, Nombre;
            decimal Precio;
            IdArticulo = Convert.ToInt32(dgvArticulos.CurrentRow.Cells["ID"].Value);
            Codigo = Convert.ToString(dgvArticulos.CurrentRow.Cells["Codigo"].Value);
            Nombre = Convert.ToString(dgvArticulos.CurrentRow.Cells["Nombre"].Value);
            Precio = Convert.ToDecimal(dgvArticulos.CurrentRow.Cells["Precio_Venta"].Value);
            this.AgregarDetalle(IdArticulo,Codigo,Nombre,Precio);
        }

        private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataRow Fila = (DataRow)dtDetalle.Rows[e.RowIndex];
            decimal Precio = Convert.ToDecimal(Fila["precio"]);
            int Cantidad = Convert.ToInt32(Fila["cantidad"]);
            Fila["importe"] = Precio*Cantidad;
            this.CalcularTotales(); 

        }

        private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.CalcularTotales();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (txtIdProveedor.Text == String.Empty||txtImpuesto.Text==String.Empty||txtNumComprobante.Text ==string.Empty||dtDetalle.Rows.Count==0)
                {
                    this.MensajeError("Falta ingresar algunos datos, seran remarcados");
                    errorIcono.SetError(txtIdProveedor, "Seleccione un proveedor.");
                    errorIcono.SetError(txtImpuesto, "Ingrese el impuesto");
                    errorIcono.SetError(txtNumComprobante, "Ingrese el Nro de Comprobante");
                    errorIcono.SetError(dgvDetalle, "Ingrese al menos 1 detalle");
                }
                else
                {
                    Rpta = NIngreso.Insertar(Convert.ToInt32(txtIdProveedor.Text),Variables.IdUsuario,cboComprobante.Text, txtSerieComprobante.Text.Trim(),
                        txtNumComprobante.Text.Trim(),Convert.ToDecimal(txtImpuesto.Text),Convert.ToDecimal(txtTotal.Text),dtDetalle);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnFiltrarArticulos_Click_1(object sender, EventArgs e)
        {
            try
            {
                dgvArticulos.DataSource = NArticulo.Buscar(txtBuscarArticulo.Text.Trim());
                this.FormatoArticulos();
                lblTotalArticulos.Text = "Total Registros: " + Convert.ToString(dgvArticulos.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCerrarArticulo_Click_1(object sender, EventArgs e)
        {
            pnlArticulo.Visible = false;
        }

        private void dgvArticulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvArticulos_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int IdArticulo;
            string Codigo, Nombre;
            decimal Precio;
            IdArticulo = Convert.ToInt32(dgvArticulos.CurrentRow.Cells["ID"].Value);
            Codigo = Convert.ToString(dgvArticulos.CurrentRow.Cells["Codigo"].Value);
            Nombre = Convert.ToString(dgvArticulos.CurrentRow.Cells["Nombre"].Value);
            Precio = Convert.ToDecimal(dgvArticulos.CurrentRow.Cells["Precio_Venta"].Value);
            this.AgregarDetalle(IdArticulo, Codigo, Nombre, Precio);
        }
    }
}
