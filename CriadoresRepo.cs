using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGemes
{
    class CriadoresRepo
    {
        private readonly string _connectionString;

        public CriadoresRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Criadores> TodosCriadores()
        {
            List<Criadores> criadores = new List<Criadores>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM criadores";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        criadores.Add(new Criadores
                        {
                            nome = reader.GetString("n_criador"),
                            ID = reader.GetInt32("id_criador"),
                            nascimento = reader.GetDateTime("d_nasc"),
                            idade = reader.GetInt32("idade")
                        });
                    }
                }
            }
            return criadores;
        }

        public int NovoCriador(Criadores criadores)
        {
            int affectedRows = -1;
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO criadores (n_criador, d_nasc, idade) VALUES (@nome, @nascimento, @idade)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", criadores.nome);
                    command.Parameters.AddWithValue("@nascimento", criadores.nascimento);
                    command.Parameters.AddWithValue("@idade", criadores.idade);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        public int AtualizarCriador(Criadores criador)
        {
            int affectedRows = -1;
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE criadores SET n_criador = @nome, d_nasc = @nascimento, idade = @idade WHERE id_criador = @id_criador;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", criador.nome);
                    command.Parameters.AddWithValue("@ID", criador.ID);
                    command.Parameters.AddWithValue("@nascimento", criador.nascimento);
                    command.Parameters.AddWithValue("@idade", criador.idade);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        public List<(string Criador, string jogo)> CriadoresPorJogo()
        {
            List<(string, string)> criadores = new();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT c.n_criador, j.nome_jogo FROM jogos j, criadores c, criador_jogo cj WHERE c.id_criador = cj.id_criador AND cj.id_jogo = j.id_jogo";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string criador = reader.GetString("n_criador");
                        string jogo = reader.GetString("nome_jogo");
                        criadores.Add((criador, jogo));
                    }
                }
            }
            return criadores;
        }

    }
}

