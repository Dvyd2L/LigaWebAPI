namespace EvaluacionDavidLlopis.DTOs;

public class DTOJugadoresCustom
{
    public int IdEquipo { get; set; }
    public required string Nombre { get; set; }
    public required string Ciudad { get; set; }
    public double PromedioEdad { get; set; }
    public required List<DTOListaJugadores> ListaJugadores { get; set; }
}

public class DTOListaJugadores
{
    public int IdJugador { get; set; }
    public required string Nombre { get; set; }
    public int Edad { get; set; }
    public decimal Sueldo { get; set; }
}