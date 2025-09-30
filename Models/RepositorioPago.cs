using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Inmobiliaria_.Net_Core.Models
{
    public class RepositorioPago : RepositorioBase, IRepositorioPago
    {
        public RepositorioPago(IConfiguration configuration) : base(configuration)
        {
        }

        public int Alta(Pago p)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO pago 
                    (id_contrato, fecha_pago, monto_pagado, mes_correspondiente, anio_correspondiente, estado)
                    VALUES (@id_contrato,@fecha_pago,@monto_pagado,@mes,@anio,@estado);
                    SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id_contrato", p.IdContrato);
                    command.Parameters.AddWithValue("@fecha_pago", p.FechaPago);
                    command.Parameters.AddWithValue("@monto_pagado", p.MontoPagado);
                    command.Parameters.AddWithValue("@mes", p.MesCorrespondiente);
                    command.Parameters.AddWithValue("@anio", p.AnioCorrespondiente);
                    command.Parameters.AddWithValue("@estado", p.Estado ?? "activo");
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    p.IdPago = res;
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
                string sql = "DELETE FROM pago WHERE id_pago=@id";
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

        public int Modificacion(Pago p)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE pago SET 
                        id_contrato=@id_contrato, 
                        fecha_pago=@fecha_pago,
                        monto_pagado=@monto_pagado, 
                        mes_correspondiente=@mes,
                        anio_correspondiente=@anio, 
                        estado=@estado
                    WHERE id_pago=@id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id_contrato", p.IdContrato);
                    command.Parameters.AddWithValue("@fecha_pago", p.FechaPago);
                    command.Parameters.AddWithValue("@monto_pagado", p.MontoPagado);
                    command.Parameters.AddWithValue("@mes", p.MesCorrespondiente);
                    command.Parameters.AddWithValue("@anio", p.AnioCorrespondiente);
                    command.Parameters.AddWithValue("@estado", p.Estado);
                    command.Parameters.AddWithValue("@id", p.IdPago);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public Pago ObtenerPorId(int id)
        {
            Pago p = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT * FROM pago WHERE id_pago=@id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = Mapear(reader);
                    }
                    connection.Close();
                }
            }
            return p;
        }

        public IList<Pago> ObtenerTodos()
        {
            IList<Pago> res = new List<Pago>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT * FROM pago";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        res.Add(Mapear(reader));
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Pago> ObtenerPorContrato(int idContrato)
        {
            IList<Pago> res = new List<Pago>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT * FROM pago WHERE id_contrato=@idc";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idc", idContrato);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        res.Add(Mapear(reader));
                    }
                    connection.Close();
                }
            }
            return res;
        }

        private Pago Mapear(MySqlDataReader r)
        {
            return new Pago
            {
                IdPago = r.GetInt32("id_pago"),
                IdContrato = r.GetInt32("id_contrato"),
                FechaPago = r.GetDateTime("fecha_pago"),
                MontoPagado = r.GetDecimal("monto_pagado"),
                MesCorrespondiente = r.GetByte("mes_correspondiente"),
                AnioCorrespondiente = r.GetInt16("anio_correspondiente"),
                Estado = r.GetString("estado")
            };
        }
    }
}

