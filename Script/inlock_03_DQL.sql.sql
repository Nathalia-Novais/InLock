USE inlock_games_tarde;
GO

SELECT * FROM USUARIO
SELECT * FROM ESTUDIO
SELECT * FROM JOGO

UPDATE ESTUDIO SET nomeEstudio = 'teste 2' WHERE idEstudio= 5

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

SELECT idUsuario,ISNULL(USUARIO.idTipoUsuario,0),email,senha,titulo FROM USUARIO
     FULL JOIN TIPOUSUARIO 
	 ON TIPOUSUARIO.idTipoUsuario = USUARIO.idTipoUsuario


UPDATE USUARIO SET email = 'teste3' , senha = '@senha' WHERE idUsuario= 14


DELETE FROM USUARIO WHERE idUsuario = 3

 SELECT idUsuario,email,senha,titulo FROM USUARIO
                                          INNER JOIN TIPOUSUARIO ON TIPOUSUARIO.idTipoUsuario = USUARIO.idUsuario 
                                          WHERE idUsuario = 1
                 
                         