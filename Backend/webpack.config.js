const path = require('path');

module.exports = {
    // Your existing webpack configuration
    resolve: {
        fallback: {
            "buffer": require.resolve("buffer/")
        }
    },
    // Other configurations
    entry: './src/index.js', // Update this according to your entry point
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'dist')
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env', '@babel/preset-react']
                    }
                }
            },
            // Add other rules as needed
        ]
    },
    // Add other plugins or configurations as needed
};
