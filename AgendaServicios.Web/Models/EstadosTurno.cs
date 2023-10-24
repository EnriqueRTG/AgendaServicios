using System.ComponentModel.DataAnnotations;

namespace AgendaServicios.Web.Models;

public partial class EstadosTurno
{
    [Key]
    public int EstadoTurnoId { get; set; }

    [Display(Name = "Descripción")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [StringLength(50, ErrorMessage = "La descripción no debe exceder los 50 caracteres")]
    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
