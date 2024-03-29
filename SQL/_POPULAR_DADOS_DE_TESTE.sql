USE [CRM_BLUE]
GO
/****** Object:  StoredProcedure [dbo].[_POPULAR_DADOS_DE_TESTE]    Script Date: 05/11/2019 17:36:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[_POPULAR_DADOS_DE_TESTE] AS
BEGIN

	SET NOCOUNT ON

	DECLARE @i INT

	--USUARIO
	INSERT INTO USUARIO VALUES('Testador', 'testador@bs.com', '123')

	--AGENCIA
	SET @i = 0
	WHILE @i < 5
	BEGIN
		SET @i = @i + 1
		INSERT INTO AGENCIA VALUES(CONCAT('Agencia ',@i))
	END

	--CLIENTE
	SET @i = 0
	WHILE @i < 10
	BEGIN
		SET @i = @i + 1
		INSERT INTO CLIENTE VALUES(
			(SELECT TOP 1 AGENCIA_ID FROM AGENCIA ORDER BY NEWID()),
			CONCAT('Cliente ',@i)
		)
	END

	--PROJETO
	SET @i = 0
	WHILE @i < 500
	BEGIN
		SET @i = @i + 1
		INSERT INTO PROJETO VALUES(
			(SELECT TOP 1 USUARIO_ID FROM USUARIO ORDER BY NEWID()),
			(SELECT TOP 1 AGENCIA_ID FROM AGENCIA ORDER BY NEWID()), 
			(SELECT TOP 1 CLIENTE_ID FROM CLIENTE ORDER BY NEWID()), 
			CONCAT('Projeto ',@i),
			CONCAT('Descrição do Projeto ',@i),
			(RAND()*(20-5)+5)*1000,
			CONCAT('Solicitante ',@i),
			1,
			DATEADD(DAY, (ABS(CHECKSUM(NEWID()) % 365) * (-1)), GETDATE()),
			NULL,
			1,
			30,
			15,
			20,
			100,
			10,
			GETDATE()
		)
	END

END

