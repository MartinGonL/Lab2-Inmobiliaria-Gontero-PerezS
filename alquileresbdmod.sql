-- Script final sin creado_por/terminado_por en contrato y sin creado_por/anulado_por en pago

CREATE DATABASE IF NOT EXISTS alquileresbdmod CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE alquileresbdmod;

CREATE TABLE `usuario` (
  `id_usuario` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `email` varchar(150) NOT NULL,
  `contrasena_hash` varchar(255) NOT NULL,
  `rol` enum('administrador','empleado') NOT NULL,
  PRIMARY KEY (`id_usuario`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO `usuario` (`nombre`,`email`,`contrasena_hash`,`rol`) VALUES
('Admin Juan','admin@example.com','hash123','administrador'),
('Empleado Pedro','pedro@example.com','hash456','empleado');

CREATE TABLE `propietario` (
  `id_propietario` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `dni` varchar(20) NOT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_propietario`),
  UNIQUE KEY `dni` (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO `propietario` (`nombre`,`apellido`,`dni`,`telefono`,`direccion`) VALUES
('Carlos','López','12345678A','555-1111','Av. Libertad 123'),
('María','Gómez','87654321B','555-2222','Calle Sol 456'),
('nacho','torres','46041451','347777777','rivadavia1317');

CREATE TABLE `inquilino` (
  `id_inquilino` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `dni` varchar(20) NOT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_inquilino`),
  UNIQUE KEY `dni` (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO `inquilino` (`nombre`,`apellido`,`dni`,`telefono`,`direccion`) VALUES
('Luis','Pérez','11223344C','555-3333','Av. Roca 789'),
('Ana','Martínez','44332211D','555-4444','Calle Verde 101'),
('nacho','grimaraez','45666777','3471404444','rivadavia342');

CREATE TABLE `inmueble` (
  `id_inmueble` int(11) NOT NULL AUTO_INCREMENT,
  `id_propietario` int(11) NOT NULL,
  `direccion` varchar(255) NOT NULL,
  `tipo` enum('Casa','Departamento','Local de negocio','') DEFAULT NULL,
  `estado` enum('disponible','suspendido') DEFAULT 'disponible',
  PRIMARY KEY (`id_inmueble`),
  KEY `id_propietario` (`id_propietario`),
  CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`id_propietario`) REFERENCES `propietario` (`id_propietario`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO `inmueble` (`id_propietario`,`direccion`,`tipo`,`estado`) VALUES
(1,'Calle Luna 12','Departamento','disponible'),
(1,'Av. Estrella 45','Casa','disponible'),
(2,'Calle Mar 9','','suspendido');

CREATE TABLE `contrato` (
  `id_contrato` int(11) NOT NULL AUTO_INCREMENT,
  `id_inquilino` int(11) NOT NULL,
  `id_inmueble` int(11) NOT NULL,
  `monto_mensual` decimal(10,2) NOT NULL,
  `fecha_inicio` date NOT NULL,
  `fecha_fin` date NOT NULL,
  PRIMARY KEY (`id_contrato`),
  KEY `id_inquilino` (`id_inquilino`),
  KEY `id_inmueble` (`id_inmueble`),
  CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`id_inquilino`) REFERENCES `inquilino` (`id_inquilino`) ON DELETE CASCADE,
  CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`id_inmueble`) REFERENCES `inmueble` (`id_inmueble`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO `contrato` (`id_inquilino`,`id_inmueble`,`monto_mensual`,`fecha_inicio`,`fecha_fin`) VALUES
(1,1,500.00,'2025-01-01','2025-12-31'),
(2,3,750.00,'2025-02-01','2025-12-31');

CREATE TABLE `pago` (
  `id_pago` int(11) NOT NULL AUTO_INCREMENT,
  `id_contrato` int(11) NOT NULL,
  `fecha_pago` date NOT NULL,
  `monto_pagado` decimal(10,2) NOT NULL,
  `mes_correspondiente` tinyint(4) NOT NULL,
  `anio_correspondiente` smallint(6) NOT NULL,
  `estado` enum('activo','anulado') DEFAULT 'activo',
  PRIMARY KEY (`id_pago`),
  KEY `id_contrato` (`id_contrato`),
  CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`id_contrato`) REFERENCES `contrato` (`id_contrato`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO `pago` (`id_contrato`,`fecha_pago`,`monto_pagado`,`mes_correspondiente`,`anio_correspondiente`,`estado`) VALUES
(1,'2025-01-05',500.00,1,2025,'activo'),
(1,'2025-02-05',500.00,2,2025,'activo'),
(2,'2025-02-10',750.00,2,2025,'activo');
