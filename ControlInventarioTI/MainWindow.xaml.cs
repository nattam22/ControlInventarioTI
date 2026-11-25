using ControlInventarioTI.Properties;
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
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace ControlInventarioTI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn = new SqlConnection();
        public MainWindow()
        {
            InitializeComponent();
            conn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_InventarioTI;Integrated Security=True;";
            try
            {
                conn.Open();
                MessageBox.Show("Se establecio la conexion");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("No se pudo conectar");
                MessageBox.Show(ex.Message);
            }
        }

        private void Insertar_Click(object sender, RoutedEventArgs e)
        {
            Insertar wdwInsertar = new Insertar(conn);
            wdwInsertar.ShowDialog();
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            Eliminar wdwEliminar = new Eliminar(conn);
            wdwEliminar.ShowDialog();
        }

        private void Reporte_Click(object sender, RoutedEventArgs e)
        {
            Reporte wdwReporte = new Reporte(conn);
            wdwReporte.ShowDialog();

        }
    }
}
