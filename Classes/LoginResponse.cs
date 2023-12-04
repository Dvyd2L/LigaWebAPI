using EvaluacionDavidLlopis.Interfaces;

namespace EvaluacionDavidLlopis.Classes;

public record LoginResponse(string Email, string Token) : ILoginResponse;