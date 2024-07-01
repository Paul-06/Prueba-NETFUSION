using PruebaTecnica.Models;
using System.Data.SqlClient;

namespace PruebaTecnica.Data
{
    public class PersonaService
    {
        private readonly ConexionBD _con = new();

        public async Task<IEnumerable<Persona>> GetAll()
        {
            var personas = new List<Persona>();
            try
            {
                using (var conexion = new SqlConnection(_con.GetDbConn()))
                {
                    await conexion.OpenAsync();
                    string sql = "SELECT * FROM Personas";

                    using (var cmd = new SqlCommand(sql, conexion))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            personas.Add(new Persona
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombres = reader["Nombres"].ToString(),
                                ApellidoPaterno = reader["ApellidoPaterno"].ToString(),
                                ApellidoMaterno = reader["ApellidoMaterno"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"].ToString()),
                                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"].ToString())
                            });
                        }

                        return personas;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Persona>();
            }  
        }

        public async Task<Persona> GetById(int id)
        {
            try
            {
                using (var conexion = new SqlConnection(_con.GetDbConn()))
                {
                    await conexion.OpenAsync();
                    string sql = "SELECT * FROM Personas WHERE Id = @Id";

                    using (var cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        var reader = await cmd.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            return new Persona
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombres = reader["Nombres"].ToString(),
                                ApellidoPaterno = reader["ApellidoPaterno"].ToString(),
                                ApellidoMaterno = reader["ApellidoMaterno"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"].ToString())
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<bool> Add(Persona persona)
        {
            try
            {
                using (var conexion = new SqlConnection(_con.GetDbConn()))
                {
                    await conexion.OpenAsync();
                    string sql = "INSERT INTO Personas (Nombres, ApellidoPaterno, ApellidoMaterno, FechaNacimiento) VALUES (@Nombres, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento)";

                    using (var cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Nombres", persona.Nombres);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", persona.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", persona.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento);

                        var result = await cmd.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(Persona persona)
        {
            try
            {
                using (var conexion = new SqlConnection(_con.GetDbConn()))
                {
                    await conexion.OpenAsync();
                    string sql = "UPDATE Personas SET Nombres = @Nombres, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, FechaNacimiento = @FechaNacimiento WHERE Id = @Id";

                    using (var cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Id", persona.Id);
                        cmd.Parameters.AddWithValue("@Nombres", persona.Nombres);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", persona.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", persona.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento);

                        var result = await cmd.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var conexion = new SqlConnection(_con.GetDbConn()))
                {
                    await conexion.OpenAsync();
                    string sql = "DELETE FROM Personas WHERE Id = @Id";

                    using (var cmd = new SqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        var result = await cmd.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
