resource "aws_subnet" "public" {
  count             = length(var.public_subnet_cidrs)
  vpc_id            = aws_vpc.this.id
  availability_zone = var.public_subnet_cidrs[count.index].zone
  cidr_block        = var.public_subnet_cidrs[count.index].cidr

  map_public_ip_on_launch = true

  tags = {
    Name                                     = "public-${format("%02d", count.index + 1)}"
    SubnetTraffic                            = "public"
    "kubernetes.io/role/elb"                 = "1"
    "kubernetes.io/cluster/${var.base_name}" = "owned"
  }
}

resource "aws_subnet" "private" {
  count             = length(var.private_subnet_cidrs)
  vpc_id            = aws_vpc.this.id
  availability_zone = var.private_subnet_cidrs[count.index].zone
  cidr_block        = var.private_subnet_cidrs[count.index].cidr
  tags = {
    "Name"                                   = "private-${format("%02d", count.index + 1)}"
    "SubnetTraffic"                          = "private"
    "kubernetes.io/role/internal-elb"        = "1"
    "kubernetes.io/cluster/${var.base_name}" = "owned"
  }
}
