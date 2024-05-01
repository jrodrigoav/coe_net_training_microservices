resource "aws_kms_key" "this" {
  key_usage               = "ENCRYPT_DECRYPT"
  description             = "KMS key for eks test cluster"
  deletion_window_in_days = 7
}