# A PCL development environment.
# On basis of https://github.com/DLopezMadrid/pcl-docker/blob/master/docker/Dockerfile
# Removed NVIDIA CUDA regardings.
# To configure ssh remote dev by CLion, reference to: https://austinmorlan.com/posts/docker_clion_development/

# ======== Packages ========
# CMake-3.19.2
# VTK-8.2.0
# PCL-1.11.0
# OpenCV-4.4.0
# Eigen
# Flann
# Boost
# gdb
# openssh
# zsh & oh-my-zsh

FROM ubuntu:20.04

# ======== Setup time zone ========
# https://rtfm.co.ua/en/docker-configure-tzdata-and-timezone-during-build/
ENV TZ=Australia/Melbourne
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

# ======== Installing basic dependencies ========

# Why is `DEBIAN_FRONTEND=noninteractive` discouraged in Dockerfiles:
# https://docs.docker.com/engine/faq/#why-is-debian_frontendnoninteractive-discouraged-in-dockerfiles
ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
    && apt-get install -y \
        build-essential \
        apt-utils \
        wget \
        unzip \
        git

# ======== Installing CMake ========
WORKDIR /tmp

RUN apt-get update \
    && apt-get install -y \
        libssl-dev \
        libgl1-mesa-dev

ENV CMAKE_VERSION="3.19.1"
RUN wget https://github.com/Kitware/CMake/releases/download/v${CMAKE_VERSION}/cmake-${CMAKE_VERSION}.tar.gz \
    && tar -xvf cmake-${CMAKE_VERSION}.tar.gz \
    && cd cmake-${CMAKE_VERSION} \
    && ./bootstrap -- -DCMAKE_BUILD_TYPE:STRING=Release \
    && make \
    && make install

# ======== Installing VTK ========
WORKDIR /tmp

RUN apt-get update && apt-get install -y \
    libxt-dev 

RUN wget https://www.vtk.org/files/release/8.2/VTK-8.2.0.tar.gz \
    && tar -xf VTK-8.2.0.tar.gz \
    && cd VTK-8.2.0 && mkdir build && cd build \
    && cmake .. -DVTK_MODULE_ENABLE_VTK_RenderingContextOpenGL2=YES \
                -DCMAKE_BUILD_TYPE=Release \
    && make -j$(nproc) \
    && make install

# ======== Installing PCL library ========
WORKDIR /tmp
ENV PCL_VERSION="1.11.0"

# PCL dependencies
RUN apt-get update && apt-get install -y \
    libeigen3-dev \
    libflann-dev \
    libglu1-mesa-dev \
    freeglut3-dev \
    mesa-common-dev \
    libboost-all-dev \
    libusb-1.0-0-dev \
    libusb-dev \
    libopenni-dev \
    libopenni2-dev \
    libpcap-dev \
    libpng-dev \
    mpi-default-dev \
    openmpi-bin \
    openmpi-common \
    libqhull-dev \
    libgtest-dev

RUN wget https://github.com/PointCloudLibrary/pcl/archive/pcl-${PCL_VERSION}.tar.gz \
    && tar -xf pcl-${PCL_VERSION}.tar.gz \
    && cd pcl-pcl-${PCL_VERSION} \
    && mkdir build \
    && cd build \
    && cmake .. -DCMAKE_BUILD_TYPE=Release \
                -DVTK_RENDERING_BACKEND=OpenGL2 \
    && make -j$(nproc) \
    && make install

RUN apt-get update && apt-get install -y pcl-tools

RUN unset PCL_VERSION

# ======== Installing OpenCV library ========
RUN apt-get update && apt-get install -y \
    libgtk2.0-dev \
    pkg-config \
    libavcodec-dev \
    libavformat-dev \
    libswscale-dev

WORKDIR /tmp
ENV OPENCV_VERSION="4.4.0"
RUN wget https://github.com/opencv/opencv/archive/${OPENCV_VERSION}.tar.gz \
    && tar -xf ${OPENCV_VERSION}.tar.gz \
    && cd opencv-${OPENCV_VERSION} \
    && mkdir build && cd build \
    && cmake -D CMAKE_BUILD_TYZZPE=Release -D CMAKE_INSTALL_PREFIX=/usr/local .. \
    && make -j$(nproc) \
    && make install

# ======== Installing productivity tools ========
RUN apt-get update && apt-get install -y \
    sudo \
    vim \
    terminator \
    dbus \
    dbus-x11

# ======== Install extra stuff for IDE compatibility ========
RUN apt-get update && apt-get install -y \
    gdb \
    curl \
    rsync \
    zsh \
    openssh-server

# ======== start ssh ============
RUN ssh-keygen -A  && service ssh start

# ======== user config ==========

RUN unset DEBIAN_FRONTEND

#Add new sudo user
ENV USERNAME pcl
RUN useradd -m $USERNAME && \
        echo "$USERNAME:$USERNAME" | chpasswd && \
        usermod --shell /bin/bash $USERNAME && \
        usermod -aG sudo $USERNAME && \
        echo "$USERNAME ALL=(ALL) NOPASSWD:ALL" >> /etc/sudoers.d/$USERNAME && \
        chmod 0440 /etc/sudoers.d/$USERNAME && \
        # Replace 1000 with your user/group id
        usermod  --uid 1000 $USERNAME && \
        groupmod --gid 1000 $USERNAME
WORKDIR /home/$USERNAME

# ========== install oh my zsh ===============

# the user we're applying this too (otherwise it most likely install for root)
USER $USERNAME
# terminal colors with xterm
ENV TERM xterm
# set the zsh theme
ENV ZSH_THEME agnoster

# run the installation script  
RUN wget https://github.com/robbyrussell/oh-my-zsh/raw/master/tools/install.sh -O - | zsh || true

# start zsh
CMD [ "zsh" ]