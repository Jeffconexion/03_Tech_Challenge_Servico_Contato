/*
Autor:Jefferson Santos

Data de Criação: 10/06/2024

Propósito:
Este script é destinado a simplificar o acesso a informações sobre contatos.

*/

-- CRIACIATION
CREATE DATABASE DB_FIAP_ARQUITETO

-- USER DATABASE
USE DB_FIAP_ARQUITETO

-- CREATIATION TABLES
CREATE TABLE TB_AREA_CODE(
	id_area_code UNIQUEIDENTIFIER NOT NULL,
	code_region INT NOT NULL,
	PRIMARY KEY(id_area_code)
)


CREATE TABLE TB_CONTACT(
	id_contact UNIQUEIDENTIFIER NOT NULL,
	[name] VARCHAR(100) NOT NULL,
	phone VARCHAR(20) NOT NULL,
	email VARCHAR(40),
	fk_id_area_code UNIQUEIDENTIFIER NOT NULL,
	FOREIGN KEY (fk_id_area_code) REFERENCES TB_AREA_CODE(id_area_code),
	PRIMARY KEY(id_contact)
)

-- Inserindo registros na tabela TB_AREA_CODE
INSERT INTO TB_AREA_CODE (id_area_code, code_region) VALUES
('550e8400-e29b-41d4-a716-446655440000', 1),
('550e8400-e29b-41d4-a716-446655440001', 2),
('550e8400-e29b-41d4-a716-446655440002', 3);

-- Inserindo registros na tabela TB_CONTACT
INSERT INTO TB_CONTACT (id_contact, [name], phone, email, fk_id_area_code) VALUES
('660e8400-e29b-41d4-a716-446655440000', 'Alice Johnson', '555-1234', 'alice.johnson@example.com', '550e8400-e29b-41d4-a716-446655440000'),
('660e8400-e29b-41d4-a716-446655440001', 'Bob Smith', '555-5678', 'bob.smith@example.com', '550e8400-e29b-41d4-a716-446655440001'),
('660e8400-e29b-41d4-a716-446655440002', 'Carol White', '555-9012', 'carol.white@example.com', '550e8400-e29b-41d4-a716-446655440002');

-- Inserindo um novo registro de contato para Bob Smith com outro telefone
INSERT INTO TB_CONTACT (id_contact, [name], phone, email, fk_id_area_code) VALUES
('660e8400-e29b-41d4-a716-446655440003', 'Bob Smith', '555-6789', 'bob.smith@example.com', '550e8400-e29b-41d4-a716-446655440001');


SELECT * FROM TB_AREA_CODE
SELECT * FROM TB_CONTACT 

SELECT c.name, c.phone, c.email, a.code_region FROM TB_CONTACT c
INNER JOIN TB_AREA_CODE a
ON a.id_area_code = c.AreaCodeId order by [name]