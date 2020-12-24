
# Table of contents
- [Table of contents](#table-of-contents)
- [Features/technology category:](#featurestechnology-category)
    - [Point cloud to mesh](#point-cloud-to-mesh)
- [Lib/Software](#libsoftware)
  - [Modeling](#modeling)
    - [CloudCompare(c++)](#cloudcomparec)
      - [Feature](#feature)
      - [Philosophy](#philosophy)
      - [Technical considerations](#technical-considerations)
      - [Qustions and considerations](#qustions-and-considerations)
    - [MeshLab](#meshlab)
    - [Point Cloud Library(PCL)](#point-cloud-librarypcl)
  - [3D object detection](#3d-object-detection)
    - [TensorFlow](#tensorflow)
    - [PointNet](#pointnet)
    - [PCDet](#pcdet)
  - [Rendering](#rendering)
    - [Rust](#rust)
      - [three-rs](#three-rs)
      - [Others](#others)
    - [WebGL, WebGPU, and others (js/ts/rust)](#webgl-webgpu-and-others-jstsrust)
      - [Potree](#potree)
      - [PotreeConverter](#potreeconverter)
      - [plasio](#plasio)
- [Data](#data)

# Features/technology category:

### Point cloud to mesh
+ [MeshLab](#point-mesh-mashlab)
+ [PCL](#point-mesh-pcl)

# Lib/Software

## Modeling

### CloudCompare(c++)

https://cloudcompare.org/
https://github.com/cloudcompare/cloudcompare

3D point cloud (and triangular mesh) editing and processing software

#### Feature

+ Comparison between a point cloud and a triangular mesh.
+ Many other point cloud processing algorithms have followed (registration, resampling, color/normal vectors/scalar fields management, statistics computation, sensor management, interactive or automatic segmentation, etc.) as well as display enhancement tools (custom color ramps, color & normal vectors handling, calibrated pictures handling, OpenGL shaders, plugins, etc.).

#### Philosophy

**Point cloud Vs Mesh**

**Scalar fields**

https://www.cloudcompare.org/doc/wiki/index.php?title=Introduction#Scalar_fields

I thinks this represents the key word of compariation.
Seems we could even do scalar fileds math directly by multiple point could models.

#### Technical considerations

https://www.cloudcompare.org/doc/wiki/index.php?title=Introduction#Some_technical_considerations

Worth to read though "Trade-off between storage and speed".

#### Qustions and considerations
**What comparison does? Why?**

**Change detection (e.g. subsidence monitoring)**
What change detection is? Except subsidence monitoring, any other?

quote: 
https://www.cloudcompare.org/doc/wiki/index.php?title=Introduction

    Of course, as CloudCompare is meant to do **change detection (e.g. subsidence monitoring)** and as a triangular mesh is a very common way to represent a reference shape (e.g. a building), it is very useful and it couldn't be ignored. 

**In what case we need to convert point cloud to mesh?**

quote:
https://www.cloudcompare.org/doc/wiki/index.php?title=Introduction

    Of course, as CloudCompare is meant to do change detection (e.g. subsidence monitoring) and as a triangular mesh is a very common way to represent a reference shape (e.g. a building), it is very useful and it couldn't be ignored. Nevertheless it remains a "secondary" entity, especially as CloudCompare is able to compare two point clouds directly, without the need to generate an intermediary mesh.

    The main reasons for this are:

    + meshes are generally very hard to generate properly on real-life scenes, especially when scanned with a laser scanner (noise, variable density, etc.)
    + and as ALS/TLS point clouds are generally very dense (and accurate), we already have all the information we need

Since it is hard to generate seperate meshes and there is enough data stored in point coulds, in what case do we need to convert point cloud to mesh?

**!!Use it in Rust project as lib?**
Unknow

---

### MeshLab

As I know it is a tool for mesh process including simplification, clean, ...

**Point cloud to mesh.<a id="point-mesh-mashlab"></a>** Worth to do some inspection on this feature.

**Hole filling** Does it work for LoD hole filing in DS streaming project?

---

### Point Cloud Library(PCL)

https://github.com/PointCloudLibrary/pcl

**Fast triangulation of unordered point clouds: <a id="point-mesh-pcl"></a>**

https://pcl-tutorials.readthedocs.io/en/latest/greedy_projection.html#greedy-triangulation

**The CloudViewer:**

https://pcl-tutorials.readthedocs.io/en/latest/cloud_viewer.html#cloud-viewer

---
---

## 3D object detection

Detect objects in point could models.

Most of projects are on basis of ML and Deep L.

### TensorFlow

https://www.tensorflow.org/graphics/tensorboard

### PointNet

https://github.com/charlesq34/pointnet

### PCDet

https://github.com/sshaoshuai/PCDet

---
---

## Rendering

### Rust

#### three-rs

https://github.com/three-rs/three

#### Others

https://users.rust-lang.org/t/point-cloud-visualizations/31394

---

### WebGL, WebGPU, and others (js/ts/rust)

#### Potree 

Potree is a free open-source WebGL based point cloud renderer for large point clouds. It is based on the TU Wien Scanopy project and research projects Harvest4D, GCD Doctoral College and Superhumans.#### potree

https://github.com/potree/potree

#### PotreeConverter

Builds a potree octree from las, laz, binary ply, xyz or ptx files.
(Seems to create LoDs)

https://github.com/potree/PotreeConverter


#### plasio

https://github.com/verma/plasio
Drag-n-drop In-browser LAS/LAZ point cloud viewer.

---
---

# Data

[Find open data](https://data.gov.uk/)

[ASL Datasets](https://projects.asl.ethz.ch/datasets/doku.php?id=laserregistration:laserregistration)

[Oakland 3-D Point Cloud Dataset - CVPR 2009 subset](https://www.cs.cmu.edu/~vmr/datasets/oakland_3d/cvpr09/doc/)

[Lincoln Centre for Autonomous Systems](https://lcas.lincoln.ac.uk/wp/research/data-sets-software/l-cas-3d-point-cloud-people-dataset/)

[Seeking point cloud (LiDAR) data? [closed]](https://gis.stackexchange.com/questions/18202/seeking-point-cloud-lidar-dataA)

[LIDAR Point Cloud](https://data.gov.uk/dataset/977a4ca4-1759-4f26-baa7-b566bd7ca7bf/lidar-point-cloud)

[NavVis M6 Sample Point Clouds](https://www.navvis.com/m6-pointclouds)
