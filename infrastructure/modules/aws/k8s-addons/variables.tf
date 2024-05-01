variable "tags" {
  type = map(string)
}

variable "eks_name" {
  type = string
}

variable "openid_provider_arn" {
    type = string
}

variable "enable_cluster_autoscaler" {
  type = bool
  default = false
}