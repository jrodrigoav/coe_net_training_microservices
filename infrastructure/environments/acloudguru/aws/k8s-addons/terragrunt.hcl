terraform {
  source = "../../../../modules/aws//k8s-addons"
}

include "root" {
  path = find_in_parent_folders()
}

generate "provider" {
  path      = "provider.tf"
  if_exists = "overwrite"
  contents  = <<EOF
  provider "aws" {
  default_tags {
    tags = var.tags
  }
  profile = "acloudguru"
}
  EOF
}

inputs = {
  tags = {
    Account     = "acloudguru"
    Environment = "sandbox"
    CreatedBy   = "jesus.acedo@unosquare.com"
  }
  eks_name           = "jrav20240428"
  openid_provider_arn = "arn:aws:iam::850071827025:oidc-provider/oidc.eks.us-east-1.amazonaws.com/id/6BA0B061D9128A2E6DCD1B07B3B44E8B"
  enable_cluster_autoscaler      = true
}