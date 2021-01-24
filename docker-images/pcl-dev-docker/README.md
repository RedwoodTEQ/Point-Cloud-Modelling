# PCL Dev Env Image
## Two Images

### Compiled image 

[Docker Hub]()

[Docker file](./docker/compiled-image)

This image is based on Ubuntu 20.04 and some main libraries are latest version and compiled by
source code. it has the following packages installed:
- CMake-3.19.2
- VTK-8.2.0
- PCL-1.11.0
- OpenCV-4.4.0
- Eigen
- Flann
- Boost
- gdb
- openssh
- zsh

### Minimum image

[Docker Hub](https://hub.docker.com/repository/docker/redwoodteq/pcl-docker-dev-minimum)

[Docker file](./docker/minimum-image)

This image is based on Ubuntu 20.04 and main libraries are installed via APT. Their versions are locked.
It has the following packages installed:

+ CMake-3.16.3
+ VTK-7.1.1
+ PCL-1.10.0
+ python3-opencv-4.2.0
+ gdb
+ openssh
+ zsh & oh-my-zsh

## Credit:

This image is on basis of [pcl-docker image](https://github.com/DLopezMadrid/pcl-docker) from [DLopezMadrid](https://github.com/DLopezMadrid). I just removed NVIDIA CUDA layers and updated cmake version.

I recommend to use DLopezMadrid's image if you need CUDA.

## Overview

Default user is `pcl` and password is `pcl` too.  
It starts an ssh server and exposes it in port 2222 of the host.

The idea is that the docker image will contain all the required software and libraries to develop on PCL and then we will connect from an IDE (CLion in my case) running in the host that will do "remote" compiling. 

## Clion configuration
Follow the steps [here](https://austinmorlan.com/posts/docker_clion_development/).  
Note: CMake will be installed on `/usr/local/bin/cmake` instead of on the default location

## Docker image
Clone repository and go to folder `pcl-dev-docker`

You can either choose to build the image by yourself or pull it from docker hub

### Opt1: Pull from docker-hub (update later)

Please follow instruction of docker image pulling. 

### Opt2: Build it for yourself 
Build the image with shell script `build_image.sh`
Firstly change image name by editing following lines:
```shell
IMAGE_NAME_COMPILED=birdinforest/pcl-dev-docker-comp
IMAGE_NAME_MIN=birdinforest/pcl-dev-docker-min
```
Execute shell script by:
```shell
# build compiled image
$ ./build_image.sh -i comp

# build min image
$ ./build_image.sh -i min
```

### Creat & run container
Once the image is ready, you can create a container by shell script `setup_container.sh`.
Firstly define corresponding images name and containers name:

```shell
IMAGE_NAME_COMPILED=redwoodteq/pcl-docker-dev-minimum:latest
IMAGE_NAME_MIN=redwoodteq/pcl-docker-dev-comp:latest

CONTAINER_NAME_COMPILED=pcl-docker-comp
CONTAINER_NAME_MIN=pcl-docker-min
```

then execute shell script:

```
$ ./setup_container.sh  
```

Then you will enter the shell of container.

Exit & stop container by
```
exit
```

Once the container is created, there is no need to create it again unless it is deleted

Start the container again by script `start_container.sh`
Firstly define the container names:

```shell
CONTAINER_NAME_COMPILED=pcl-docker-comp
CONTAINER_NAME_MIN=pcl-docker-min
```

Then, execute script:

```
$ ./start_container.sh
```

## PCL Project

### Build the example project

The container has mounted a volume containing an example pcl projects.

Start the container with
```
$ ./start_container.sh
```

Then run these commands within the container

```
$ cd docker_dir/example_project/cloud_viewer
$ mkdir build && cd build
$ cmake ..
$ make
```

If you have setup X11 forwarding, you can run the project then with

```
$ ./cloud_viewer
```

You may need to zoom out to see the example point clouds

### Change volume of pcl projects

Open `setup_container.sh`.
Edit following lines
```
    --volume=`pwd`/docker_dir:/home/$CONTAINER_USER/docker_dir \
    --volume=`pwd`/example_project:/home/$CONTAINER_USER/docker_dir/example_project \
```

# To-do

+ Update `setup_container.sh` and `start_container.sh` to launch GUI applications from the container using `xhost`.

+ Add an example pcl that don't need visualization.
