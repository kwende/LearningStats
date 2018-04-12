using LearningStatsWeb.DataContracts;
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

        public void InsertMetaData(string sessionName, string gitHash, string notes)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                object id = null;
                using (MySqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "select id from sessionproperties where sessionName = @name";
                    command.Parameters.AddWithValue("name", sessionName);
                    id = command.ExecuteScalar();
                }

                if (id == null)
                {
                    using (MySqlCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "insert into sessionproperties (sessionName, gitHash, notes) values (@name, @gitHash, @notes)";
                        command.Parameters.AddWithValue("name", sessionName);
                        command.Parameters.AddWithValue("gitHash", gitHash);
                        command.Parameters.AddWithValue("notes", notes);
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (MySqlCommand command = conn.CreateCommand())
                    {
                        command.CommandText = "update sessionproperties set gitHash=@gitHash,notes=@notes where id=@id";
                        command.Parameters.AddWithValue("id", (int)id);
                        command.Parameters.AddWithValue("gitHash", gitHash);
                        command.Parameters.AddWithValue("notes", notes);
                        command.ExecuteNonQuery();
                    }
                }
            }
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

        public SessionProperties GetSessionProperties(string sessionName)
        {
            SessionProperties ret = null;

            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open(); 
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select id,gitHash,notes from sessionproperties where sessionName=@sessionName";
                    cmd.Parameters.AddWithValue("sessionName", sessionName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ret = new SessionProperties
                            {
                                SessionPropertiesId = (int)reader[0],
                                GitHash = (string)reader[1],
                                Notes = (string)reader[2],
                                SessionName = sessionName
                            };
                            break; 
                        }
                    }
                }
            }

            return ret;
        }

        public List<float> GetSessionValues(string sessionName)
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
