import { CommonRoutesConfig } from '../common/common.routes.config';
import ModelsController from './controllers/models.controller';
import ModelsMiddleware from './middleware/models.middleware';
import express from 'express';

export class ModelsRoutes extends CommonRoutesConfig {
    constructor(app: express.Application) {
        super(app, 'ModelsRoutes');
    }

    configureRoutes() {
        this.app.route(`/models`)
            .get(ModelsController.listModels)
            .post(
                ModelsMiddleware.validateRequiredModelBodyFields,
                ModelsController.processModel);

        this.app.param(`modelName`, ModelsMiddleware.extractModelName);
        this.app.route(`/models/:modelName`)
            .all(ModelsMiddleware.validateModelExists)
            .get(ModelsController.getModelByName)

        return this.app;
    }

}