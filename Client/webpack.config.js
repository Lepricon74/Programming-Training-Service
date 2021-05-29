const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
module.exports = {
    entry: "./src/index.js",
    output: {
        path: path.join(__dirname, "/dist"),
        filename: "index-bundle.js"
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: ["babel-loader"]
            },
            {
                test: /\.css$/,
                use: ["style-loader", "css-loader"]
            }
        ]
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: "./src/index.html"
        })
    ],

    devServer: {
        port: 8080,
        historyApiFallback: true,
        proxy: {
            '/api': {
            target: 'https://localhost:44318',
            secure: false              
            },
            '/Files': {
                target: 'https://localhost:44318',
                secure: false              
                },
            '/account': {
                target: 'https://localhost:44318',
                secure: false              
                },
            '/Account': {
                target: 'https://localhost:44318',
                secure: false              
                },
        }
    },
    //debug: true,
    devtool: 'source-map'
};