using Amazon.Lambda.Core;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotSample.Core;
using TelegramBotSample.Infrastructure;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TelegramBotSample;

public class Function
{
    private readonly ITelegramBotClient _telegramClient;
    
    public Function()
    {
        _telegramClient = new TelegramBotClient(EnvironmentUtils.GetTelegramBotToken());
    }

    public Function(ITelegramBotClient telegramClient)
    {
        _telegramClient = telegramClient;
    }
    
    /// <summary>
    /// A simple function that responds to Telegram API updates
    /// </summary>
    /// <param name="update">A Telegram update object.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public async Task<Response> FunctionHandler(Update update, ILambdaContext context)
    {
        ArgumentNullException.ThrowIfNull(update);
        
        ILambdaLogger logger = context.Logger;
        UpdateProcessor updateProcessor = new(_telegramClient, logger);

        try
        {
            await updateProcessor.ProcessUpdateAsync(update);
            return new Response("Success", "Update processed successfully.");
        }
        catch (Exception e)
        {
            string errorMessage = $"Failed to process update: {e.Message}";
            logger.LogError(errorMessage);
            return new Response("Error", errorMessage);
        }
    }
    
    public record Response(string Status, string Message);
}
