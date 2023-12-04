using EvaluacionDavidLlopis.Interfaces;
using EvaluacionDavidLlopis.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDavidLlopis.Services;

public class SueldoService(LigaContext DbContext)
    : IReducirSueldo
{
    private static decimal AplicarPorcentaje(decimal sueldo, int porcentaje)
        => sueldo - (sueldo * porcentaje / 100);

    public async Task<IEnumerable<Jugadore>?> ReducirSueldo(int porcentaje)
    {
        Jugadore[] lesionadosDB = await DbContext.Jugadores
            .Where((x) => x.Lesionado)
            .ToArrayAsync();

        foreach (Jugadore? j in lesionadosDB)
        {
            j.Sueldo = AplicarPorcentaje(j.Sueldo, porcentaje);
        }

        return lesionadosDB;
    }
}
