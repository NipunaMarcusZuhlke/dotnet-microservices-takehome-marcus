namespace OrderProcessor.Middleware;

public record ApiError(string ErrorCode, string Message, List<string> ErrorDetails = default!);
