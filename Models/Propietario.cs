using System.ComponentModel.DataAnnotations;


namespace Inmobiliaria_.Net_Core.Models
{
	public class Propietario
	{
		[Key]
		public int IdPropietario { get; set; }
		[Required]
		public string Nombre { get; set; } = string.Empty;      
		public string Apellido { get; set; } = string.Empty;    
		[Required]
		public string Dni { get; set; } = string.Empty;
		public string Telefono { get; set; } = string.Empty;
		public string Direccion { get; set; } = string.Empty;

		public override string ToString()
		{
			return $"{Nombre} {Apellido} ({Dni})";
		}
	}
}
