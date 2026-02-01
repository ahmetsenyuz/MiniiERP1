using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MiniiERP1.Services
{
    public class LoggingService
    {
        private readonly ILogger _logger;

        public LoggingService(ILogger logger)
        {
            _logger = logger;
        }

        public void LogValidationError(string operation, string message)
        {
            _logger.LogWarning($"Validation Error in {operation}: {message}");
        }

        public void LogBusinessRuleViolation(string operation, string message)
        {
            _logger.LogWarning($"Business Rule Violation in {operation}: {message}");
        }

        public void LogSystemError(string operation, Exception ex)
        {
            _logger.LogError(ex, $"System Error in {operation}");
        }

        public void LogInfo(string operation, string message)
        {
            _logger.LogInformation($"{operation}: {message}");
        }
    }
}