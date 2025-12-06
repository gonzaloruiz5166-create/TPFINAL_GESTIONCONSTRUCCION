using System;
using System.Collections.Generic;
using System.Configuration;
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
using TP2.BE;
using TP2.BLL;

namespace TP2
{
    /// <summary>
    /// Lógica de interacción para AltaTrabajador.xaml
    /// </summary>
    public partial class AltaTrabajador : Window
    {

        private string myConnectionString;
        private List<Rango> rangos;
        private List<Categoria> categorias;
        public AltaTrabajador()
        {
            myConnectionString = ConfigurationManager.ConnectionStrings["TP3_P2_conection"].ConnectionString;
           CrudUtils crudUtils = new CrudUtils(myConnectionString);
            rangos = crudUtils.GetRangos();
            categorias = crudUtils.GetCategorias();
           
            InitializeComponent();
            cmbCategoria.ItemsSource = categorias;
            cmbCategoria.DisplayMemberPath = "Nombre";  // Mostrar la propiedad 'Nombre'
            cmbCategoria.SelectedValuePath = "Id";
            cmbRango.ItemsSource = rangos;
            cmbRango.DisplayMemberPath = "Nombre";  // Mostrar la propiedad 'Nombre'
            cmbRango.SelectedValuePath = "Id";
        }

        private void CallMainWindows()
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.LlenarGrilla();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CRUDtrabajador crud = new CRUDtrabajador(myConnectionString);
                Trabajador trabajador = new Trabajador();
                trabajador.Apellido = txtApellido.Text;
                trabajador.Nombre = txtNombre.Text;
                trabajador.Domicilio = txtDomicilio.Text;
                trabajador.Localidad = txtLocalidad.Text;
                trabajador.Provincia = cmbProvincia.Text;
                trabajador.NroCelular = Convert.ToInt32(txtNroCelular.Text);
                trabajador.Categoria = new Categoria { Id = (int)cmbCategoria.SelectedValue };
                trabajador.Rango = new Rango { Id = (int)cmbRango.SelectedValue };
                trabajador.AreaTrabajo = txtAreaTrabajo.Text;
                trabajador.FechaIngreso = Convert.ToDateTime(dpFechaIngreso.Text);
                crud.Insert(trabajador);
                MessageBox.Show("Trabajador Agregado con exito");
                CallMainWindows();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrio un error:" + ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
