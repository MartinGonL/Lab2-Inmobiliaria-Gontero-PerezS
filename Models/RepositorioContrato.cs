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
                   // command.Parameters.AddWithValue("@creadoPor", contrato.CreadoPor);
                   //command.Parameters.AddWithValue("@terminadoPor", contrato.TerminadoPor);
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
                   // command.Parameters.AddWithValue("@creadoPor", contrato.CreadoPor);
                  //  command.Parameters.AddWithValue("@terminadoPor", contrato.TerminadoPor);
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
                string sql = "SELECT * FROM contrato";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new Contrato
                        {
                            IdContrato = reader.GetInt32("id_contrato"),
                            IdInquilino = reader.GetInt32("id_inquilino"),
                            IdInmueble = reader.GetInt32("id_inmueble"),
                            MontoMensual = reader.GetDecimal("monto_mensual"),
                            FechaInicio = reader.GetDateTime("fecha_inicio"),
                            FechaFin = reader.GetDateTime("fecha_fin"),
                          //  CreadoPor = reader.GetInt32("creado_por"),
                          //  TerminadoPor = reader.IsDBNull(reader.GetOrdinal("terminado_por")) ? null : reader.GetInt32("terminado_por")
                        });
                    }
                }
            }
            return lista;
        }

        public Contrato ObtenerPorId(int id)
        {
            Contrato contrato = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "SELECT * FROM contrato WHERE id_contrato = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        contrato = new Contrato
                        {
                            IdContrato = reader.GetInt32("id_contrato"),
                            IdInquilino = reader.GetInt32("id_inquilino"),
                            IdInmueble = reader.GetInt32("id_inmueble"),
                            MontoMensual = reader.GetDecimal("monto_mensual"),
                            FechaInicio = reader.GetDateTime("fecha_inicio"),
                            FechaFin = reader.GetDateTime("fecha_fin"),
                           //CreadoPor = reader.GetInt32("creado_por"),
                           // TerminadoPor = reader.IsDBNull(reader.GetOrdinal("terminado_por")) ? null : reader.GetInt32("terminado_por")
                        };
                    }
                }
            }
            return contrato;
        }
    }
}