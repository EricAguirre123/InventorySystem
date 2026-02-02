# InventorySystem
Este proyecto es una aplicación web para la gestión de inventarios, desarrollada como parte de una evaluación técnica.
La solución está construida con arquitectura de microservicios, usando .NET Core en el backend y Angular en el frontend, con una base de datos SQL Server.

Permite:

Gestionar productos

Registrar transacciones de compra y venta

Validar stock

Filtrar información de forma dinámica

 Requisitos

Antes de levantar el proyecto debemos tener instalado las siguientes herramientas:

.NET SDK 7 o superior

Node.js 18+

Angular CLI

SQL Server (LocalDB o SQL Server Express sirve)

Visual Studio 2022

Git

 Base de Datos 

En la raíz del proyecto se incluye el archivo:

database.sql


Este archivo contiene:

Creación de la base de datos

Tablas (Products, Transactions)

Relaciones (FOREIGN KEY)

Datos iniciales

 PASOS

Antes de ejecutar el backend:

Abrimos SQL Server Management Studio

Ejecutar el archivo database.sql

Verificar que existan las tablas y datos

Si no realizamos esto, el backend no va a funcionar.

 Ejecución del Backend (.NET)

El backend está dividido en dos microservicios:

ProductService

TransactionService

Pasos:

Abrir la solución en Visual Studio 2022

Verificar los connectionStrings en appsettings.json

Ejecutar ambos servicios (cada uno corre en un puerto distinto)

Swagger disponible en:

https://localhost:7102/swagger → Productos

https://localhost:7090/swagger → Transacciones

 Ejecución del Frontend (Angular)

Ir a la carpeta del frontend

Instalar dependencias:

npm install


Ejecutar la app:

ng serve


Abrir en el navegador:

http://localhost:4200

 Funcionalidades Implementadas
 Gestión de Productos

Crear producto

Editar producto

Eliminar producto

Listar productos en tabla dinámica

 Gestión de Transacciones

Registrar compras y ventas

Validación de stock (no permite vender más de lo disponible)

Ajuste automático de stock

Listado de transacciones

 Filtros Dinámicos

Filtrado por:

Rango de fechas

Tipo de transacción (Compra / Venta)

Producto

 Validaciones

Formularios reactivos

Campos obligatorios

Validación de stock en frontend y backend

 Evidencias

Las siguientes pantallas forman parte de la solución:

 Listado dinámico de productos
<img width="2080" height="826" alt="image" src="https://github.com/user-attachments/assets/36eb7ab7-e041-4651-afd3-38f6187b7996" />

 Listado dinámico de transacciones
<img width="1933" height="1066" alt="image" src="https://github.com/user-attachments/assets/2a4ab9e5-fe6e-4860-a714-05c8857eeb35" />

 Pantalla de creación de productos.
<img width="1469" height="875" alt="image" src="https://github.com/user-attachments/assets/912f5030-7702-40da-a8f7-1d42e48e82b4" />

 Pantalla de editar de productos.
<img width="1882" height="64" alt="image" src="https://github.com/user-attachments/assets/4527e8fa-5f53-4396-bc4b-4526a7b6ebb5" />
<img width="782" height="787" alt="image" src="https://github.com/user-attachments/assets/f5c28fa6-2936-432e-9782-b0bfe3e312a8" />
<img width="1911" height="71" alt="image" src="https://github.com/user-attachments/assets/8104f26a-7346-4b66-921f-3e69be255c02" />

 Pantalla de creación de transacciones.
<img width="849" height="790" alt="image" src="https://github.com/user-attachments/assets/c59270cf-4099-44dd-b96a-2c127babb08d" />

Pantalla de filtros dinámicos.
<img width="650" height="581" alt="image" src="https://github.com/user-attachments/assets/515801b3-9de2-4b28-a4ae-6b3b69e99707" />

<img width="595" height="591" alt="image" src="https://github.com/user-attachments/assets/f4d041de-717c-4e7c-b25b-bdd360571439" />

Base de Datos SQL:
<img width="1235" height="1112" alt="image" src="https://github.com/user-attachments/assets/377760af-738a-4153-b649-17f78d5e2bdf" />

La arquitectura está separada por responsabilidades

El stock se valida sí o sí antes de registrar una venta

El script SQL se entrega como pide la evaluación, sin atajos

 Autor

Evaluación desarrollada por Eric Aguirre
Fullstack Developer (.NET + Angular)
