# Cloud Viewer

Build
```shell
cd ./cloud_viewer
mkdir build && cd build
cmake ..
make
```

Run
```shell
./could_viewer
```

Only works when there is proper X11 forwarding setup.

# Smooth Resample Noisy Data via MLS

This example shows how a Moving Least Squares (MLS) surface reconstruction method can be used to smooth and resample noisy data.

Official tutorial:
http://pointclouds.org/documentation/tutorials/resampling.html#moving-least-squares

Build
```shell
cd ./smooth_resample_noisy_data_mls
mkdir build && cd build
cmake ..
make
```

Run
```shell
./resampling
```

A new file is generated at path `./data/bun0-mls.pcd`.

You could view .pcd files by any pcd viewer. Here is an online viewer:
https://www.creators3d.com/online-viewer