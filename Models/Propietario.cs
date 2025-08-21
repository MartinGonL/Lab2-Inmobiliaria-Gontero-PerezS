using System.ComponentModel.DataAnnotations;


namespace Inmobiliaria_.Net_Core.Models
{
	public class Propietario
	{
		[Key]
		public int IdPropietario { get; set; }
		[Required]
		public string Nombre { get; set; }
		[Required]
		public string Apellido { get; set; }
		[Required]
		public string Dni { get; set; }
		public string Telefono { get; set; }
		public string Direccion { get; set; }

		public override string ToString()
		{
			return $"{Nombre} {Apellido} ({Dni})";
		}
	}
}
