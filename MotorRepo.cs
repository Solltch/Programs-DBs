using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGemes
{
    class MotorRepo
    {
        private readonly string _connectionString;

        public MotorRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Motor> TodasEngine()
        {
            List<Motor> engines = new List<Motor>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM motor";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        engines.Add(new Motor
                        {
                            nome = reader.GetString("nome_motor"),
                            ID = reader.GetInt32("id_motor")
                        });
                    }
                }
            }
            return engines;
        }

        public int NovaEngine(Motor engine)
        {
            int affectedRows = -1;
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO motor (nome_motor) VALUES (@nome)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", engine.nome);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        public int AtualizarEngine(Motor engine)
        {
            int affectedRows = -1;
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE motor SET nome_motor = @nome WHERE id_motor = @ID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", engine.nome);
                    command.Parameters.AddWithValue("@ID", engine.ID);
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }
        
    }
}
    
