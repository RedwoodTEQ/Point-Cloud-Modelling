
// Import express to add types to the request/response objects from our controller functions
import express from 'express';

import modelsService from '../services/models.service';

// Import the argon2 library for password hashing
import argon2 from 'argon2';

import debug from 'debug';
import usersService from "../../users/services/users.service";

const log: debug.IDebugger = debug('app:users-controller');

/**
 *  Users controller separates the route configuration from
 *  the code that finally processes a route request.
 */
class ModelsController {
    private static instance: ModelsController;

    // this will be a controller singleton (same pattern as before)
    static getInstance(): ModelsController {
        if (!ModelsController.instance) {
            ModelsController.instance = new ModelsController();
        }
        return ModelsController.instance;
    }

    async listModels(req: express.Request, res: express.Response) {
        const models = await modelsService.list(100, 0);
        res.status(200).send(models);
    }

    async getModelByName(req: express.Request, res: express.Response) {
        const model = await modelsService.readByName(req.params.modelName);
        res.status(200).send(model);
    }

    /**
     * Process model.
     * As the demo, it process model by Entwine command line tool.
     * @param req
     *  @param req.body.path - Path of model in local disk.
     *  @param req.body.command - Only supports command 'entwine'
     * @param res
     */
    async processModel(req: express.Request, res: express.Response) {
        const path = req.body.path;
        const command = req.body.command;

        const result = await modelsService.processModel(path, command);

        if(result instanceof Error) {
            res.status(500).send({
                message: result,
                data: {
                    name: result.name,
                    stack: result.stack,
                },
            });
        } else if(result.error) {
            res.status(500).send({
                message: result.error,
            });
        } else {
            res.status(200).send({
                message: 'Success',
                data: {
                    output: `${result.output}`
                },
            });
        }
    }
}

export default ModelsController.getInstance();