const webpack = require('webpack');
const path = require('path');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');

const fileExtensions = ['jpg', 'jpeg', 'png', 'gif', 'eot', 'otf', 'svg', 'ttf', 'woff', 'woff2'];

const options = {
  mode: process.env.NODE_ENV || 'development',
  entry: {
    popup: path.join(__dirname, 'src', 'js', 'popup.js'),
    options: path.join(__dirname, 'src', 'js', 'options.js'),
    background: path.join(__dirname, 'src', 'js', 'background.js'),
    'content-script': path.join(__dirname, 'src', 'js', 'content-script.js'),
  },
  output: {
    path: path.join(__dirname, 'build', 'js'),
    filename: '[name].js',
  },
  module: {
    rules: [
      {
        test: /\.m?js$/,
        exclude: /(node_modules|bower_components)/,
        use: {
          loader: 'babel-loader',
          options: {
            presets: ['@babel/preset-env'],
          },
        },
      },
      {
        test: /\.css$/,
        loader: 'style-loader!css-loader',
        exclude: /node_modules/,
      },
      {
        test: new RegExp(`.(${fileExtensions.join('|')})$`),
        loader: 'file-loader?name=[name].[ext]',
        exclude: /node_modules/,
      },
      {
        test: /\.html$/,
        loader: 'html-loader',
        exclude: /node_modules/,
      },
    ],
  },
  plugins: [
    new CleanWebpackPlugin(),
    new webpack.EnvironmentPlugin(['NODE_ENV']),
    new CopyWebpackPlugin({
      patterns: [{
        from: 'src/manifest.json',
        to: 'build/',
        transform(content) {
          return Buffer.from(JSON.stringify({
            description: process.env.npm_package_description,
            version: process.env.npm_package_version,
            ...JSON.parse(content.toString()),
          }));
        },
      }],
    }),
    new HtmlWebpackPlugin({
      title: 'Easy Apply - Popup',
      template: path.join(__dirname, 'src', 'html', 'popup.html'),
      filename: 'html/popup.html',
      chunks: ['popup'],
      minify: true,
    }),
    new HtmlWebpackPlugin({
      title: 'Easy Apply - Options',
      template: path.join(__dirname, 'src', 'html', 'options.html'),
      filename: 'html/options.html',
      chunks: ['options'],
      minify: true,
    }),
    new HtmlWebpackPlugin({
      title: 'Easy Apply - Background',
      template: path.join(__dirname, 'src', 'html', 'background.html'),
      filename: 'html/background.html',
      chunks: ['background'],
      minify: true,
    }),
  ],
};

module.exports = options;
