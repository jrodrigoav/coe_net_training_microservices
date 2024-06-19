resource "aws_eks_cluster" "this" {
  name     = var.base_name
  version  = var.eks_version
  role_arn = aws_iam_role.eks.arn

  vpc_config {
    endpoint_private_access = false
    endpoint_public_access  = true

    subnet_ids          = concat(data.aws_subnets.private.ids, data.aws_subnets.public.ids)
    public_access_cidrs = var.public_access_cidrs
  }

  depends_on = [aws_iam_role_policy_attachment.eks]
}