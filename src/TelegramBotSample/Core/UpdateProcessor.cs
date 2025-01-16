using Amazon.Lambda.Core;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBotSample.Core;

/// <summary>
/// This class is the core functionality of the bot. It handles the messages and sends the replies.
/// </summary>
/// <param name="telegramClient">A Telegram update object.</param>
/// <param name="logger">To avoid custom wrappers, logger needs to be ILambdaLogger until https://github.com/aws/aws-lambda-dotnet/issues/1747 is fixed
/// </param>
/// <returns></returns>
public class UpdateProcessor(ITelegramBotClient telegramClient, ILambdaLogger logger)
{
    public async Task ProcessUpdateAsync(Update update)
    {
        ArgumentNullException.ThrowIfNull(update);
        
        if (update is not { Type: UpdateType.Message, Message: not null })
        {
            const string errorMessage = "Unsupported update. Only non-null messages are supported.";
            logger.LogError(errorMessage);
            throw new InvalidOperationException(errorMessage);
        }
        
        long? userId = update.Message.From?.Id;
        string? username = update.Message.From?.Username;
        string? message = update.Message.Text;
        string reply = $"You said: {message}";
        
        logger.LogInformation($"Received message from {username} (id: {userId}).");
        await telegramClient.SendMessage(update.Message.Chat.Id, reply);
    }
}