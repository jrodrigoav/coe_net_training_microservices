resource "aws_nat_gateway" "this" {
  subnet_id = aws_subnet.public[0].id
  allocation_id = aws_eip.nat.allocation_id
  tags = {
    Name = var.base_name
  }
}
