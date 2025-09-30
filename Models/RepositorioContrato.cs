using MySql.Data.MySqlClient;

namespace Inmobiliaria_.Net_Core.Models
{
    public class RepositorioContrato : RepositorioBase, IRepositorioContrato
    {
        public RepositorioContrato(IConfiguration configuration) : base(configuration)
        {
        }

        // Alta
        public int Alta(Contrato contrato)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO contrato (id_inquilino, id_inmueble, monto_mensual, fecha_inicio, fecha_fin)
                               VALUES (@idInquilino, @idInmueble, @montoMensual, @fechaInicio, @fechaFin);
                               SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
                    command.Parameters.AddWithValue("@idInmueble", contrato.IdInmueble);
                    command.Parameters.AddWithValue("@montoMensual", contrato.MontoMensual);
                    command.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", contrato.FechaFin);
                    connection.Open();
                    contrato.IdContrato = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return contrato.IdContrato;
        }
       
        public int Baja(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "DELETE FROM contrato WHERE id_contrato = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int Modificacion(Contrato contrato)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE contrato 
                               SET id_inquilino = @idInquilino, 
                                   id_inmueble = @idInmueble, 
                                   monto_mensual = @montoMensual, 
                                   fecha_inicio = @fechaInicio, 
                                   fecha_fin = @fechaFin                                   
                               WHERE id_contrato = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
                    command.Parameters.AddWithValue("@idInmueble", contrato.IdInmueble);
                    command.Parameters.AddWithValue("@montoMensual", contrato.MontoMensual);
                    command.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", contrato.FechaFin);
                    command.Parameters.AddWithValue("@id", contrato.IdContrato);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public IList<Contrato> ObtenerTodos()
        {
            IList<Contrato> lista = new List<Contrato>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT c.id_contrato, c.monto_mensual, c.fecha_inicio, c.fecha_fin,
                      i.id_inmueble, i.direccion AS InmuebleDireccion,
                      inq.id_inquilino, inq.nombre AS InquilinoNombre, inq.apellido AS InquilinoApellido,
                      p.nombre AS PropietarioNombre, p.apellido AS PropietarioApellido
               FROM contrato c
               INNER JOIN inmueble i ON c.id_inmueble = i.id_inmueble
               INNER JOIN propietario p ON i.id_propietario = p.id_propietario
               INNER JOIN inquilino inq ON c.id_inquilino = inq.id_inquilino";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Contrato
                            {
                                IdContrato = reader.GetInt32(reader.GetOrdinal("id_contrato")),
                                IdInquilino = reader.GetInt32(reader.GetOrdinal("id_inquilino")),
                                IdInmueble = reader.GetInt32(reader.GetOrdinal("id_inmueble")),
                                MontoMensual = reader.GetDecimal(reader.GetOrdinal("monto_mensual")),
                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("fecha_inicio")),
                                FechaFin = reader.GetDateTime(reader.GetOrdinal("fecha_fin")),
                                Inquilino = new Inquilino
                                {
                                    IdInquilino = reader.GetInt32(reader.GetOrdinal("id_inquilino")),
                                    Nombre = reader.GetString(reader.GetOrdinal("InquilinoNombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("InquilinoApellido"))
                                },
                                Inmueble = new Inmueble
                                {
                                    IdInmueble = reader.GetInt32(reader.GetOrdinal("id_inmueble")),
                                    Direccion = reader.GetString(reader.GetOrdinal("InmuebleDireccion"))
                                },
                                Propietario = new Propietario
                                {
                                    Nombre = reader.GetString(reader.GetOrdinal("PropietarioNombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("PropietarioApellido"))
                                }
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public Contrato? ObtenerPorId(int id)
        {
            Contrato contrato = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT c.id_contrato, c.monto_mensual, c.fecha_inicio, c.fecha_fin,
                              i.id_inmueble, i.direccion AS InmuebleDireccion,
                              inq.id_inquilino, inq.nombre AS InquilinoNombre, inq.apellido AS InquilinoApellido,
                              p.nombre AS PropietarioNombre, p.apellido AS PropietarioApellido
                       FROM contrato c
                       INNER JOIN inmueble i ON c.id_inmueble = i.id_inmueble
                       INNER JOIN propietario p ON i.id_propietario = p.id_propietario
                       INNER JOIN inquilino inq ON c.id_inquilino = inq.id_inquilino
                       WHERE c.id_contrato = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            contrato = new Contrato
                            {
                                IdContrato = reader.GetInt32(reader.GetOrdinal("id_contrato")),
                                IdInquilino = reader.GetInt32(reader.GetOrdinal("id_inquilino")),
                                IdInmueble = reader.GetInt32(reader.GetOrdinal("id_inmueble")),
                                MontoMensual = reader.GetDecimal(reader.GetOrdinal("monto_mensual")),
                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("fecha_inicio")),
                                FechaFin = reader.GetDateTime(reader.GetOrdinal("fecha_fin")),
                                Inquilino = new Inquilino
                                {
                                    IdInquilino = reader.GetInt32(reader.GetOrdinal("id_inquilino")),
                                    Nombre = reader.GetString(reader.GetOrdinal("InquilinoNombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("InquilinoApellido"))
                                },
                                Inmueble = new Inmueble
                                {
                                    IdInmueble = reader.GetInt32(reader.GetOrdinal("id_inmueble")),
                                    Direccion = reader.GetString(reader.GetOrdinal("InmuebleDireccion"))
                                },
                                Propietario = new Propietario
                                {
                                    Nombre = reader.GetString(reader.GetOrdinal("PropietarioNombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("PropietarioApellido"))
                                }
                            };
                        }
                    }
                }
            }
            return contrato;
        }
    }
}