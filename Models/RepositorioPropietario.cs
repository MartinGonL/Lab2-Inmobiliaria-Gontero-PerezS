using MySql.Data.MySqlClient;
using System.Data;

namespace Inmobiliaria_.Net_Core.Models
{
    public class RepositorioPropietario : RepositorioBase, IRepositorioPropietario
    {
        public RepositorioPropietario(IConfiguration configuration) : base(configuration)
        {
        }

        public int Alta(Propietario p)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                const string sql = @"INSERT INTO propietario 
                    (nombre, apellido, dni, telefono, direccion)
                    VALUES (@nombre, @apellido, @dni, @telefono, @direccion);
                    SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", p.Nombre);
                    command.Parameters.AddWithValue("@apellido", p.Apellido);
                    command.Parameters.AddWithValue("@dni", p.Dni);
                    command.Parameters.AddWithValue("@telefono", p.Telefono);
                    command.Parameters.AddWithValue("@direccion", p.Direccion);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    p.IdPropietario = res;
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                const string sql = "DELETE FROM propietario WHERE id_propietario = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
            }
            return res;
        }

        public int Modificacion(Propietario p)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                const string sql = @"UPDATE propietario 
                    SET nombre=@nombre, apellido=@apellido, dni=@dni, telefono=@telefono, direccion=@direccion
                    WHERE id_propietario = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", p.Nombre);
                    command.Parameters.AddWithValue("@apellido", p.Apellido);
                    command.Parameters.AddWithValue("@dni", p.Dni);
                    command.Parameters.AddWithValue("@telefono", p.Telefono);
                    command.Parameters.AddWithValue("@direccion", p.Direccion);
                    command.Parameters.AddWithValue("@id", p.IdPropietario);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
            }
            return res;
        }

        public IList<Propietario> ObtenerTodos()
        {
            var res = new List<Propietario>();
            using (var connection = new MySqlConnection(connectionString))
            {
                const string sql = @"SELECT id_propietario, nombre, apellido, dni, telefono, direccion
                                     FROM propietario";
                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var p = new Propietario
                            {
                                IdPropietario = reader.GetInt32("id_propietario"),
                                Nombre = reader.GetString("nombre"),
                                Apellido = reader.GetString("apellido"),
                                Dni = reader.GetString("dni"),
                                Telefono = reader.GetString("telefono"),
                                Direccion = reader.GetString("direccion"),
                            };
                            res.Add(p);
                        }
                    }
                }
            }
            return res;
        }

        public Propietario? ObtenerPorId(int id)
        {
            Propietario? p = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                const string sql = @"SELECT id_propietario, nombre, apellido, dni, telefono, direccion
                    FROM propietario
                    WHERE id_propietario=@id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            p = new Propietario
                            {
                                IdPropietario = reader.GetInt32("id_propietario"),
                                Nombre = reader.GetString("nombre"),
                                Apellido = reader.GetString("apellido"),
                                Dni = reader.GetString("dni"),
                                Telefono = reader.GetString("telefono"),
                                Direccion = reader.GetString("direccion"),
                            };
                        }
                    }
                }
            }
            return p;
        }
        public IList<Propietario> BuscarPorNombre(string nombre)
		{
			var res = new List<Propietario>();
			nombre = "%" + nombre + "%"; // Preparar el parámetro para la búsqueda con LIKE
			using (var connection = new MySqlConnection(connectionString))
			{
				const string sql = @"SELECT id_propietario, nombre, apellido, dni, telefono, direccion
                                     FROM propietario
                                     WHERE nombre LIKE @nombre OR apellido LIKE @nombre";
				using (var command = new MySqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@nombre", nombre); // Agregar el parámetro
					connection.Open();
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var p = new Propietario
							{
								IdPropietario = reader.GetInt32("id_propietario"),
								Nombre = reader.GetString("nombre"),
								Apellido = reader.GetString("apellido"),
								Dni = reader.GetString("dni"),
								Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? null : reader.GetString("telefono"),
								Direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? null : reader.GetString("direccion"),
							};
							res.Add(p);
						}
					}
				}
			}
			return res;
		}
    }
}
