import Vue from 'vue'
import App from './App.vue'
import axios from 'axios'

import vuetify from './plugins/vuetify';
import router from './router'

Vue.prototype.$http = axios;

Vue.config.productionTip = false

window.vm = new Vue({
  vuetify,
  router,
  render: h => h(App),
}).$mount('#app')
