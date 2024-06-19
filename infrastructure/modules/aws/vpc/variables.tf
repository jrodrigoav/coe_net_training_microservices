variable "tags" {
  type = map(string)
}

variable "base_name" {
  type = string
}

variable "vpc_cidr" {
  type = string
}

variable "public_subnet_cidrs" {
  type = list(object({
    zone = string
    cidr = string
  }))
}

variable "private_subnet_cidrs" {
  type = list(object({
    zone = string
    cidr = string
  }))
}
