namespace EvaluacionDavidLlopis.DTOs;

public class DTOJugadorInput
{
    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public decimal Sueldo { get; set; }

    public bool Lesionado { get; set; }

    public int EquipoId { get; set; }
}
