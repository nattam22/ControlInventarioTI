using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ControlInventarioTI.Properties
{
    /// <summary>
    /// Lógica de interacción para Insertar.xaml
    /// </summary>
    public partial class Insertar : Window
    {
        SqlConnection conn;
        public Insertar(SqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void btn_Guardar(object sender, RoutedEventArgs e)
        {
            int Id;
            double precio;
            try
            {
                Id = Convert.ToInt32(txtId.Text);
                string marca = txtMarca.Text;
                string modelo = txtModelo.Text;
                string tipoEquipo = txtTipoEquipo.Text;
                string fechaCompra = txtFechaCompra.Text;
                precio = Convert.ToDouble(txtPrecio.Text);
                string sql = "INSERT INTO EQUIPOS VALUES (@Id, @marca, @modelo, @tipoEquipo, @fechaCompra, @precio)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@marca", marca);
                    cmd.Parameters.AddWithValue("@modelo", modelo);
                    cmd.Parameters.AddWithValue("@tipoEquipo", tipoEquipo);
                    cmd.Parameters.AddWithValue("@fechaCompra", fechaCompra);
                    cmd.Parameters.AddWithValue("@precio", precio);

                    try
                    {
                        int reg = cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro insertado correctamente.");
                        this.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error en la base de datos:\n" + ex.Message);
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor ingresa números válidos en ID y precio.");
            }
           
        }

        private void btn_Cancelar(object sender, RoutedEventArgs e)
        {
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtTipoEquipo.Text = "";
            txtFechaCompra.Text = "";
            txtPrecio.Text = "";
            txtId.Text = ""; txtId.IsReadOnly = false;
        }
    }
}