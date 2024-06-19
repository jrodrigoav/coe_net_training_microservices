data "aws_iam_policy_document" "cluster_autoscaler_assume_role" {
  statement {
    actions = ["sts:AssumeRoleWithWebIdentity"]
    effect  = "Allow"

    condition {
      test     = "StringEquals"
      variable = "${replace(data.aws_iam_openid_connect_provider.this.url, "https://", "")}:sub"
      values   = ["system:serviceaccount:kube-system:cluster-autoscaler"]
    }

    principals {
      identifiers = [data.aws_iam_openid_connect_provider.this.arn]
      type        = "Federated"
    }
  }
}

data "aws_iam_policy_document" "cluster_autoscaler" {
  statement {
    actions = [
      "autoscaling:DescribeAutoScalingGroups",
      "autoscaling:DescribeAutoScalingInstances",
      "autoscaling:DescribeLaunchConfigurations",
      "autoscaling:DescribeScalingActivities",
      "ec2:DescribeInstanceTypes",
      "ec2:DescribeLaunchTemplateVersions"
    ]
    effect    = "Allow"
    resources = ["*"]
  }
  statement {
    actions = [
      "autoscaling:SetDesiredCapacity",
      "autoscaling:TerminateInstanceInAutoScalingGroup"
    ]
    effect    = "Allow"
    resources = ["*"]
  }
}


resource "aws_iam_role" "cluster_autoscaler" {
  count = var.enable_cluster_autoscaler ? 1 : 0

  assume_role_policy = data.aws_iam_policy_document.cluster_autoscaler_assume_role.json
  name               = "${var.eks_name}-cluster-autoscaler"
}

resource "aws_iam_policy" "cluster_autoscaler" {
  count  = var.enable_cluster_autoscaler ? 1 : 0
  name   = "${var.eks_name}-cluster-autoscaler"
  policy = data.aws_iam_policy_document.cluster_autoscaler.json
}

resource "aws_iam_role_policy_attachment" "cluster_autoscaler" {
  count      = var.enable_cluster_autoscaler ? 1 : 0
  role       = aws_iam_role.cluster_autoscaler[0].name
  policy_arn = aws_iam_policy.cluster_autoscaler[0].arn
}
