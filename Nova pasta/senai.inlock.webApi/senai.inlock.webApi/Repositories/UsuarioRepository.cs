using Microsoft.AspNetCore.Authorization;
using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string stringConexao = "Data Source=DESKTOP-2U1VKIV\\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=senai@132";

        public void AtualizarIdUrl(int idUsuario, UsuarioDomain usuarioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE USUARIO SET email = @email ,senha = @senha WHERE idUsuario= @idUsuario";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@email", usuarioAtualizado.email);
                    cmd.Parameters.AddWithValue("@senha", usuarioAtualizado.senha);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            };
        }

        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string querySelect = @"   SELECT idUsuario,email,senha,titulo FROM USUARIO  FULL JOIN TIPOUSUARIO 
                                         ON TIPOUSUARIO.idTipoUsuario = USUARIO.idTipoUsuario                                         
                                          WHERE email = @email
                                          and senha = @senha ";

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain usuarioBuscado = new UsuarioDomain
                        {
                            idUsuario = Convert.ToInt32(rdr["idUsuario"]),
                            email = rdr["email"].ToString(),
                            senha = rdr["senha"].ToString(),

                            tipousuario = new TipoUsuarioDomain
                            {
                                titulo = rdr ["titulo"].ToString()
                            }
                        };

                        return usuarioBuscado;

                    }
                    return null;

                }
            }
        }
        public UsuarioDomain BuscarPorId(int idUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = @"SELECT idUsuario,email,senha,titulo FROM USUARIO  FULL JOIN TIPOUSUARIO 
                                         ON TIPOUSUARIO.idTipoUsuario = USUARIO.idTipoUsuario WHERE idUsuario = @idUsuario";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        UsuarioDomain usuarioBuscado = new UsuarioDomain
                        {
                            idUsuario = Convert.ToInt32(reader["idUsuario"]),

                            email = reader["email"].ToString(),

                            senha = reader["senha"].ToString(),

                            tipousuario = new TipoUsuarioDomain
                            {
                                titulo = reader["titulo"].ToString()
                            }            
                        };

                        return usuarioBuscado;
                    }

                    return null;
                }

            }
        }

        public void Cadastrar(UsuarioDomain novoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO USUARIO (idTipoUsuario,email,senha) VALUES (@idTipoUsuario,@email,@senha)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idTipoUsuario", novoUsuario.idTipoUsuario);
                    cmd.Parameters.AddWithValue("@email", novoUsuario.email);
                    cmd.Parameters.AddWithValue("@senha", novoUsuario.senha);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM USUARIO WHERE idUsuario = @idUsuario";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<UsuarioDomain> ListarTodos()
        {
            List<UsuarioDomain> listaUsuario = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = @"SELECT idUsuario,ISNULL(USUARIO.idTipoUsuario,0),email,senha,titulo FROM USUARIO
                                         FULL JOIN TIPOUSUARIO 
	                                     ON TIPOUSUARIO.idTipoUsuario = USUARIO.idTipoUsuario";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        UsuarioDomain usuario = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr[0]),

                            email = rdr[2].ToString(),

                            senha = rdr[3].ToString(),

                             tipousuario = new TipoUsuarioDomain()
                            {
                                idTipoUsuario = Convert.ToInt32(rdr[1]),
                                titulo = rdr[4].ToString()
                            }
                        };

                        listaUsuario.Add(usuario);
                    }
                }
            }

            return listaUsuario;
        }

    }
    
}
