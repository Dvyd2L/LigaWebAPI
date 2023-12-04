using EvaluacionDavidLlopis.Models;

namespace EvaluacionDavidLlopis.Interfaces;

public interface IReducirSueldo
{
    public Task<IEnumerable<Jugadore>?> ReducirSueldo(int porcentaje);
}
