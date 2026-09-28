-- OPCIONAL: o importador já cria a coluna iIdOrigem sozinho se ela não existir.
-- Use este script só se preferir criar as colunas manualmente (ex.: DBA) antes de importar.
-- Rode no banco de DESTINO (new_ecdb).

ALTER TABLE `ec_tb_pais` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_pais_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_estado` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_estado_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_cidade` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_cidade_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_usuario` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_usuario_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_gre` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_gre_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_instituicao` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_instituicao_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_instituicao_usuario` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_instituicao_usuario_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_area_conhecimento` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_area_conhecimento_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_categoria` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_categoria_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_tema` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_tema_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_criterio` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_criterio_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_feira_afiliada` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_feira_afiliada_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_edital_feira` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_edital_feira_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_projeto` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_projeto_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_permissao` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_permissao_iIdOrigem` (`iIdOrigem`);
ALTER TABLE `ec_tb_app_permissao` ADD COLUMN `iIdOrigem` INT NULL COMMENT 'Id do registro no banco de origem', ADD INDEX `ix_ec_tb_app_permissao_iIdOrigem` (`iIdOrigem`);

-- Conferir se os Ids são AUTO_INCREMENT (a coluna EXTRA deve mostrar auto_increment):
SELECT TABLE_NAME, COLUMN_NAME, EXTRA FROM information_schema.COLUMNS
WHERE TABLE_SCHEMA = DATABASE() AND COLUMN_NAME = 'iId' AND TABLE_NAME LIKE 'ec_tb_%';

-- Exemplo de consulta: qual é o Id novo da cidade que tinha Id 402 no banco antigo?
SELECT iId, iIdOrigem, sNome FROM ec_tb_cidade WHERE iIdOrigem = 402;
