// ARCHIVO: LoginWindow.xaml.cs

using System;
using System.Configuration;
using System.Linq;
using System.Windows;
using TP2.BE; // Para Usuario y RolUsuario
using TP2.BLL; // Para CrudUtils

namespace TP2
{
    public partial class LoginWindow : Window
    {
        private string myConnectionString;

        public LoginWindow()
        {
            InitializeComponent();
            myConnectionString = ConfigurationManager.ConnectionStrings["TP3_P2_conection"].ConnectionString;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // 1. Obtener datos de la interfaz (A diferencia de la prueba, ahora los toma del TextBox)
            string nombreUsuario = txtUsuario.Text;
            string contrasena = txtPass.Password; // Usar .Password para PasswordBox

            try
            {
                CrudUtils crudUtils = new CrudUtils(myConnectionString);

                // 2. Llamada a la Autenticación
                Usuario usuarioLogueado = crudUtils.GetUsuarioPorCredenciales(nombreUsuario, contrasena);

                if (usuarioLogueado != null)
                {
                    // --- LOGIN EXITOSO ---
                    SessionManager.IniciarSesion(usuarioLogueado);
                    MessageBox.Show($"¡Bienvenido {usuarioLogueado.NombreUsuario}! Tu rol es: {usuarioLogueado.Rol.ToString()}", "Login Exitoso");

                    // 3. Navegación: Abrir la ventana principal y cerrar el Login
                    MainWindow principal = new MainWindow();
                    principal.Show();
                    this.Close(); // Cierra la ventana de login
                }
                else
                {
                    // --- LOGIN FALLIDO ---
                    MessageBox.Show("Usuario o Contraseña inválidos.", "Error de Credenciales", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar conectar o procesar: " + ex.Message, "Error grave", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
    }
}