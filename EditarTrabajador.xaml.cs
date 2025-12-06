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
    public partial class EditarTrabajador : Window
    {
        private string myConnectionString;
        private List<Rango> rangos;
        private List<Categoria> categorias;
        private Trabajador trabajador; // Trabajador original a editar
        public EditarTrabajador(Trabajador trabajador)
        {
            InitializeComponent();
            myConnectionString = ConfigurationManager.ConnectionStrings["TP3_P2_conection"].ConnectionString;
            CrudUtils crudUtils = new CrudUtils(myConnectionString);

            rangos = crudUtils.GetRangos();
            categorias = crudUtils.GetCategorias();

            // Asigna el trabajador pasado al formulario a la variable de clase
            this.trabajador = trabajador;

            // Configuración de comboboxes
            cmbCategoria.ItemsSource = categorias;
            cmbCategoria.DisplayMemberPath = "Nombre";
            cmbCategoria.SelectedValuePath = "Id";
            cmbRango.ItemsSource = rangos;
            cmbRango.DisplayMemberPath = "Nombre";
            cmbRango.SelectedValuePath = "Id";

            // Llenado de campos del formulario con los datos del trabajador
            txtApellido.Text = trabajador.Apellido;
            txtNombre.Text = trabajador.Nombre;
            txtDomicilio.Text = trabajador.Domicilio;
            txtLocalidad.Text = trabajador.Localidad;
            cmbProvincia.Text = trabajador.Provincia;
            txtNroCelular.Text = trabajador.NroCelular.ToString();
            cmbCategoria.SelectedValue = trabajador.Categoria?.Id;
            cmbRango.SelectedValue = trabajador.Rango?.Id;
            txtAreaTrabajo.Text = trabajador.AreaTrabajo;
            dpFechaIngreso.Text = trabajador.FechaIngreso.ToShortDateString();
        }

        private void CallMainWindows()
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.LlenarGrilla();
            }
        }

        private void OnClickGuardarTrabajador(object sender, RoutedEventArgs e)
        {
            try
            {
                CRUDtrabajador crud = new CRUDtrabajador(myConnectionString);

                // Asigna los valores modificados al objeto trabajador original
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
                Console.WriteLine(trabajador.FechaIngreso.ToShortDateString());
                // Llama a Update con el trabajador actualizado
                crud.UpdateTrabajador(trabajador);

                MessageBox.Show("Los cambios se realizaron con éxito");
                CallMainWindows();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
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
