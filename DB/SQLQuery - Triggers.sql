USE Royal_Games;
GO

GO
CREATE TRIGGER trg_ExclusaoUsuario
ON Usuario
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Usuario
    SET StatusUsuario = 0
    WHERE UsuarioID IN (SELECT UsuarioID FROM deleted)
END
GO

GO
CREATE TRIGGER trg_ExclusaoJogo
ON Jogo
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Jogo
    SET StatusJogo = 0
    WHERE JogoID IN (SELECT JogoID FROM deleted)
END
GO

GO
CREATE TRIGGER trg_AlteracaoJogo
ON Jogo
AFTER UPDATE
AS
BEGIN
    INSERT INTO Log_AlteracaoJogo
    (
        DataAlteracao,
        NomeAnterior,
        PrecoAnterior,
        JogoID
    )
    SELECT
        GETDATE(),
        d.Nome,
        d.Preco,
        d.JogoID
    FROM deleted d
END
GO