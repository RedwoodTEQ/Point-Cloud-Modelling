#!/bin/bash

helpFunction()
{
   echo ""
   echo "Usage: $0 -i [comp | min]"
   echo -e "comp: \tBuild compiled image."
   echo -e "min: \t\tBuild minimum image."
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

if [ "$parameterI" == "comp" ]; then
   echo "Build compiled image.";
   docker build ./docker/compiled-image -f docker/compiled-image/Dockerfile -t birdinforest/pcl-dev-docker
elif [ "$parameterI" == "min" ]; then
   echo "Build minimum image.";
   docker build ./docker -f docker/minimum-image/Dockerfile -t birdinforest/pcl-dev-docker-min
 else
   helpFunction
fi
