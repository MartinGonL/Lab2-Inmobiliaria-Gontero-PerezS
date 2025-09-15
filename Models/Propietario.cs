using System.ComponentModel.DataAnnotations;


namespace Inmobiliaria_.Net_Core.Models
{
	public class Propietario
	{
		[Key]
		public int IdPropietario { get; set; }

		[Required]
		[StringLength(50)]
		public string Nombre { get; set; } = string.Empty;

		[Required]
		[StringLength(50)]
		public string Apellido { get; set; } = string.Empty;

		[Required]
		[StringLength(10)]
		public string Dni { get; set; } = string.Empty;

		[Required]
		[StringLength(20)]
		public string Telefono { get; set; } = string.Empty; 

		[Required]
		[StringLength(255)]
		public string Direccion { get; set; } = string.Empty; 

		public override string ToString()
		{
			return $"{Nombre} {Apellido} ({Dni})";
		}
	}
}
