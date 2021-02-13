import express from 'express';
import modelService from '../services/models.service';

/**
 * Models middleware validates request before sending them to model controller:
 */
class ModelsMiddleware {
    private static instance: ModelsMiddleware;

    static getInstance() {
        if (!ModelsMiddleware.instance) {
            ModelsMiddleware.instance = new ModelsMiddleware();
        }
        return ModelsMiddleware.instance;
    }

    async extractModelName(req: express.Request, res: express.Response, next: express.NextFunction) {
        req.body.id = req.params.modelName;
        next();
    }

    async validateRequiredModelBodyFields(req: express.Request, res: express.Response, next: express.NextFunction) {
        if (req.body && (req.body.name || req.body.path) && req.body.command) {
            next();
        } else {
            res.status(400).send({error: `Missing required fields: model name or model path, and/or cli command`});
        }
    }

    async validateModelExists(req: express.Request, res: express.Response, next: express.NextFunction) {
        const model = await modelService.readByName(req.params.modelName);
        if (model) {
            next();
        } else {
            res.status(404).send({error: `Model ${req.params.modelName} not found`});
        }
    }
}

export default ModelsMiddleware.getInstance();