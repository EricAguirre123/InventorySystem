/* ============================================
   DATABASE SCRIPT
   Evaluación Técnica Fullstack
   ============================================ */

-- Crear base de datos
IF DB_ID('InventorySystem') IS NULL
BEGIN
    CREATE DATABASE InventorySystem;
END
GO

USE InventorySystem;
GO

/* ============================================
   TABLA: Products
   ============================================ */
IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL
    DROP TABLE dbo.Products;
GO

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Description NVARCHAR(500) NULL,
    Category NVARCHAR(100) NULL,
    ImageUrl NVARCHAR(300) NULL,
    Price DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

/* ============================================
   TABLA: Transactions
   ============================================ */
IF OBJECT_ID('dbo.Transactions', 'U') IS NOT NULL
    DROP TABLE dbo.Transactions;
GO

CREATE TABLE Transactions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TransactionDate DATETIME2 NOT NULL,
    TransactionType NVARCHAR(20) NOT NULL, -- Compra / Venta
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    TotalPrice DECIMAL(18,2) NOT NULL,
    Detail NVARCHAR(300) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Transactions_Products
        FOREIGN KEY (ProductId)
        REFERENCES Products(Id)
);
GO

/* ============================================
   DATOS INICIALES - Products
   ============================================ */
INSERT INTO Products (Name, Description, Category, ImageUrl, Price, Stock)
VALUES
('Laptop Lenovo', 'Laptop Lenovo Core i5', 'Tecnología', NULL, 800.00, 20),
('Mouse Logitech', 'Mouse inalámbrico', 'Accesorios', NULL, 25.00, 50),
('Teclado Mecánico', 'Teclado mecánico RGB', 'Accesorios', NULL, 90.00, 30);
GO

/* ============================================
   DATOS INICIALES - Transactions
   ============================================ */
INSERT INTO Transactions
(TransactionDate, TransactionType, ProductId, Quantity, UnitPrice, TotalPrice, Detail)
VALUES
(GETDATE(), 'Venta', 1, 2, 800.00, 1600.00, 'Venta inicial de laptop'),
(GETDATE(), 'Compra', 2, 10, 25.00, 250.00, 'Ingreso de mouse');
GO

/* ============================================
   VERIFICACIÓN RÁPIDA QUE FUE CREADO LOS PRODUCTOS Y TRANSACCIONES DE MANERA CORRECTA 
   ============================================ */
SELECT * FROM Products;
SELECT * FROM Transactions;
GO
