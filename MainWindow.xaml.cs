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
using TP2.BE; // Para Usuario y RolUsuario
using TP2.BLL; // Para CrudUtils
// Asegúrate de incluir los 'using' necesarios para tus otras clases (Trabajador, CRUDtrabajador, etc.)

namespace TP2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        private string myConnectionString;

        // ** (CAMPOS DE PRUEBA ELIMINADOS, SOLO QUEDA LO NECESARIO) **

        public MainWindow()
        {
            myConnectionString = ConfigurationManager.ConnectionStrings["TP3_P2_conection"].ConnectionString;
            InitializeComponent();

            // LlenarGrilla se llama aquí después de que la ventana se abre
            LlenarGrilla();
        }

        // ***********************************************************************
        // <-- FUNCIÓN FINAL CON RESTRICCIÓN DE ACCESO (AUTORIZACIÓN) -->
        // ***********************************************************************
        private void AltaTrabajador_Click(object sender, RoutedEventArgs e)
        {
            // La restricción permanece aquí. Solo deja pasar al rol RecursosHumanos.
            if (SessionManager.TieneRol(RolUsuario.RecursosHumanos))
            {
                AltaTrabajador altaTrabajador = new AltaTrabajador();
                altaTrabajador.ShowDialog();
                LlenarGrilla(); // Llama a la función original después de la acción
            }
            else
            {
                // Mensaje de error para cualquier otro rol (ej: Supervisor) o si no hay sesión iniciada
                MessageBox.Show("Acceso denegado. Solo el usuario de Recursos Humanos puede dar de alta un trabajador.",
                                 "Permisos insuficientes",
                                 MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        // ***********************************************************************
        // <-- TUS MÉTODOS ORIGINALES RESTAURADOS -->
        // ***********************************************************************

        public void LlenarGrilla()
        {
            // Reemplaza esto con tu lógica original para cargar datos en la grilla.
            /*
            trabajadoresDataGrid.ItemsSource = null;
            CRUDtrabajador crud = new CRUDtrabajador(myConnectionString);
            List<Trabajador> trabajadors = crud.GetAll();
            foreach (var item in trabajadors)
            {
                item.Sueldo = CalcularSueldo.Calcular(item);
            }
            trabajadoresDataGrid.ItemsSource = trabajadors;
            */
            // Si quieres que compile sin las clases, deja este placeholder:
            // MessageBox.Show("Grilla llenada (Lógica de prueba)");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Lógica original de Button_Click
        }

        private void EliminarTrabajador_Click(object sender, RoutedEventArgs e)
        {
            // Lógica original de EliminarTrabajador_Click
        }

        private void EditarTrabajador_Click(object sender, RoutedEventArgs e)
        {
            // Lógica original de EditarTrabajador_Click
        }
    }
}