using EvaluacionDavidLlopis.Interfaces;

namespace EvaluacionDavidLlopis.Classes;

public record HashResult(string Hash, byte[] Salt) : IHashResult;
