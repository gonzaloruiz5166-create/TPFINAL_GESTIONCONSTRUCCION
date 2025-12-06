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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TP2.BE;
using TP2.BLL;

namespace TP2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>  
    public partial class MainWindow : Window
    {

        private string myConnectionString;
        public MainWindow()
        {
            myConnectionString = ConfigurationManager.ConnectionStrings["TP3_P2_conection"].ConnectionString;
            InitializeComponent();
            LlenarGrilla();
        }

        private void AltaTrabajador_Click(object sender, RoutedEventArgs e)
        {
            AltaTrabajador altaTrabajador = new AltaTrabajador();
            altaTrabajador.ShowDialog();
        }
        public void LlenarGrilla()
        {
            trabajadoresDataGrid.ItemsSource = null;
            CRUDtrabajador crud = new CRUDtrabajador(myConnectionString);
            List<Trabajador> trabajadors = crud.GetAll();
            foreach (var item in trabajadors)
            {
                item.Sueldo = CalcularSueldo.Calcular(item);
            }
            trabajadoresDataGrid.ItemsSource = trabajadors;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var trabajadorSeleccionado = trabajadoresDataGrid.SelectedItem as Trabajador;

            if (trabajadorSeleccionado != null)
            {
                // Abrir la ventana de CargarHoras y pasar el trabajador seleccionado
                CargarHorasSueldo cargarHorasWindow = new CargarHorasSueldo(trabajadorSeleccionado);
                cargarHorasWindow.Show();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un trabajador.");
            }
        }

        private void EliminarTrabajador_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var trabajador = button.DataContext as Trabajador;
            bool result = MessageBox.Show("¿Está seguro que desea eliminar al trabajador?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (result)
            {
                CRUDtrabajador crud = new CRUDtrabajador(myConnectionString);
                crud.Delete(trabajador);
                LlenarGrilla();
                MessageBox.Show("Trabajador eliminado correctamente.");
            }
        }

        private void EditarTrabajador_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var trabajador = button.DataContext as Trabajador;

            EditarTrabajador editarTrabajador = new EditarTrabajador(trabajador);
            editarTrabajador.ShowDialog();
           
           /** if (result)
            {
                CRUDtrabajador crud = new CRUDtrabajador(myConnectionString);
                crud.Delete(trabajador);
                LlenarGrilla();
                MessageBox.Show("Trabajador eliminado correctamente.");
            } */
        }
    }

}
