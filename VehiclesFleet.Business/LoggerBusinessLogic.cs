// using VehiclesFleet.BusinessLogic.Contracts;
// using VehiclesFleet.Domain.Models;
// using VehiclesFleet.Repository.Contracts;
// using VehiclesFleet.Services.Contracts;
//
// namespace VehiclesFleet.BusinessLogic;
//
// public class LoggerBusinessLogic:ILoggerBusinessLogic
// {
//     private readonly ILoggerRepository loggerRepository;
//     private readonly IJwtService jwtService;
//          
//     public LoggerBusinessLogic(ILoggerRepository loggerRepository,IJwtService jwtService)
//     {
//         this.loggerRepository = loggerRepository;
//         this.jwtService = jwtService;
//     }
//          
//     public async Task LogInfo(LoggerMessage message,string? token)
//     {
//         if (message is null)
//         {
//             throw new Exception($"{message} is null!");
//         }
//      
//         if (String.IsNullOrEmpty(message.Message))
//         {
//             throw new Exception("Cannot log an empty message!");
//         }
//
//         if (token is not null)
//         {
//             message.UserEmail = jwtService.GetUserEmailFromToken(token);
//         }
//         
//         await loggerRepository.AddLogToDatabase(message);
//     }
//      
//     public async Task LogError(LoggerMessage message)
//     {
//         if (message is null)
//         {
//             throw new Exception($"{message} is null!");
//         }
//      
//         if (String.IsNullOrEmpty(message.Message))
//         {
//             throw new Exception("Cannot log an empty message!");
//         }
//         
//         await loggerRepository.AddErrorToDatabase(message);
//     }
// }