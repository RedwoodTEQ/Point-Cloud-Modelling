# On basis of https://hub.docker.com/r/connormanning/entwine/tags?page=1&ordering=last_updated
# Original maintainer: Connor Manning <connor@hobu.co>

# Entwine: 2.1.0
# PDAL: 2.2
# NodeJS: 12.18.0 (Version of docker image nodde:12-alpine)

FROM connormanning/entwine:dependencies

ARG branch=2.1.0-maintenance
RUN echo Branch: $branch
ADD https://api.github.com/repos/connormanning/entwine/commits?sha=$branch \
    /tmp/bust-cache

RUN git clone https://github.com/connormanning/entwine.git /var/entwine && \
    cd /var/entwine && \
    git checkout $branch && \
    mkdir build && \
    cd build && \
    cmake -G Ninja \
        -DCMAKE_INSTALL_PREFIX=/usr \
        -DCMAKE_BUILD_TYPE=Release .. && \
    ninja && \
    ninja install

# Install NodeJS
ENV NODE_VERSION=12.18.0
RUN apt install -y curl
RUN curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.34.0/install.sh | bash
ENV NVM_DIR=/root/.nvm
RUN . "$NVM_DIR/nvm.sh" && nvm install ${NODE_VERSION}
RUN . "$NVM_DIR/nvm.sh" && nvm use v${NODE_VERSION}
RUN . "$NVM_DIR/nvm.sh" && nvm alias default v${NODE_VERSION}
ENV PATH="/root/.nvm/versions/node/v${NODE_VERSION}/bin/:${PATH}"
RUN node --version
RUN npm --version