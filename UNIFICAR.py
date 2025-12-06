import os

# Extensiones de archivos que queremos leer (ADAPTADO PARA C#)
extensiones_validas = [
    ".cs",       # <--- EL MÁS IMPORTANTE (Tu código C#)
    ".config",   # <--- Para App.config o Web.config
    ".json",     # <--- Para appsettings.json
    ".cshtml",   # <--- Si usas MVC o Razor Pages
    ".xml",      # <--- Configuraciones varias
    ".sql",      # <--- Por si tienes scripts de base de datos
    ".csproj",   # <--- Para ver la estructura del proyecto
]

# Carpetas a ignorar (ADAPTADO PARA VISUAL STUDIO)
carpetas_ignoradas = [
    "bin",          # <--- Basura compilada
    "obj",          # <--- Archivos temporales
    ".vs",          # <--- Configuración oculta de Visual Studio
    "packages",     # <--- Paquetes Nuget descargados
    "node_modules",
    ".git",
    "dist",
    "build",
    "Properties",   # Opcional: a veces solo tiene el AssemblyInfo
]

# Archivos específicos a ignorar
archivos_ignorados_especificos = [
    "unificar_codigo.py", 
    "PROYECTO_COMPLETO.txt", 
]

nombre_salida = "PROYECTO_COMPLETO.txt"

nombre_script_actual = os.path.basename(__file__)
archivos_ignorados_especificos.append(nombre_script_actual)

with open(nombre_salida, "w", encoding="utf-8") as outfile:
    for root, dirs, files in os.walk("."):
        # Filtrar carpetas ignoradas
        dirs[:] = [d for d in dirs if d not in carpetas_ignoradas]

        for file in files:
            if file in archivos_ignorados_especificos:
                continue

            # Verificar extensión
            if any(file.endswith(ext) for ext in extensiones_validas):
                ruta_completa = os.path.join(root, file)

                outfile.write(f"\n{'=' * 50}\n")
                outfile.write(f"ARCHIVO: {ruta_completa}\n")
                outfile.write(f"{'=' * 50}\n")

                try:
                    with open(ruta_completa, "r", encoding="utf-8") as infile:
                        outfile.write(infile.read())
                except Exception as e:
                    outfile.write(f"[Error leyendo archivo: {e}]\n")

print(f"¡Listo! Archivo generado: {nombre_salida}")