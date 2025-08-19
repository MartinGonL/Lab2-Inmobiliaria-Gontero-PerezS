-- ========================================
-- Script de Base de Datos: Alquileres (Corregido)
-- Basado en la narrativa del Proyecto Inmobiliaria
-- ========================================

DROP DATABASE IF EXISTS alquileres_db;
CREATE DATABASE alquileres_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE alquileres_db;

-- ====================
-- Tabla Usuario
-- ====================
CREATE TABLE Usuario (
    id_usuario INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    email VARCHAR(150) UNIQUE NOT NULL,
    contrasena_hash VARCHAR(255) NOT NULL,
    rol ENUM('administrador', 'empleado') NOT NULL
);

-- ====================
-- Tabla Propietario
-- ====================
CREATE TABLE Propietario (
    id_propietario INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    dni VARCHAR(20) UNIQUE NOT NULL,
    telefono VARCHAR(20),
    direccion VARCHAR(255)
);

-- ====================
-- Tabla Inmueble
-- ====================
CREATE TABLE Inmueble (
    id_inmueble INT PRIMARY KEY AUTO_INCREMENT,
    id_propietario INT NOT NULL,
    direccion VARCHAR(255) NOT NULL,
    tipo VARCHAR(50),
    superficie_m2 DECIMAL(10,2),
    estado ENUM('disponible', 'suspendido') DEFAULT 'disponible',
    FOREIGN KEY (id_propietario) REFERENCES Propietario(id_propietario)
        ON DELETE CASCADE
);

-- ====================
-- Tabla Inquilino
-- ====================
CREATE TABLE Inquilino (
    id_inquilino INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    dni VARCHAR(20) UNIQUE NOT NULL,
    telefono VARCHAR(20),
    direccion VARCHAR(255)
);

-- ====================
-- Tabla Contrato
-- ====================
CREATE TABLE Contrato (
    id_contrato INT PRIMARY KEY AUTO_INCREMENT,
    id_inquilino INT NOT NULL,
    id_inmueble INT NOT NULL,
    monto_mensual DECIMAL(10,2) NOT NULL,
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NOT NULL,
    creado_por INT NOT NULL,
    terminado_por INT,
    FOREIGN KEY (id_inquilino) REFERENCES Inquilino(id_inquilino)
        ON DELETE CASCADE,
    FOREIGN KEY (id_inmueble) REFERENCES Inmueble(id_inmueble)
        ON DELETE CASCADE,
    FOREIGN KEY (creado_por) REFERENCES Usuario(id_usuario),
    FOREIGN KEY (terminado_por) REFERENCES Usuario(id_usuario)
);

-- ====================
-- Tabla Pago
-- ====================
CREATE TABLE Pago (
    id_pago INT PRIMARY KEY AUTO_INCREMENT,
    id_contrato INT NOT NULL,
    fecha_pago DATE NOT NULL,
    monto_pagado DECIMAL(10,2) NOT NULL,
    mes_correspondiente TINYINT NOT NULL,
    anio_correspondiente SMALLINT NOT NULL,
    estado ENUM('activo', 'anulado') DEFAULT 'activo',
    creado_por INT NOT NULL,
    anulado_por INT,
    FOREIGN KEY (id_contrato) REFERENCES Contrato(id_contrato)
        ON DELETE CASCADE,
    FOREIGN KEY (creado_por) REFERENCES Usuario(id_usuario),
    FOREIGN KEY (anulado_por) REFERENCES Usuario(id_usuario)
);

-- ====================
-- Datos iniciales
-- ====================
INSERT INTO Usuario (nombre, email, contrasena_hash, rol) VALUES
('Admin Juan', 'admin@example.com', 'hash123', 'administrador'),
('Empleado Pedro', 'pedro@example.com', 'hash456', 'empleado');

INSERT INTO Propietario (nombre, apellido, dni, telefono, direccion) VALUES
('Carlos', 'López', '12345678A', '555-1111', 'Av. Libertad 123'),
('María', 'Gómez', '87654321B', '555-2222', 'Calle Sol 456');

INSERT INTO Inmueble (id_propietario, direccion, tipo, superficie_m2, estado) VALUES
(1, 'Calle Luna 12', 'Departamento', 80.5, 'disponible'),
(1, 'Av. Estrella 45', 'Casa', 120.0, 'disponible'),
(2, 'Calle Mar 9', 'Local Comercial', 60.0, 'suspendido');

INSERT INTO Inquilino (nombre, apellido, dni, telefono, direccion) VALUES
('Luis', 'Pérez', '11223344C', '555-3333', 'Av. Roca 789'),
('Ana', 'Martínez', '44332211D', '555-4444', 'Calle Verde 101');

INSERT INTO Contrato (id_inquilino, id_inmueble, monto_mensual, fecha_inicio, fecha_fin, creado_por) VALUES
(1, 1, 500.00, '2025-01-01', '2025-12-31', 1),
(2, 3, 750.00, '2025-02-01', '2025-12-31', 2);

INSERT INTO Pago (id_contrato, fecha_pago, monto_pagado, mes_correspondiente, anio_correspondiente, creado_por) VALUES
(1, '2025-01-05', 500.00, 1, 2025, 2),
(1, '2025-02-05', 500.00, 2, 2025, 2),
(2, '2025-02-10', 750.00, 2, 2025, 1);
