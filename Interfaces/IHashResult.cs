﻿namespace EvaluacionDavidLlopis.Interfaces;

public interface IHashResult
{
    string Hash { get; init; }
    byte[] Salt { get; init; }
}