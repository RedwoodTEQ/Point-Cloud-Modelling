import express from 'express';
import userService from '../services/users.service';

/**
 * Users middleware validates request before sending them to users controller:
 * 1. Ensure the presence of user fields such as email and password as required to create or update a user
 * 2. Ensure a given email isn’t in use already
 * 3. Check that we’re not changing the email field after creation (since we’re using that as the primary user-facing ID for simplicity)
 * 4. Validate whether a given user exists
 */
class UsersMiddleware {
    private static instance: UsersMiddleware;

    static getInstance() {
        if (!UsersMiddleware.instance) {
            UsersMiddleware.instance = new UsersMiddleware();
        }
        return UsersMiddleware.instance;
    }

    /**
     * Extract the userId from the request parameters—coming in from the request URL itself—and add it to the request body,
     * where the rest of the user data resides.
     * The idea here is to be able to simply use the full body request when we would like to update user information,
     * without worrying about getting the ID from the parameters every time.
     */
    async extractUserId(req: express.Request, res: express.Response, next: express.NextFunction) {
        req.body.id = req.params.userId;
        next();
    }

    async validateRequiredUserBodyFields(req: express.Request, res: express.Response, next: express.NextFunction) {
        if (req.body && req.body.email && req.body.password) {
            next();
        } else {
            res.status(400).send({error: `Missing required fields: email and/or password`});
        }
    }

    async validateSameEmailDoesntExist(req: express.Request, res: express.Response, next: express.NextFunction) {
        const user = await userService.getUserByEmail(req.body.email);
        if (user) {
            res.status(400).send({error: `User email already exists`});
        } else {
            next();
        }
    }

    async validateSameEmailBelongToSameUser(req: express.Request, res: express.Response, next: express.NextFunction) {
        const user = await userService.getUserByEmail(req.body.email);
        if (user && user.id === req.params.userId) {
            next();
        } else {
            res.status(400).send({error: `Invalid email`});
        }
    }

    async validatePatchEmail(req: express.Request, res: express.Response, next: express.NextFunction) {
        if (req.body.email) {
            UsersMiddleware.getInstance().validateSameEmailBelongToSameUser(req, res, next);
        } else {
            next();
        }
    }

    async validateUserExists(req: express.Request, res: express.Response, next: express.NextFunction) {
        const user = await userService.readById(req.params.userId);
        if (user) {
            next();
        } else {
            res.status(404).send({error: `User ${req.params.userId} not found`});
        }
    }
}

export default UsersMiddleware.getInstance();