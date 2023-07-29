# API API_ProyectoFinallCodehouseJoseZaragoza

Esta es una API simple para gestionar usuarios/productos/ventas en una base de datos SQLServer.

## Descripción

La API API_ProyectoFinallCodehouseJoseZaragoza está construida en C# utilizando ASP.NET Web API y se utiliza para realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) en una tabla de usuarios/productos/ventas en una base de datos.
Fue realizada como proyecto de finalización del curso de Coderhouse C#, con los conocimientos adquiridos durante la cursada de la primera comisión del año 2023.

## Características
- Gestiona usuarios: permite obtener la lista de todos los usuarios, obtener un usuario por su ID, crear un nuevo usuario y modificar o eliminar un usuario existente.
- Gestiona productos: permite obtener la lista de todos los productos, obtener un producto por su ID, crear un nuevo producto y modificar o eliminar un producto existente.
- Gestiona ventas: permite obtener la lista de todas las ventas, obtener una venta por su ID y crear una nueva venta.
- Gestiona productos vendidos: permite obtener la lista de todos los productos vendidos y obtener un producto vendido por su ID.

## Requisitos
- Visual Studio 2019 (o una versión posterior) para la ejecución local.
- .NET Framework 4.7.2 (o una versión posterior).
- Postman para las pruebas
- 
## Instalación
0. Corre el archivo Script Creacion.sql" en sqlserver.
1. Clona este repositorio en tu máquina local utilizando el siguiente comando:

   ```
   git clone https://github.com/TuUsuario/API-UsuariosCH.git
   ```

2. Abre el proyecto en Visual Studio.

3. Restaura los paquetes NuGet si es necesario.
4. Coloca tu connectionString en el archivo Repository/DB_Helper.cs ```private readonly string connectionString = //Coloca aquí tu string de conección;```
4. Compila el proyecto y asegúrate de que no hay errores.

5. Ejecuta la aplicación en Visual Studio.

## Uso

Una vez que la API está en ejecución, puedes realizar solicitudes HTTP utilizando herramientas como Postman o cURL.

Ejemplos de endpoints:

## Endpoints de Usuario

- Obtener todos los usuarios:
  ```
  GET http://localhost:44304/Usuarios
  ```

- Obtener un usuario por su ID:
  ```
  GET http://localhost:44304/Usuarios/1
  ```

- Crear un nuevo usuario:
  ```
  POST http://localhost:44304/Usuarios/NuevoUsuario
  Body: {
      "Nombre": "NombreUsuario",
      "Apellido": "ApellidoUsuario",
      "NombreUsuario": "UsuarioNuevo",
      "Contraseña": "Contraseña123",
      "Mail": "usuario@mail.com"
  }
  ```

- Modificar un usuario existente:
  ```
  PUT http://localhost:44304/Usuarios/ModificarUsuario/1
  Body: {
      "Nombre": "NombreModificado",
      "Apellido": "ApellidoModificado",
      "NombreUsuario": "UsuarioModificado",
      "Contraseña": "NuevaContraseña123",
      "Mail": "usuario_modificado@mail.com"
  }
  ```

- Eliminar un usuario:
  ```
  DELETE http://localhost:44304/Usuarios/EliminarUsuario/1
  ```

## Endpoints de Producto

- Obtener todos los productos:
  ```
  GET http://localhost:44304/Producto
  ```

- Obtener un producto por su ID:
  ```
  GET http://localhost:44304/Producto/1
  ```

- Crear un nuevo producto:
  ```
  POST http://localhost:44304/Producto/NuevoProducto
  Body: {
      "Descripciones": "Descripción del producto",
      "Costo": 50.99,
      "IdUsuario": 1,
      "PrecioVenta": 99.99,
      "Stock": 100
  }
  ```

- Modificar un producto existente:
  ```
  PUT http://localhost:44304/Producto/ModificarProducto/1
  Body: {
      "Descripciones": "Nueva descripción del producto",
      "Costo": 60.99,
      "IdUsuario": 2,
      "PrecioVenta": 109.99,
      "Stock": 150
  }
  ```

- Eliminar un producto:
  ```
  DELETE http://localhost:44304/Producto/EliminarProducto/1
  ```

## Endpoints de Venta

- Obtener todos las ventas:
  ```
  GET http://localhost:44304/Venta
  ```

- Obtener una venta por su ID:
  ```
  GET http://localhost:44304/Venta/1
  ```

- Crear una nueva venta:
  ```
  POST http://localhost:44304/Venta/NuevaVenta
  Body: {
      "Comentarios": "Descripción de la venta",
      "IdUsuar": 1
  }
  ```

