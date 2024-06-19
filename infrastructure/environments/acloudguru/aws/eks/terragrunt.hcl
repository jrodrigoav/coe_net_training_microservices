terraform {
  source = "../../../../modules/aws//eks"
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
  base_name           = "jrav20240428"
  vpc_id              = "vpc-02d9cc7148d2efba5"
  public_access_cidrs = ["189.203.100.8/32"]
  node_groups = {
    workers = {
      capacity_type  = "ON_DEMAND"
      instance_types = ["t3.medium"]
      scaling_config = {
        min_size     = 1
        max_size     = 5
        desired_size = 1
      }
    }
  }
}