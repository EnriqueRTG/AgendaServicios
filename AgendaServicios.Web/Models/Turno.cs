using System.ComponentModel.DataAnnotations;

namespace AgendaServicios.Web.Models;

public partial class Turno
{
	[Key]
	public int TurnoId { get; set; }

	[Display(Name = "Fecha turno")]
	[Required(ErrorMessage = "El campo {0} es obligatorio.")]
	public DateTime FechaTurno { get; set; }

	[Display(Name = "Hora turno")]
	[Required(ErrorMessage = "El campo {0} es obligatorio.")]
	public TimeSpan HoraTurno { get; set; }

	[Display(Name = "Estado turno")]
	[Required(ErrorMessage = "El campo {0} es obligatorio.")]
	public int EstadoTurnoId { get; set; }

	[Display(Name = "Cliente")]
	public int? ClienteId { get; set; }

	[Display(Name = "Servicio")]
	public int? ServicioId { get; set; }

	[Display(Name = "Observación")]
	public string? Observacion { get; set; }

	public virtual Cliente? Cliente { get; set; }

	public virtual EstadosTurno EstadoTurno { get; set; } = null!;

	public virtual Servicio? Servicio { get; set; }
}
