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
using ControlInventarioTI.Properties;

namespace ControlInventarioTI.Properties
{
    /// <summary>
    /// Lógica de interacción para Eliminar.xaml
    /// </summary>
    public partial class Eliminar : Window
    {
        SqlConnection conexion;
        public Eliminar(SqlConnection conn)
        {
            InitializeComponent();
            this.conexion = conn;
            Panel.Visibility = Visibility.Hidden;
        }

        private void btn_Eliminar(object sender, RoutedEventArgs e)
        {
            MessageBoxResult respuesta = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este registro permanentemente?",
                "Confirmar Eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );
            if (respuesta == MessageBoxResult.Yes)
            {
                try
                {
                    int Id = Convert.ToInt32(txtId.Text);
                    string sql = "DELETE FROM Equipos WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Id", Id);

                        int reg = cmd.ExecuteNonQuery();

                        if (reg > 0)
                        {
                            MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                            txtMarca.Text = "";
                            txtModelo.Text = "";
                            txtTipoEquipo.Text = "";
                            txtFechaCompra.Text = "";
                            txtPrecio.Text = "";
                            txtId.Text = "";
                            txtId.IsReadOnly = false;
                            Panel.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el registro.");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al eliminar el registro:\n" + ex.Message, "Error SQL", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd;
            txtMarca.Text = ""; txtModelo.Text = ""; txtTipoEquipo.Text = ""; txtFechaCompra.Text = ""; txtPrecio.Text = "";
            Panel.Visibility = Visibility.Collapsed;
            try
            {
                int Id = Convert.ToInt32(txtId.Text);
                string sql = "select * from Equipos where Id = " + Id;
                cmd = new SqlCommand(sql, conexion);
            }
            catch
            {
                MessageBox.Show("El ID debe ser numerico");
                return;
            }
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Panel.Visibility = Visibility.Visible;
                    txtMarca.Text = reader.GetString(1);
                    txtModelo.Text = reader.GetString(2);
                    txtTipoEquipo.Text = reader.GetString(3);
                    txtFechaCompra.Text = reader.GetString(4);
                    txtPrecio.Text = reader.GetDouble(5).ToString();

                    reader.Close(); txtId.IsReadOnly = true;
                }
                else
                {
                    MessageBox.Show("No existe ningun registro con ese ID\n\n");
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("No existe ningun registro con ese ID\n\n" + ex.Message);
            }
        }

        private void btn_Cancelar(object sender, RoutedEventArgs e)
        {
            Panel.Visibility = Visibility.Collapsed;
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtTipoEquipo.Text = "";
            txtFechaCompra.Text = "";
            txtPrecio.Text = "";
            txtId.Text = ""; txtId.IsReadOnly = false;
        }

       
    }
}