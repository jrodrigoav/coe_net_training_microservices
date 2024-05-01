resource "aws_route_table" "private" {
  vpc_id = aws_vpc.this.id
  route {
    cidr_block     = "0.0.0.0/0"
    nat_gateway_id = aws_nat_gateway.this.id
  }
  tags = {
    Name = "private-${var.base_name}"
  }
}

resource "aws_route_table" "public" {
  vpc_id = aws_vpc.this.id
  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.this.id
  }
  tags = {
    Name = "public-${var.base_name}"
  }
}

resource "aws_route_table_association" "private" {
  for_each       = { for idx, s in aws_subnet.private : idx => s.id }
  subnet_id      = each.value
  route_table_id = aws_route_table.private.id
}

resource "aws_route_table_association" "public" {
  for_each       = { for idx, s in aws_subnet.public : idx => s.id }
  subnet_id      = each.value
  route_table_id = aws_route_table.public.id
}
