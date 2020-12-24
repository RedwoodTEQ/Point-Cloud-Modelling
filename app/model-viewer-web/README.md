# model-viewer-app

A web app as the point cloud model viewer.

GUI: vuetify, vue router.

Authentication, API, and database: AWS Amplify Framework.

Point cloud render (In development): [potree](https://github.com/potree/potree).

## Submodules

- Potress. The core library to provide point cloud model rendering.
- sample-model. Provide models via static http server.

## Project setup

```shell
npm install
```

Ensure to update all of submodules.

### Compiles and hot-reloads for development

To start local http server for model data:

(note: since the data model url has been changed to fetch from online,
it has been not necessary to start this local server):

```shell
npm run assets-http-server
```

To start viewer app:

```shell
npm run serve
```

Click tap 'MODELVIEWER' to viewer page.
If everything works, it loads entry file `https://127.0.0.1:1234/sobeca_1_group1_densified_point_cloud_entwine/ept.json`,
then loads all of other `.laz` files, finally render point cloud model.

### Compiles and minifies for production

```shell
npm run build
```

### Lints and fixes files

```shell
npm run lint
```

### Customize configuration

See [Configuration Reference](https://cli.vuejs.org/config/).
