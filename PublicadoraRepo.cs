using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGemes
{
    class PublicadoraRepo
    {
        private readonly string _connectionString;

        public PublicadoraRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Publicadora> TodasPublicadoras()
        {
            List<Publicadora> publicadoras = new List<Publicadora>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM publicadora";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        publicadoras.Add(new Publicadora
                        {
                            // Lê o ano da fundação como inteiro
                            fundacao = reader.IsDBNull(reader.GetOrdinal("fundacao")) ? string.Empty : reader.GetInt32("fundacao").ToString(),

                            // Lê o nome da publicadora
                            nome = reader.GetString("nome_publicadora"),

                            // Lê o ID da publicadora como inteiro
                            ID = reader.IsDBNull(reader.GetOrdinal("id_publicadora")) ? 0 : reader.GetInt32("id_publicadora")
                        });
                    }
                }
            }
            return publicadoras;
        }

        public int NovaPublicadora(Publicadora publicadora)
        {
            int affectedRows = -1;
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO publicadora (nome_publicadora, fundacao) VALUES (@nome, @fundacao)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", publicadora.nome);
                    command.Parameters.AddWithValue("@fundacao", publicadora.fundacao);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        public int AtualizarPublicadora(Publicadora publicadora)
        {
            int affectedRows = -1;
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE publicadora SET nome_publicadora = @nome, fundacao = @fundacao WHERE id_publicadora = @ID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", publicadora.nome);
                    command.Parameters.AddWithValue("@fundacao", publicadora.fundacao);
                    command.Parameters.AddWithValue("@ID", publicadora.ID);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        public int RemoverPublicadora(int ID)
        {
            int affectedRows = -1;
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM publicadora WHERE id_publicadora = @ID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }
    }
}
