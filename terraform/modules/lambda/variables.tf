variable "function_name" {
  type = string
  description = "The name of the Lambda function"
}
variable "iam_role_arn" {
  type = string
  description = "The name ARN of the IAM role with which the Lambda function will be executed"
}

variable "telegram_bot_token" {
  type = string
  description = "Telegram bot token"
}
