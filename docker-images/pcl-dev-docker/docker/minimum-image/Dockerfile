# A minimum PCL development environment on basis of Ubuntu 20.04.
# Install PCL and CMake by apt.
# To configure ssh remote dev by CLion, reference to: https://austinmorlan.com/posts/docker_clion_development/

# ======== Packages ========
# CMake-3.16.3
# VTK-7.1.1
# PCL-1.10.0
# python3-opencv-4.2.0
# gdb
# openssh
# zsh & oh-my-zsh

FROM ubuntu:20.04

# ======== Disable GUI ========
# Why is `DEBIAN_FRONTEND=noninteractive` discouraged in Dockerfiles:
# https://docs.docker.com/engine/faq/#why-is-debian_frontendnoninteractive-discouraged-in-dockerfiles
ENV DEBIAN_FRONTEND=noninteractive

# ======== Installing basic dependencies ========
RUN apt-get update && apt-get install -y \
        build-essential \
        apt-utils \
        wget \
        unzip \
        git

# ======== Installing CMake ========
ENV CMAKE_VERSION="3.16.3-1ubuntu1"
RUN apt-get update && apt-get install -y cmake=${CMAKE_VERSION}
RUN unset CMAKE_VERSION

# ======== Installing PCL library ========
ENV PCL_VERSION="1.10.0+dfsg-5ubuntu1"
RUN apt-get update && apt-get install -y libpcl-dev=${PCL_VERSION}
RUN unset PCL_VERSION

# ======== Installing OpenCV library ========
ENV OPENCV_VERSION="4.2.0+dfsg-5"
RUN apt-get update && apt-get install -y python3-opencv=${OPENCV_VERSION}
RUN unset OPENCV_VERSION

# ======== Installing productivity tools ========
RUN apt-get update && apt-get install -y \
    sudo \
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