import express from 'express';
import * as http from 'http';
import * as bodyparser from 'body-parser';

// Logging requests to our API and the responses (and errors) returned
import * as winston from 'winston';
import * as expressWinston from 'express-winston';
// Allows to enable cross-origin resource sharing.
import cors from 'cors';
// Print debug info while developing. It switches off in production.
// https://github.com/visionmedia/debug#usage
// Set environment variable in npm script `start` of `package.json`: `"start": "export DEBUG=* && ts-node ."`
// Maybe you need to change `export` to be `SET` on windows.
import debug from 'debug';

import {CommonRoutesConfig} from './common/common.routes.config';
import {UsersRoutes} from './users/users.routes.config';
import {ModelsRoutes} from "./models/models.routes.config";

const app: express.Application = express();
const server: http.Server = http.createServer(app);
const port: number = 3000;
const routes: CommonRoutesConfig[] = [];
const debugLog: debug.IDebugger = debug('app');

// Middleware to parse all incoming requests as JSON
app.use(bodyparser.json());

// Middleware to allow cross-origin requests
app.use(cors());

// Configure the expressWinston logging middleware,
// which will automatically log all HTTP requests handled by Express.js
app.use(expressWinston.logger({
    transports: [
        new winston.transports.Console()
    ],
    format: winston.format.combine(
        winston.format.colorize(),
        winston.format.json()
    )
}));

// Adding the UserRoutes to our array,
// after sending the Express.js application object to have the routes added to our app!
routes.push(new UsersRoutes(app));
routes.push(new ModelsRoutes(app));

// Configure the expressWinston error-logging middleware,
// which doesn't *handle* errors per se, but does *log* them
app.use(expressWinston.errorLogger({
    transports: [
        new winston.transports.Console()
    ],
    format: winston.format.combine(
        winston.format.colorize(),
        winston.format.json()
    )
}));

// A simple route to make sure everything is working properly
app.get('/', (req: express.Request, res: express.Response) => {
    res.status(200).send(`Server up and running!`)
});

// Start server
server.listen(port, () => {
    debugLog(`Server running at http://localhost:${port}`);
    routes.forEach((route: CommonRoutesConfig) => {
        debugLog(`Routes configured for ${route.getName()}`);
    });
});