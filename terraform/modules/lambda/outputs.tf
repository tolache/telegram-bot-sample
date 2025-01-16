output "function_name" {
  value = aws_lambda_function.telegram_bot_sample.function_name
}

output "invoke_arn" {
  value = aws_lambda_function.telegram_bot_sample.invoke_arn
  description = "The invoke ARN of the Lambda function. Used by API Gateway."
}
