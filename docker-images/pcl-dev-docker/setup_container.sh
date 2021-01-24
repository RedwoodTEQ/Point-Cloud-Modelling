#!/bin/bash

#-------- X11 ---------------------

# XSOCK=/tmp/.X11-unix
# XAUTH=/tmp/.docker.xauth
# touch $XAUTH
# xauth nlist $DISPLAY | sed -e 's/^..../ffff/' | xauth -f $XAUTH nmerge -
# CONTAINER_USER=pcl

# ip=$(ifconfig en0 | grep inet | awk '$1=="inet" {print $2}')

# docker run -it \
#            --volume=$XSOCK:$XSOCK:rw \
#            --volume=$XAUTH:$XAUTH:rw \
#            --env="XAUTHORITY=${XAUTH}" \
#            --env="DISPLAY=${ip}:0" \
# 	         --name="pcl-docker" \
# 	         --cap-add sys_ptrace \
# 	         -p 127.0.0.1:2222:22 \
#            --user=$CONTAINER_USER \
#            --volume=`pwd`/docker_dir:/home/$CONTAINER_USER/docker_dir \
#            --volume=`pwd`/example_projects:/home/$CONTAINER_USER/docker_dir/example_projects \
#            birdinforest/pcl-dev-docker:latest




#-------- Non X11 ---------------------

helpFunction()
{
   echo ""
   echo "Usage: $0 -i [comp | min]"
   echo -e "comp: \tCreate container by image birdinforest/pcl-dev-docker."
   echo -e "min: \tCreate container by image birdinforest/pcl-dev-docker-min."
   exit 1 # Exit script after printing help
}

while getopts "i:" opt
do
   case "${opt}" in
      i) parameterI="$OPTARG" ;;
      ?) helpFunction ;; # Print helpFunction in case parameter is non-existent
   esac
done

# Print helpFunction in case parameters are empty
if [ -z "$parameterI" ]
then
   echo "Some or all of the parameters are empty";
   helpFunction
fi

CONTAINER_USER=pcl

if [ "$parameterI" == "min" ]; then
    IMAGE=birdinforest/pcl-dev-docker-min:latest
    CONTAINER_NAME=pcl-docker-min
elif [ "$parameterI" == "comp" ]; then
    IMAGE=birdinforest/pcl-dev-docker:latest
    CONTAINER_NAME=pcl-docker-comp
else
    helpFunction
fi

echo "Setup container by image ${IMAGE}"

docker run -it \
      --name=${CONTAINER_NAME} \
	    --cap-add sys_ptrace \
	    -p 127.0.0.1:2222:22 \
        --user=$CONTAINER_USER \
        --volume=`pwd`/docker_dir:/home/$CONTAINER_USER/docker_dir \
        --volume=`pwd`/example_projects:/home/$CONTAINER_USER/docker_dir/example_projects \
        ${IMAGE}