using MySql.Data.MySqlClient;

namespace Inmobiliaria_.Net_Core.Models
{
    public class RepositorioInmueble : RepositorioBase, IRepositorioInmueble
    {
        public RepositorioInmueble(IConfiguration configuration) : base(configuration)
        {
        }

        public int Alta(Inmueble casa)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO inmueble (id_propietario, direccion, tipo, estado)
                               VALUES (@idPropietario, @direccion, @tipo, @estado);
                               SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPropietario", casa.IdPropietario);
                    command.Parameters.AddWithValue("@direccion", casa.Direccion);
                    command.Parameters.AddWithValue("@tipo", casa.Tipo);
                    command.Parameters.AddWithValue("@estado", casa.Estado);
                    connection.Open();
                    casa.IdInmueble = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return casa.IdInmueble;
        }

        public int Baja(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "DELETE FROM inmueble WHERE id_inmueble = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int Modificacion(Inmueble casa)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE inmueble 
                               SET id_propietario=@idPropietario, direccion=@direccion, tipo=@tipo, estado=@estado
                               WHERE id_inmueble = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPropietario", casa.IdPropietario);
                    command.Parameters.AddWithValue("@direccion", casa.Direccion);
                    command.Parameters.AddWithValue("@tipo", casa.Tipo);
                    command.Parameters.AddWithValue("@estado", casa.Estado);
                    command.Parameters.AddWithValue("@id", casa.IdInmueble);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public IList<Inmueble> ObtenerTodos()
        {
            IList<Inmueble> lista = new List<Inmueble>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT i.id_inmueble, i.id_propietario, i.direccion, i.tipo, i.estado, 
                                      p.nombre AS PropietarioNombre, p.apellido AS PropietarioApellido
                               FROM inmueble i
                               INNER JOIN propietario p ON i.id_propietario = p.id_propietario";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new Inmueble
                        {
                            IdInmueble = reader.GetInt32("id_inmueble"),
                            IdPropietario = reader.GetInt32("id_propietario"),
                            Direccion = reader.GetString("direccion"),
                            Tipo = reader.IsDBNull(reader.GetOrdinal("tipo")) ? string.Empty : reader.GetString("tipo"),
                            Estado = reader.GetString("estado"),
                            Propietario = new Propietario
                            {
                                Nombre = reader.GetString("PropietarioNombre"),
                                Apellido = reader.GetString("PropietarioApellido")
                            }
                        });
                    }
                }
            }
            return lista;
        }

        public Inmueble? ObtenerPorId(int id)
        {
            Inmueble? inmueble = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                const string sql = @"SELECT i.id_inmueble, i.id_propietario, i.direccion, i.tipo, i.estado,
                                    p.id_propietario AS PropietarioId, p.nombre, p.apellido
                             FROM inmueble i
                             LEFT JOIN propietario p ON i.id_propietario = p.id_propietario
                             WHERE i.id_inmueble = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            inmueble = new Inmueble
                            {
                                IdInmueble = reader.GetInt32("id_inmueble"),
                                IdPropietario = reader.GetInt32("id_propietario"),
                                Direccion = reader.GetString("direccion"),
                                Tipo = reader.GetString("tipo"),
                                Estado = reader.GetString("estado"),
                                Duenio = new Propietario
                                {
                                    IdPropietario = reader.GetInt32("PropietarioId"),
                                    Nombre = reader.GetString("nombre"),
                                    Apellido = reader.GetString("apellido")
                                }
                            };
                        }
                    }
                }
            }
            return inmueble;
        }
    }
}