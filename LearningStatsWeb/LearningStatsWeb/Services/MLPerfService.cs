using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningStatsWeb.Services
{
    public class MLPerfService
    {
        public string ConnectionString { get; set; }

        public MLPerfService(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void InsertValue(int step, float value, DateTime insertedOn, string sessionName)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (MySqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "insert into status (Value, Step, InsertedOn, SessionName) values (@value, @step, @insertedon, @sessionName)";
                    command.Parameters.AddWithValue("value", value);
                    command.Parameters.AddWithValue("step", step);
                    command.Parameters.AddWithValue("insertedon", insertedOn);
                    command.Parameters.AddWithValue("sessionName", sessionName);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<string> GetSessionNames()
        {
            List<string> sessionNames = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (MySqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "select distinct sessionname from mlperf.status";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sessionNames.Add((string)reader[0]);
                        }
                    }
                }
            }

            return sessionNames;
        }

        public List<float> GetValues(string sessionName)
        {
            List<float> ret = new List<float>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (MySqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "select value from mlperf.status where SessionName = @sessionName order by step asc";
                    command.Parameters.AddWithValue("sessionName", sessionName);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret.Add((float)(decimal)reader[0]);
                        }
                    }
                }
            }
            return ret;
        }
    }
}
