"use strict";

var path = require("path");
var WebpackNotifierPlugin = require("webpack-notifier");
var BrowserSyncPlugin = require("browser-sync-webpack-plugin");
var webpack = require("webpack");

module.exports = {
    entry: "./index.js",
    output: {
        path: path.resolve(__dirname, "../es6-dist"),
        filename: "bundle.js"
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader"
                }
            }
        ]
    },
    devtool: "inline-source-map",
    plugins: [
        new WebpackNotifierPlugin(),
        new BrowserSyncPlugin(),
        new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/)
    ]
};