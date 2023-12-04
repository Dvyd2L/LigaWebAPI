using EvaluacionDavidLlopis.Classes;
using EvaluacionDavidLlopis.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace EvaluacionDavidLlopis.Services;

public class HashService
{
    #region PROPs

    #endregion

    #region CONSTRUCTORs

    #endregion

    #region METHODs
    /// <summary>
    /// Método para generar el salt aleatorio
    /// </summary>
    /// <returns>El <![CDATA[byte[]]]> salt aleatorio</returns>
    public byte[] GetSalt()
    {
        byte[] salt = new byte[16];
        using (RandomNumberGenerator random = RandomNumberGenerator.Create())
        {
            random.GetBytes(salt); // Genera un array aleatorio de bytes
        }

        return salt;
    }

    /// <summary>
    /// Método para obtener el hash
    /// </summary>
    /// <param name="textoPlano">Texto que deseamos encriptar</param>
    /// <param name="salt">Salt con el cual queremos generar el hash</param>
    /// <returns>Un objeto <![CDATA[IHashResult]]> con el hash y el salt que se ha usado para generarlo</returns>
    public IHashResult GetHash(string textoPlano, byte[]? salt = null)
    {
        // Si no se proporciona un salt generamos el salt aleatorio
        salt ??= GetSalt();

        //Pbkdf2 es un algoritmo de encriptación
        byte[] claveDerivada = KeyDerivation.Pbkdf2(password: textoPlano,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 32);

        string hash = Convert.ToBase64String(claveDerivada);

        // Llamamos al record ResultadoHash y retornamos el hash con el salt
        return new HashResult(hash, salt);
    }
    #endregion
}
