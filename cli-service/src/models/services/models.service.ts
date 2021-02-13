import modelsDao from '../dao/models.dao';
import { CRUD } from "../../common/interfaces/crud.interface";
import { ModelDto } from "../dto/models.model";
import {UserDto} from "../../users/dto/users.model";
import usersDao from "../../users/dao/users.dao";

class ModelsService implements CRUD {
    private static instance: ModelsService;

    static getInstance(): ModelsService {
        if (!ModelsService.instance) {
            ModelsService.instance = new ModelsService();
        }
        return ModelsService.instance;
    }

    async create(resource: ModelDto) {
        return await modelsDao.addModel(resource);
    }

    async list(limit: number, page: number) { // limit and page are ignored until we upgrade our DAO
        return await modelsDao.getModels();
    };

    async readById(resourceId: string) {
        return await modelsDao.getModelById(resourceId);
    };

    async readByName(modelName: string) {
        return await modelsDao.getModelByName(modelName);
    };

    async readByPath(modelPath: string) {
        return await modelsDao.getModelByPath(modelPath);
    };

    async updateById(modelId: string) {
        return await 'models.server.updateById is not implement.';
    };

    async deleteById(modelId: string) {
        return await 'models.server.deleteById is not implementated.';
    };

    async patchById(modelId: string) {
        return await 'models.server.patchById is not implementated.';
    }

    async processModel(modelPath: string, command: string) {
        return await modelsDao.processModelByPath(modelPath, command);
    }
}

export default ModelsService.getInstance();