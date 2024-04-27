resource "aws_eip" "nat" {
  domain = "vpc"  
  depends_on = [ aws_internet_gateway.this ]
}