## Endpoints de ProductoVendido

- Obtener todos los productos vendidos:
  ```
  GET http://localhost:44304/ProductoVendido
  ```

- Obtener un producto vendido por su ID:
  ```
  GET http://localhost:44304/ProductoVendido/1
  ```

## Contribución

Si deseas contribuir a este proyecto, puedes enviar un pull request. Todas las contribuciones son bienvenidas.

## Autor

- josegabrielzaragoza

- 











Aquí está el README actualizado para la API con las funcionalidades adicionales:

# API UsuariosCH

Esta es una API simple para gestionar usuarios, productos, ventas y productos vendidos en una base de datos.

## Descripción

La API UsuariosCH está construida en C# utilizando ASP.NET Web API y se utiliza para realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) en diferentes tablas de la base de datos.

## Características

- Gestiona usuarios: permite obtener la lista de todos los usuarios, obtener un usuario por su ID, crear un nuevo usuario y modificar o eliminar un usuario existente.
- Gestiona productos: permite obtener la lista de todos los productos, obtener un producto por su ID, crear un nuevo producto y modificar o eliminar un producto existente.
- Gestiona ventas: permite obtener la lista de todas las ventas, obtener una venta por su ID y crear una nueva venta.
- Gestiona productos vendidos: permite obtener la lista de todos los productos vendidos y obtener un producto vendido por su ID.

## Requisitos

- Visual Studio 2019 (o una versión posterior) para la ejecución local.
- .NET Framework 4.7.2 (o una versión posterior).



## Endpoints de Usuario

- Obtener todos los usuarios:
  ```
  GET http://localhost:44304/Usuarios
  ```

- Obtener un usuario por su ID:
  ```
  GET http://localhost:44304/Usuarios/1
  ```

- Crear un nuevo usuario:
  ```
  POST http://localhost:44304/Usuarios/NuevoUsuario
  Body: {
      "Nombre": "NombreUsuario",
      "Apellido": "ApellidoUsuario",
      "NombreUsuario": "UsuarioNuevo",
      "Contraseña": "Contraseña123",
      "Mail": "usuario@mail.com"
  }
  ```

- Modificar un usuario existente:
  ```
  PUT http://localhost:44304/Usuarios/ModificarUsuario/1
  Body: {
      "Nombre": "NombreModificado",
      "Apellido": "ApellidoModificado",
      "NombreUsuario": "UsuarioModificado",
      "Contraseña": "NuevaContraseña123",
      "Mail": "usuario_modificado@mail.com"
  }
  ```

- Eliminar un usuario:
  ```
  DELETE http://localhost:44304/Usuarios/EliminarUsuario/1
  ```

## Endpoints de Producto

- Obtener todos los productos:
  ```
  GET http://localhost:44304/Producto
  ```

- Obtener un producto por su ID:
  ```
  GET http://localhost:44304/Producto/1
  ```

- Crear un nuevo producto:
  ```
  POST http://localhost:44304/Producto/NuevoProducto
  Body: {
      "Descripciones": "Descripción del producto",
      "Costo": 50.99,
      "IdUsuario": 1,
      "PrecioVenta": 99.99,
      "Stock": 100
  }
  ```

- Modificar un producto existente:
  ```
  PUT http://localhost:44304/Producto/ModificarProducto/1
  Body: {
      "Descripciones": "Nueva descripción del producto",
      "Costo": 60.99,
      "IdUsuario": 2,
      "PrecioVenta": 109.99,
      "Stock": 150
  }
  ```

- Eliminar un producto:
  ```
  DELETE http://localhost:44304/Producto/EliminarProducto/1
  ```

## Endpoints de Venta

- Obtener todos las ventas:
  ```
  GET http://localhost:44304/Venta
  ```

- Obtener una venta por su ID:
  ```
  GET http://localhost:44304/Venta/1
  ```

- Crear una nueva venta:
  ```
  POST http://localhost:44304/Venta/NuevaVenta
  Body: {
      "Comentarios": "Descripción de la venta",
      "IdUsuar": 1
  }
  ```

## Endpoints de ProductoVendido

- Obtener todos los productos vendidos:
  ```
  GET http://localhost:44304/ProductoVendido
  ```

- Obtener un producto vendido por su ID:
  ```
  GET http://localhost:44304/ProductoVendido/1
  ```

## Contribución

Si deseas contribuir a este proyecto, puedes enviar un pull request. Todas las contribuciones son bienvenidas.

## Autor

- Tu Nombre <tu@mail.com>

## Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

¡Gracias por utilizar la API UsuariosCH! Si tienes alguna pregunta o sugerencia, por favor no dudes en contactarnos.
