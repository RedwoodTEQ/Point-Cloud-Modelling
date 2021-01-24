#!/bin/bash

# ----------- X11 ---------------

# XSOCK=/tmp/.X11-unix
# XAUTH=/tmp/.docker.xauth
# touch $XAUTH
# # xauth nlist $DISPLAY | sed -e 's/^..../ffff/' | xauth -f $XAUTH nmerge -
# xhost +local:root
# # to set up the right environment variables in CLion
# echo "Set \$DISPLAY parameter to $DISPLAY" 

# docker start pcl-docker
# docker exec pcl-docker sudo service ssh start
# docker attach pcl-docker

# # disallow x server connection
# xhost -local:root


# ------------ Non X11 ----------

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

if [ "$parameterI" == "min" ]; then
    CONTAINER_NAME=pcl-docker-min
elif [ "$parameterI" == "comp" ]; then
    CONTAINER_NAME=pcl-docker-comp
else
    helpFunction
fi

docker start ${CONTAINER_NAME}
docker exec ${CONTAINER_NAME} sudo service ssh start
docker attach ${CONTAINER_NAME}