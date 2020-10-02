const path = require('path');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');

const fileExtensions = ['jpg', 'jpeg', 'png', 'gif', 'eot', 'otf', 'svg', 'ttf', 'woff', 'woff2'];

const options = {
  mode: 'production',
  entry: {
    popup: path.join(__dirname, 'src', 'js', 'popup.js'),
    options: path.join(__dirname, 'src', 'js', 'options.js'),
    background: path.join(__dirname, 'src', 'js', 'background.js'),
    'content-scripts': path.join(__dirname, 'src', 'js', 'content-scripts.js'),
  },
  output: {
    path: path.join(__dirname, 'build'),
    filename: 'js/[name].js',
  },
  module: {
    rules: [
      {
        test: /\.m?js$/,
        exclude: /(node_modules|bower_components)/,
        use: {
          loader: 'babel-loader',
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
    new CopyWebpackPlugin({
      patterns: [
        {
          from: 'src/manifest.json',
        },
      ],
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
  ],
};

module.exports = options;
