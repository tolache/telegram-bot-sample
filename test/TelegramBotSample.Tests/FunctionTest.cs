using Amazon.Lambda.TestUtilities;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace TelegramBotSample.Tests;

public class FunctionTest
{
    [Fact]
    public async Task FunctionHandler_GivenValidUpdate_ReturnsSuccessfulResponse()
    {
        // Arrange
        Mock<ITelegramBotClient> mockTelegramClient = new();
        Function function = new(mockTelegramClient.Object);
        Update update = new()
        {
            Id = 101,
            Message = new Message
            {
                Id = 201,
                Date = new DateTime(2025, 1, 1),
                Text = "Hello, bot!",
                From = new User
                {
                    Id = 301,
                    FirstName = "John",
                    LastName = "Doe",
                    Username = "johndoe",
                    IsBot = false,
                    LanguageCode = "en",
                },
                Chat = new Chat
                {
                    Id = 401,
                    FirstName = "John",
                    LastName = "Doe",
                    Username = "johndoe",
                    Type = ChatType.Private,
                },
            }
        };
        TestLambdaContext context = new();

        // Act
        Function.Response result = await function.FunctionHandler(update, context);

        // Assert
        Assert.Equal("Success", result.Status);
    }
    
    [Fact]
    public async Task FunctionHandler_GivenInvalidUpdate_ReturnsErrorResponse()
    {
        // Arrange
        Mock<ITelegramBotClient> mockBotClient = new();
        Function function = new(mockBotClient.Object);
        Update update = new();
        TestLambdaContext context = new();

        // Act
        Function.Response response = await function.FunctionHandler(update, context);

        // Assert
        Assert.Equal("Error", response.Status);
        Assert.Contains("Failed to process update", response.Message);
    }
}
