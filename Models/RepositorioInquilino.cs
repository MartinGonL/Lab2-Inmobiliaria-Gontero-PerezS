using MySql.Data.MySqlClient;

namespace Inmobiliaria_.Net_Core.Models
{
    public class RepositorioInquilino : RepositorioBase, IRepositorioInquilino
    {
        public RepositorioInquilino(IConfiguration configuration) : base(configuration)
        {
        }

        public int Alta(Inquilino i)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO inquilino 
                    (nombre, apellido, dni, telefono, direccion)
                    VALUES (@nombre, @apellido, @dni, @telefono, @direccion);
                    SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", i.Nombre);
                    command.Parameters.AddWithValue("@apellido", i.Apellido);
                    command.Parameters.AddWithValue("@dni", i.Dni);
                    command.Parameters.AddWithValue("@telefono", i.Telefono);
                    command.Parameters.AddWithValue("@direccion", i.Direccion);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    i.IdInquilino = res;
                    connection.Close();
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "DELETE FROM inquilino WHERE id_inquilino = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int Modificacion(Inquilino i)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE inquilino 
                    SET nombre=@nombre, apellido=@apellido, dni=@dni, telefono=@telefono, direccion=@direccion
                    WHERE id_inquilino = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", i.Nombre);
                    command.Parameters.AddWithValue("@apellido", i.Apellido);
                    command.Parameters.AddWithValue("@dni", i.Dni);
                    command.Parameters.AddWithValue("@telefono", i.Telefono);
                    command.Parameters.AddWithValue("@direccion", i.Direccion);
                    command.Parameters.AddWithValue("@id", i.IdInquilino);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Inquilino> ObtenerTodos()
        {
            IList<Inquilino> res = new List<Inquilino>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT id_inquilino, nombre, apellido, dni, telefono, direccion
                    FROM inquilino";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inquilino i = new Inquilino
                        {
                            IdInquilino = reader.GetInt32("id_inquilino"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Dni = reader.GetString("dni"),
                            Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? string.Empty : reader.GetString("telefono"),
                            Direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? string.Empty : reader.GetString("direccion"),
                        };
                        res.Add(i);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Inquilino> BuscarPorNombre(string nombre)
        {
            var res = new List<Inquilino>();
            nombre = "%" + nombre + "%"; 
            using (var connection = new MySqlConnection(connectionString))
            {
                const string sql = @"SELECT id_inquilino, nombre, apellido, dni, telefono, direccion
                                     FROM inquilino
                                     WHERE nombre LIKE @nombre OR apellido LIKE @nombre";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", nombre); 
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var i = new Inquilino
                            {
                                IdInquilino = reader.GetInt32("id_inquilino"),
                                Nombre = reader.GetString("nombre"),
                                Apellido = reader.GetString("apellido"),
                                Dni = reader.GetString("dni"),
                                Telefono = reader.GetString("telefono"),
                                Direccion = reader.GetString("direccion"),
                            };
                            res.Add(i);
                        }
                    }
                }
            }
            return res;
        }

        public Inquilino ObtenerPorId(int id)
        {
            Inquilino i = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT id_inquilino, nombre, apellido, dni, telefono, direccion
                    FROM inquilino
                    WHERE id_inquilino = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        i = new Inquilino
                        {
                            IdInquilino = reader.GetInt32("id_inquilino"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Dni = reader.GetString("dni"),
                            Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? string.Empty : reader.GetString("telefono"),
                            Direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? string.Empty : reader.GetString("direccion"),
                        };
                    }
                    connection.Close();
                }
            }
            return i;
        }
    }
}