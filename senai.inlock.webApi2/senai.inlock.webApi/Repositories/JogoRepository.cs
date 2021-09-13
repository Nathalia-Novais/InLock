using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Repositories
{
    public class JogoRepository : IJogoRepository

    {
        private string stringConexao = "Data Source= LAPTOP-FU757ILI\\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=SA; pwd=R260169p";
        //private string stringConexao = "Data Source=DESKTOP-2U1VKIV\\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=senai@132";

        public void AtualizarIdUrl(int idJogo, JogoDomain jogoAtualizado)
        {
            if (jogoAtualizado.nomeJogo != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateUrl = "UPDATE JOGO SET nomeJogo = @nomeJogo, descricao = @descricao , dataLancamento = @data, valor = @valor WHERE idJogo = @idJogo";

                    using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                    {
                        cmd.Parameters.AddWithValue("@nomeJogo", jogoAtualizado.nomeJogo);
                        cmd.Parameters.AddWithValue("@descricao", jogoAtualizado.descricao);
                        cmd.Parameters.AddWithValue("@data", jogoAtualizado.dataLancamento);
                        cmd.Parameters.AddWithValue("@valor", jogoAtualizado.valor);
                        cmd.Parameters.AddWithValue("@idJogo", jogoAtualizado.idJogo);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
                
        }

        public JogoDomain BuscarPorId(int idJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = @"SELECT idJogo, nomeJogo as JOGO, descricao, dataLancamento as dataLancamento, valor, nomeEstudio as ESTUDIO 
                                            FROM JOGO
                                            LEFT JOIN ESTUDIO
                                            ON JOGO.idEstudio = ESTUDIO.idEstudio
                                            WHERE idJogo = @idJogo
                                            ";


                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", idJogo);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        JogoDomain jogoBuscado = new JogoDomain
                        {
                            idJogo = Convert.ToInt32(reader[0]),

                            nomeJogo = reader[1].ToString(),
                            descricao = reader[2].ToString(),
                            dataLancamento = Convert.ToDateTime(reader[3]),

                            valor = reader[4].ToString(),

                            estudio = new EstudioDomain() { nomeEstudio = reader[5].ToString() },


                        };

                        return jogoBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(JogoDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO JOGO (nomeJogo, descricao, dataLancamento,valor,idEstudio) VALUES (@nomeJogo, @descricao, @dataLancamento, @valor, @idEstudio)";


                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    EstudioDomain e = new EstudioDomain();
                    cmd.Parameters.AddWithValue("@nomeJogo", novoJogo.nomeJogo);
                    cmd.Parameters.AddWithValue("@descricao", novoJogo.descricao);
                    cmd.Parameters.AddWithValue("@dataLancamento", novoJogo.dataLancamento);
                    cmd.Parameters.AddWithValue("@valor", novoJogo.valor);
                    cmd.Parameters.AddWithValue("@idEstudio", novoJogo.idEstudio);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM JOGO WHERE idjogo = @idJogo";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", idJogo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogoDomain> ListarTodos()
        {
            List<JogoDomain> listaJogo = new List<JogoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = @"SELECT * FROM JOGO";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        JogoDomain jogo = new JogoDomain()
                        {
                            idJogo = Convert.ToInt32(reader[0]),

                            nomeJogo = reader[1].ToString(),

                            descricao = reader[2].ToString(),
                            dataLancamento = Convert.ToDateTime(reader[3]),


                            valor = reader[4].ToString(),

                            estudio = new EstudioDomain() { nomeEstudio = reader[5].ToString() },

                        };

                        listaJogo.Add(jogo);
                    }
                }
            }

            return listaJogo;
        }
    }
}
