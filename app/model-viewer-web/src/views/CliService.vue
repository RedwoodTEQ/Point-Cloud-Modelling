<template>
  <div class="CliService">
    <v-row row d-flex nowrap align="center" justify="center" class="px-2">
      <v-col cols="12" md="12">
        <v-text-field
            v-model="apiGet"
            filled
            label="API"
            clearable
            required
        ></v-text-field>
      </v-col>
      <v-btn @click="apiGET(apiGet)">
        Get
      </v-btn>
    </v-row>
    <v-row row d-flex nowrap align="center" justify="center" class="px-2">
      <v-col cols="12" md="12">
        <v-text-field
            v-model="apiPost"
            filled
            label="API"
            clearable
            required
        ></v-text-field>
      </v-col>
      <v-col cols="12" md="12">
        <v-text-field
            v-model="apiResBody"
            filled
            label="Body"
            clearable
            required
        ></v-text-field>
      </v-col>
      <v-btn @click="apiPOST(apiPost, apiResBody)">
        POST
      </v-btn>
    </v-row>
    <v-row d-flex justify="center" class="px-2">
    <div v-show="info && info.data && info.data && info.data[0] && info.data[0].name !== undefined" id="models-table">
        <p>Model Tables</p>
        <v-simple-table style="max-height: 200px" class="overflow-y-auto">
            <thead>
            <tr>
              <th class="text-left">
                Name
              </th>
              <th class="text-left">
                Path
              </th>
            </tr>
            </thead>
            <tbody>
            <tr
                v-for="item in info.data"
                :key="item.name"
              >
              <td class="text-left">{{ item.name }}</td>
              <td class="text-left">{{ item.path}}</td>
            </tr>
            </tbody>
        </v-simple-table>
      </div>
    </v-row>
    <v-row d-flex justify="center" class="px-2">
      <div id="api-response" style="max-height: 200px" class="overflow-y-auto">
        <p></p>
        <p>API Response</p>
        <pre>{{ JSON.stringify(info, null, 2) }}</pre>
      </div>
    </v-row>
  </div>
</template>

<script>

export default {
  name: "CliService",

  data: function() {
    return {
      apiGet: 'http://127.0.0.1:3000/models',
      apiPost: 'http://127.0.0.1:3000/models',
      apiResBody: '{"path":"sobeca_1_group1_densified_point_cloud.las","command": "entwine"}',
      info: {},
    }
  },
  created() {
    console.log('CliService created');
  },
  mounted() {
    console.log('CliService mounted');
  },
  methods: {
    apiGET: function(api) {
      console.log('GET: ', api);
      this.$http
          .get(api)
          .then(response => (this.info = response))
          .catch(error => (this.info = `${error.response.status}: ${error.response.data.error}`));
    },
    apiPOST: function(api, data) {
      console.log('POST: ', api, ' Body: ', data);
      this.$http
          .post(api, JSON.parse(data))
          .then(response => (this.info = response))
          .catch(error => (this.info = `${error.response.status}: ${error.response.data.error}`));
    }
  }
}

</script>

<style>
</style>