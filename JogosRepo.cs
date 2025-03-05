using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGemes
{
    class JogosRepo
    {
        private readonly string _connectionString;

        public JogosRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Jogos> TodosJogos()
        {
            List<Jogos> jogos = new List<Jogos>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM jogos";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        jogos.Add(new Jogos
                        {
                            Developer = reader.GetString("desenvolvedora"),
                            nome = reader.GetString("nome_jogo"),
                            ID = reader.GetInt32("id_jogo"),
                            ID_motor = reader.IsDBNull(reader.GetOrdinal("id_motor")) ? 0 : reader.GetInt32("id_motor"),
                            ID_publi = reader.IsDBNull(reader.GetOrdinal("id_publicadora")) ? 0 : reader.GetInt32("id_publicadora"),
                            genero = reader.GetString("genero"),
                            release_date = reader.GetDateTime("d_publi").ToString("yyyy-MM-dd"),
                            Vendas = reader.GetInt32("vendas")
                        });
                    }
                }
            }
            return jogos;
        }

        public int NovaJogo(Jogos jogos)
        {
            int affectedRows = 0;

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO jogos (nome_jogo, d_publi, id_motor, desenvolvedora, id_publicadora, genero, vendas) 
                                 VALUES (@nome, @lancamento, @id_motor, @desenvolvedora, @id_publicadora, @genero, @vendas)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", jogos.nome);
                    command.Parameters.AddWithValue("@lancamento", string.IsNullOrEmpty(jogos.release_date) ? DBNull.Value : (object)DateTime.Parse(jogos.release_date));
                    command.Parameters.AddWithValue("@id_motor", jogos.ID_motor);
                    command.Parameters.AddWithValue("@desenvolvedora", jogos.Developer);
                    command.Parameters.AddWithValue("@id_publicadora", jogos.ID_publi);
                    command.Parameters.AddWithValue("@genero", jogos.genero);
                    command.Parameters.AddWithValue("@vendas", jogos.Vendas);

                    affectedRows = command.ExecuteNonQuery();
                }
            }

            return affectedRows;
        }

        public int AtualizarJogo(Jogos jogos)
        {
            int affectedRows = -1;
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE jogos SET nome_jogo = @nome, d_publi = @lancamento, id_motor = @id_engine, desenvolvedora = @desenvolvedora, id_publicadora = @ID_publi, genero = @genero, vendas = @vendas WHERE id_jogo = @ID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", jogos.nome);
                    command.Parameters.AddWithValue("@ID", jogos.ID);
                    command.Parameters.AddWithValue("@lancamento", jogos.release_date);
                    command.Parameters.AddWithValue("@ID_publi", jogos.ID_publi);
                    command.Parameters.AddWithValue("@desenvolvedora", jogos.Developer);
                    command.Parameters.AddWithValue("@id_engine", jogos.ID_motor);
                    command.Parameters.AddWithValue("@genero", jogos.genero);
                    command.Parameters.AddWithValue("@vendas", jogos.Vendas);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        public int RemoverJogo(int ID)
        {
            int affectedRows = -1;
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM jogos WHERE id_jogo = @ID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        public List<Jogos> JogosPorVenda()
        {
            List<Jogos> jogos = new List<Jogos>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM jogos ORDER BY vendas DESC";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        jogos.Add(new Jogos
                        {
                            Developer = reader.GetString("desenvolvedora"),
                            nome = reader.GetString("nome_jogo"),
                            ID = reader.GetInt32("id_jogo"),
                            ID_motor = reader.IsDBNull(reader.GetOrdinal("id_motor")) ? 0 : reader.GetInt32("id_motor"),
                            ID_publi = reader.IsDBNull(reader.GetOrdinal("id_publicadora")) ? 0 : reader.GetInt32("id_publicadora"),
                            genero = reader.GetString("genero"),
                            release_date = reader.GetDateTime("d_publi").ToString("yyyy-MM-dd"),
                            Vendas = reader.GetInt32("vendas")
                        });
                    }
                }
            }
            return jogos;
        }

        public List<Jogos> JogosPorGenero(string genero)
        {
            List<Jogos> jogos = new List<Jogos>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM jogos WHERE genero LIKE @genero";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@genero", "'%" + genero + "%'"); // Corrigido com % no parâmetro
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jogos.Add(new Jogos
                            {
                                Developer = reader.GetString("desenvolvedora"),
                                nome = reader.GetString("nome_jogo"),
                                ID = reader.GetInt32("id_jogo"),
                                ID_motor = reader.IsDBNull(reader.GetOrdinal("id_motor")) ? 0 : reader.GetInt32("id_motor"),
                                ID_publi = reader.IsDBNull(reader.GetOrdinal("id_publicadora")) ? 0 : reader.GetInt32("id_publicadora"),
                                genero = reader.GetString("genero"),
                                release_date = reader.GetDateTime("d_publi").ToString("yyyy-MM-dd"),
                                Vendas = reader.GetInt32("vendas")
                            });
                        }
                    }
                }
            }
            return jogos;
        }

        public bool JogoExiste(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM jogos WHERE id_jogo = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }
    }
}

