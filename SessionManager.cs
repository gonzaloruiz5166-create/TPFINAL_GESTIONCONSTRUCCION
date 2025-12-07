// ARCHIVO: SessionManager.cs (en el proyecto TP2)

using TP2.BE; // Para poder usar la clase Usuario

// La clase 'static' significa que solo hay una copia para toda la aplicación
public static class SessionManager
{
    // 1. Aquí se guarda el usuario que inició sesión. 'private set' lo protege.
    public static Usuario UsuarioActual { get; private set; }

    // 2. Método para guardar el usuario después del login
    public static void IniciarSesion(Usuario usuario)
    {
        UsuarioActual = usuario;
    }

    // 3. Método para preguntar: "¿El usuario actual tiene el rol X?"
    public static bool TieneRol(RolUsuario rolNecesario)
    {
        // Retorna verdadero si hay un usuario Y su rol coincide
        return UsuarioActual != null && UsuarioActual.Rol == rolNecesario;
    }
}