FROM alpine:3.19
ARG PLATFORM=Linux_amd64
WORKDIR /tmp/setup
RUN apk add bash aws-cli curl jq k9s helm
RUN curl --silent -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"
RUN curl --silent -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl.sha256"
RUN echo "$(cat kubectl.sha256)  kubectl" | sha256sum -c
RUN chmod +x kubectl
RUN mv ./kubectl /usr/local/bin/kubectl
RUN curl -sLO "https://github.com/eksctl-io/eksctl/releases/latest/download/eksctl_$PLATFORM.tar.gz"
RUN curl -sL "https://github.com/eksctl-io/eksctl/releases/latest/download/eksctl_checksums.txt" | grep $PLATFORM | sha256sum -c
RUN tar -xzf eksctl_$PLATFORM.tar.gz -C /tmp/setup && chmod +x eksctl && rm eksctl_$PLATFORM.tar.gz
RUN mv /tmp/setup/eksctl /usr/local/bin
WORKDIR /workdir
RUN rm -Rf /tmp/setup
