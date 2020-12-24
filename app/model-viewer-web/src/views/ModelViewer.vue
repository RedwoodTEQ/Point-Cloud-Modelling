<template>
  <div class="ModelViewer">
        <v-row row d-flex nowrap align="center" justify="center" class="px-2">
          <v-col cols="10" md="8">
            <v-text-field
                v-model="url"
                filled
                label="URL"
                clearable
                required
            ></v-text-field>
          </v-col>
          <v-btn @click="loadModel(url)">
              load
          </v-btn>
        </v-row>
    <v-row align="center">
      <div class="row potree_container" style="width: 100%; height: 100%; left: 0px; top: 0px; ">
        <div ref="potree_render_area" id="potree_render_area"
             style="position: relative; height:700px; width: 100%; left: 25px; margin-right: 25px"></div>
        <div id="potree_sidebar_container"> </div>
      </div>
    </v-row>
  </div>
</template>

<script>

let viewer;

export default {
  name: "ModelViewer",

  data: function() {
    return {
      url: 'http://127.0.0.1:1234/sobeca_1_group1_densified_point_cloud_entwine/ept.json',
      viewer: {},
    }
  },
  created() {
    console.log('ModelViewer created');
  },
  mounted() {
    /** @type ViewerOptions */
    const viewerOptions = {
      canvasOpts: {
        position: 'relative',
      },
      sidebarOpts: {
        height: '700px',
      }
    }
    // eslint-disable-next-line no-undef
    viewer = new Potree.Viewer(this.$refs.potree_render_area, viewerOptions);

    viewer.setEDLEnabled(false);
    viewer.setFOV(60);
    viewer.setPointBudget(2*1000*1000);
    viewer.loadSettingsFromURL();

    viewer.setDescription("Loading Entwine-generated EPT format");

    viewer.loadGUI(() => {
      viewer.setLanguage('en');
      // eslint-disable-next-line no-undef
      $("#menu_appearance").next().show();
    });
  },
  methods: {
    loadModel: function(url) {
      console.log('submit');
      console.log(url);

      // TODO: Error msg UI popup.
      if(!url) {
        throw Error("Given url is null.");
      }
      if(url.split('.').pop() !== 'json') {
        throw Error(`Given url has to be a JSON file. Given url: ${url}`);
      }

      let name = 'unknown';
      const segments = url.split('/').reverse();
      if(segments) {
        segments.length > 0 ? name = segments[1] : name = segments[0];
      }

      // eslint-disable-next-line no-undef
      Potree.loadPointCloud(url, name, function(e){
        viewer.scene.addPointCloud(e.pointcloud);

        let material = e.pointcloud.material;
        material.size = 1;
        // eslint-disable-next-line no-undef
        material.pointSizeType = Potree.PointSizeType.ADAPTIVE;

        viewer.fitToScreen(0.5);
      });

    }
  }
}

</script>

<style>
#potree_sidebar_container {
  height: 700px;
  left: 1px;
}

</style>

<style src="../../public/potree-build/potree/potree.css"></style>
<style src="../../public/libs/jquery-ui/jquery-ui.min.css"></style>
<style src="../../public/libs/jquery-ui/jquery-ui.min.css"></style>
<style src="../../public/libs/openlayers3/ol.css"></style>
<style src="../../public/libs/spectrum/spectrum.css"></style>
<style src="../../public/libs/jstree/themes/mixed/style.css"></style>
