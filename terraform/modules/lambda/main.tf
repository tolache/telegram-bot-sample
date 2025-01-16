resource "aws_lambda_function" "telegram_bot_sample" {
  architectures                  = ["x86_64"]
  description                    = "A Telegram bot sample application"
  function_name                  = var.function_name
  handler                        = "TelegramBotSample::TelegramBotSample.Function::FunctionHandler"
  memory_size                    = 512
  package_type                   = "Zip"
  reserved_concurrent_executions = -1
  role                           = var.iam_role_arn
  runtime                        = "dotnet8"
  timeout                        = 30
  tags = {
    Application = var.function_name
    Terraform   = "true"
  }

  filename = "../publish/TelegramBotSample.zip"

  environment {
    variables = {
      "TELEGRAM_BOT_TOKEN" = var.telegram_bot_token
    }
  }

  logging_config {
    log_format            = "Text"
    log_group             = "/aws/lambda/${var.function_name}"
  }
}

resource "aws_cloudwatch_log_group" "lambda_log_group" {
  name              = "/aws/lambda/${var.function_name}"
  retention_in_days = 90
  tags = {
    Application = var.function_name
    Terraform   = "true"
  }
}
