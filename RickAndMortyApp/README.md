# Rick and Morty App

Una aplicación web en ASP.NET Core MVC que consume la [API de Rick and Morty](https://rickandmortyapi.com/) y permite visualizar, filtrar y paginar los personajes.

---

## Características

- Listado de personajes con imagen, nombre, estado, especie y género.
- Filtrado por:
  - **Nombre**
  - **Estado**: Alive, Dead, Unknown
  - **Especie**: Human, Robot, Alien, etc.
  - **Género**: Male, Female, Unknown
- Paginación con límite de 10 páginas visibles.
- Manejo de errores en la conexión a la API y en la deserialización de datos.
- Interfaz simple y responsiva usando Bootstrap.

---

## Estructura del proyecto

### Modelos

- `Character`: representa un personaje con sus propiedades principales (`Id`, `Name`, `Status`, `Species`, `Gender`, `Image`, etc.).
- `ApiResponse`: estructura para capturar la respuesta de la API (`Info` y `Results`).
- `ApiInfo`: información de paginación (`Count`, `Pages`, `Next`, `Prev`).
- `CharacterListViewModel`: modelo para la vista, contiene lista de personajes, filtros aplicados y paginación.

### Servicio

- `CharacterService`: encapsula la lógica para obtener todos los personajes de la API con paginación automática y manejo de errores.

### Controlador

- `CharactersController`:
  - Acción `Index`: obtiene los personajes según los filtros especificados.
  - Construye la URL con los parámetros recibidos.
  - Maneja errores de red y de deserialización.
  - Devuelve un modelo `CharacterListViewModel` para la vista.

### Vista

- `Index.cshtml`:  
  - Formulario para filtros (Name, Status, Species, Gender).  
  - Tabla que muestra los personajes.  
  - Paginación con enlaces "Previous" y "Next".  

---

## Configuracion

- Abrir el proyecto en Visual Studio 2022 o superior.
- Instaler el NuGet Newtonsoft.Json.
- Ejecutar el proyecto con IIS Express.

## Uso

- Accede a la página principal.
- Aplica filtros si deseas (Nombre, Estado, Especie, Género).
- Haz clic en Filter para actualizar la lista.
- Navega entre páginas usando la paginación.

## Notas técnicas

- La API utilizada: Rick and Morty .
- El proyecto está hecho en ASP.NET Core MVC.
- Se usa HttpClient para las llamadas a la API y Newtonsoft.Json para la deserialización.
- La vista utiliza Bootstrap para un diseño simple y responsivo.
