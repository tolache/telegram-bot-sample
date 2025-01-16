output "arn" {
  value       = aws_iam_role.telegram_bot_sample.arn
  description = "The ARN of the Lambda function IAM role"
}
