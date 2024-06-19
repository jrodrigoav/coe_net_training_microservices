remote_state {
  backend = "local"
  generate = {
    path      = "backend.tf"
    if_exists = "overwrite"
  }
  config = {
    path = "${path_relative_to_include()}/terraform.tfstate"
  }
}
