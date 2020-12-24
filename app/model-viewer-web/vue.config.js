const path = require('path');

module.exports = {
  "transpileDependencies": [
    "vuetify"
  ],
  // ??: What is this. Try to fix `Invalid Host header` error message on heroku deploy.
  devServer: {
    disableHostCheck: true
  },
  configureWebpack: {
    resolve: {
      alias: {
        "potree": path.resolve(__dirname, '/potree'),
      }
    },
    // module: {
    //   rules: [
    //     {
    //       test: /\.css$/i,
    //       use: ["style-loader", "css-loader"],
    //     },
    //   ],
    // },
  }
}
