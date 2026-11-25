using Microsoft.Reporting.WinForms;
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
using System.Windows.Forms;

namespace ControlInventarioTI
{
    /// <summary>
    /// Lógica de interacción para Reporte.xaml
    /// </summary>
    public partial class Reporte : Window
    {
        SqlConnection conexion;
        public Reporte(SqlConnection conn)
        {
            InitializeComponent();
            conexion = conn;
            IniciaVistaReporte();
        }
        private void IniciaVistaReporte()
        {
            
            var reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                Dock = DockStyle.Fill
            };
            
            reportViewer.LocalReport.ReportPath = @"repEquipos.rdlc";

            
            List<Equipo> ListaEquipos = conseguirDatos();
            System.Windows.MessageBox.Show(ListaEquipos.Count.ToString());
            reportViewer.LocalReport.ReportPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "repEquipos.rdlc");
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Equipos", ListaEquipos));

            reportViewer.LocalReport.Refresh();
            reportViewer.RefreshReport();

            contenedor.Child = reportViewer;
        }
        private List<Equipo> conseguirDatos()
        {
            List<Equipo> equipos = new List<Equipo>();
            string sql = "SELECT * FROM Equipos";
            SqlCommand cmd = new SqlCommand(sql, conexion);
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Equipo eq = new Equipo();
                        eq.Id = reader.GetInt32(0);
                        eq.marca = reader.GetString(1);
                        eq.modelo = reader.GetString(2);
                        eq.tipoEquipo = reader.GetString(3);
                        eq.fechaCompra = reader.GetString(4);
                        eq.Precio = reader.GetDouble(5);
                        equipos.Add(eq);
                    }
                }
            }
            catch (SqlException ex)
            {

            }
            return equipos;
        }
    }
}
