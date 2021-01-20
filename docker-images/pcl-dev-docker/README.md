# PCL Dev Env Image
## Links
[GitHub repo (update later)]()  
[Docker Hub image repo (update later)]()

## Credit:

This image is on basis of [pcl-docker image](https://github.com/DLopezMadrid/pcl-docker) from [DLopezMadrid](https://github.com/DLopezMadrid). I just removed NVIDIA CUDA layers and updated cmake version.

I recommend to use DLopezMadrid's image if you need CUDA.

## Overview

This image is based on Ubuntu 20.04 and has the following packages installed:
- CMake-3.19.2
- VTK-8.2.0
- PCL-1.11.0
- OpenCV-4.4.0
- Eigen
- Flann
- Boost
- gdb
- openssh
- Sublime Text
- zsh

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
```
to-do
```

### Opt2: Build it for yourself 
Build the image with 
```
$ ./build_image.sh
```

### Creat & run container
Once the image is ready, you can create & run a container called `pcl-docker` with
```
$ ./setup_container.sh  
```

Then you will enter the shell of container.

Exit & stop container by
```
exit
```

Once the container is created, there is no need to create it again unless it is deleted


Start the container again with 
```
$ ./start_container.sh
```

## PCL Project

### Build the example project

The container has mounted a volumn containing an example pcl projects.

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