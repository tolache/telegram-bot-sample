variable "application_name" {
  default = "TelegramBotSample"
  description = "Name of the application. Will be used as the name of resources like IAM role, Lambda function, and API Gateway."
}

variable "telegram_bot_token" {
  type = string
  description = "Telegram bot token. Required by Lambda function."
}

variable "telegram_bot_secret" {
  type        = string
  description = "Telegram bot webhook's secret token. Required by WAF to verify that requests to AWS originate from your Telegram webhook."
}
