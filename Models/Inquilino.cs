using System.ComponentModel.DataAnnotations;


namespace Inmobiliaria_.Net_Core.Models
{
	public class Inquilino
	{
		[Key]
		[Display(Name = "Código")]
		public int IdInquilino { get; set; }
		[Required]
		public string Nombre { get; set; }
		[Required]
		public string Apellido { get; set; }
		[Required]
		public string Dni { get; set; }
		public string Telefono { get; set; }
		public string Direccion { get; set; }
	}
}
