terraform {
  source = "../../../../modules/aws//vpc"
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
  base_name = "jrav20240428"
  vpc_cidr  = "10.0.0.0/18" #16384 IPs
  public_subnet_cidrs = [
    {
      zone = "us-east-1a"
      cidr = "10.0.0.0/22" #1024 IPs
    },
    {
      zone = "us-east-1b"
      cidr = "10.0.4.0/22"
    },
    {
      zone = "us-east-1c"
      cidr = "10.0.8.0/22"
    }
  ]
  private_subnet_cidrs = [
    {
      zone = "us-east-1a"
      cidr = "10.0.32.0/22" #1024 IPs
    },
    {
      zone = "us-east-1b"
      cidr = "10.0.36.0/22"
    },
    {
      zone = "us-east-1c"
      cidr = "10.0.40.0/22"
    }
  ]
}