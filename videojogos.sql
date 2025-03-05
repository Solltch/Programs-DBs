-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 04/03/2025 às 23:49
-- Versão do servidor: 10.4.32-MariaDB
-- Versão do PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `videojogos`
--

-- --------------------------------------------------------

--
-- Estrutura para tabela `criadores`
--

CREATE TABLE `criadores` (
  `id_criador` bigint(100) NOT NULL,
  `n_criador` varchar(100) NOT NULL,
  `d_nasc` date NOT NULL,
  `idade` bigint(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `criadores`
--

INSERT INTO `criadores` (`id_criador`, `n_criador`, `d_nasc`, `idade`) VALUES
(1, 'Rafael Lange', '1997-11-02', 28),
(2, 'Ari Gibson', '1883-05-14', 34),
(3, 'Jonatan Sõderstrom', '0000-00-00', 39),
(4, 'Dennis Wedin', '0000-00-00', 37),
(5, 'Toby FOX', '0000-00-00', 33),
(6, 'Hidetaka Miyazaki', '0000-00-00', 25),
(7, 'Marcus Person ', '0000-00-00', 45),
(8, 'Andrew Spinks', '0000-00-00', 41),
(9, 'Erick', '0000-00-00', 37);

-- --------------------------------------------------------

--
-- Estrutura para tabela `criador_jogo`
--

CREATE TABLE `criador_jogo` (
  `id_criador` bigint(100) NOT NULL,
  `id_jogo` bigint(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `jogos`
--

CREATE TABLE `jogos` (
  `id_jogo` bigint(100) NOT NULL,
  `nome_jogo` varchar(100) NOT NULL,
  `d_publi` date DEFAULT NULL,
  `id_motor` bigint(100) DEFAULT NULL,
  `desenvolvedora` varchar(100) NOT NULL,
  `id_publicadora` bigint(100) DEFAULT NULL,
  `genero` varchar(100) NOT NULL,
  `vendas` bigint(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `jogos`
--

INSERT INTO `jogos` (`id_jogo`, `nome_jogo`, `d_publi`, `id_motor`, `desenvolvedora`, `id_publicadora`, `genero`, `vendas`) VALUES
(1, 'Hollow Knight', '0000-00-00', 2, 'Team_Cherry', 1, 'Metroidvania', 21800000),
(2, 'Hotline Miami', '0000-00-00', 1, 'Dennaton Games', 2, 'Shoot em up', 130000),
(3, 'Undertale', '0000-00-00', 1, 'Toby Fox', NULL, 'RPG', 3000000),
(4, 'Dark Souks', '0000-00-00', 3, 'FromSoftWare', 3, 'RPG, Fantasia', 35180000),
(5, 'Minecraft', '0000-00-00', 4, 'Mojang Studios', 4, 'Sobrevivência, Sandbox', 300000000),
(6, 'Forager', '0000-00-00', 1, 'HopFrog', 5, 'Sobrevivência, Simulação', 2400000),
(7, 'Terraria', '0000-00-00', 5, 'Re-Logic', 6, 'RPG', 58000000),
(8, 'Stardew Valley', '0000-00-00', 6, 'MonoGame', 7, 'RPG, Fazenda', 41000000),
(9, 'Astroneer', '0000-00-00', 3, 'System Era Softworks', 8, 'Exploração, Simulação', 2000000),
(10, 'Ark Survival Evolved', '0000-00-00', 3, 'Unreal Engine', 9, 'Sobrevivência, MMO', 60000000),
(11, 'Dead Cells', '0000-00-00', 7, 'Motion Twin', NULL, 'Roguelike, Metroidvania', 2000000),
(12, 'Cuphead', '0000-00-00', 2, 'Studio MDHR', NULL, 'Run n Gun', 6000000),
(13, 'Celeste', '0000-00-00', 6, 'Maddy Makes Games', NULL, 'Plataforma', 4000000),
(14, 'Mouthwashing', '0000-00-00', 2, 'Wrong Organ', 10, 'Terror', 500000);

-- --------------------------------------------------------

--
-- Estrutura para tabela `motor`
--

CREATE TABLE `motor` (
  `id_motor` bigint(100) NOT NULL,
  `nome_motor` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `motor`
--

INSERT INTO `motor` (`id_motor`, `nome_motor`) VALUES
(1, 'Game maker'),
(2, 'Unity'),
(3, 'Unreal Engine'),
(4, 'Java Game Library '),
(5, 'Xna Game 4.0'),
(6, 'Mono Game'),
(7, 'Heaps.io');

-- --------------------------------------------------------

--
-- Estrutura para tabela `publicadora`
--

CREATE TABLE `publicadora` (
  `id_publicadora` bigint(100) NOT NULL,
  `nome_publicadora` varchar(100) DEFAULT NULL,
  `fundacao` year(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `publicadora`
--

INSERT INTO `publicadora` (`id_publicadora`, `nome_publicadora`, `fundacao`) VALUES
(1, 'Team Cherry', '2014'),
(2, 'Devolver Digital', '2009'),
(3, 'Bandai Namco', '2005'),
(4, 'Microsoft', '1975'),
(5, 'Humble Games', '2010'),
(6, 'Re-Logic', '2011'),
(7, 'Concerned Ape', '2014'),
(8, 'System Era Softworks', '2014'),
(9, 'Snail Games USA', '2009'),
(10, 'CRITICAL REFLEX', '2015');

--
-- Índices para tabelas despejadas
--

--
-- Índices de tabela `criadores`
--
ALTER TABLE `criadores`
  ADD PRIMARY KEY (`id_criador`);

--
-- Índices de tabela `criador_jogo`
--
ALTER TABLE `criador_jogo`
  ADD PRIMARY KEY (`id_criador`,`id_jogo`),
  ADD KEY `id_jogo` (`id_jogo`);

--
-- Índices de tabela `jogos`
--
ALTER TABLE `jogos`
  ADD PRIMARY KEY (`id_jogo`),
  ADD KEY `id_motor` (`id_motor`),
  ADD KEY `id_publicadora` (`id_publicadora`);

--
-- Índices de tabela `motor`
--
ALTER TABLE `motor`
  ADD PRIMARY KEY (`id_motor`);

--
-- Índices de tabela `publicadora`
--
ALTER TABLE `publicadora`
  ADD PRIMARY KEY (`id_publicadora`);

--
-- AUTO_INCREMENT para tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `criadores`
--
ALTER TABLE `criadores`
  MODIFY `id_criador` bigint(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de tabela `jogos`
--
ALTER TABLE `jogos`
  MODIFY `id_jogo` bigint(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de tabela `motor`
--
ALTER TABLE `motor`
  MODIFY `id_motor` bigint(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de tabela `publicadora`
--
ALTER TABLE `publicadora`
  MODIFY `id_publicadora` bigint(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Restrições para tabelas despejadas
--

--
-- Restrições para tabelas `criador_jogo`
--
ALTER TABLE `criador_jogo`
  ADD CONSTRAINT `criador_jogo_ibfk_1` FOREIGN KEY (`id_criador`) REFERENCES `criadores` (`id_criador`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `criador_jogo_ibfk_2` FOREIGN KEY (`id_jogo`) REFERENCES `jogos` (`id_jogo`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Restrições para tabelas `jogos`
--
ALTER TABLE `jogos`
  ADD CONSTRAINT `jogos_ibfk_1` FOREIGN KEY (`id_motor`) REFERENCES `motor` (`id_motor`),
  ADD CONSTRAINT `jogos_ibfk_2` FOREIGN KEY (`id_publicadora`) REFERENCES `publicadora` (`id_publicadora`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
