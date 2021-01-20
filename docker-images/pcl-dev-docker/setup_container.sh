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
#            --volume=`pwd`/example_project:/home/$CONTAINER_USER/docker_dir/example_project \
#            birdinforest/pcl-dev-docker:latest




#-------- Non X11 ---------------------

CONTAINER_USER=pcl

docker run -it \
        --name="pcl-docker" \
	    --cap-add sys_ptrace \
	    -p 127.0.0.1:2222:22 \
        --user=$CONTAINER_USER \
        --volume=`pwd`/docker_dir:/home/$CONTAINER_USER/docker_dir \
        --volume=`pwd`/example_project:/home/$CONTAINER_USER/docker_dir/example_project \
        birdinforest/pcl-dev-docker:latest