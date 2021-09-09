USE inlock_games_tarde;
GO

SELECT * FROM USUARIO
SELECT * FROM ESTUDIO
SELECT * FROM JOGO

SELECT nomeJogo,nomeEstudio FROM JOGO
LEFT JOIN ESTUDIO
ON JOGO.idJogo = ESTUDIO.idEstudio

SELECT nomeJogo,nomeEstudio FROM ESTUDIO
LEFT JOIN JOGO
ON JOGO.idJogo = ESTUDIO.idEstudio

SELECT email,senha FROM USUARIO
WHERE email = 'admin@admin.com' and senha='admin'

SELECT * FROM JOGO
WHERE idJogo = '2'

SELECT * FROM ESTUDIO
WHERE idEstudio = '2'
