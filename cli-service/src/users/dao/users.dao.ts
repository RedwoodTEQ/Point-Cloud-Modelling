import { UserDto } from "../dto/users.model";
import shortid from "shortid";
import debug from "debug";

const log: debug.IDebugger = debug('app:in-memory-dao');

/**
 * Data access object of User.
 */
class UsersDao {
    private static instance: UsersDao;
    users: UserDto[] = [];

    constructor() {
        log('Create new instance of UsersDao');
    }

    static getInstance(): UsersDao {
        if(!UsersDao.instance) {
            UsersDao.instance = new UsersDao();
        }

        return UsersDao.instance;
    }

    async addUser(user: UserDto) {
        user.id = shortid.generate();
        this.users.push(user);
        return user.id;
    }

    async getUsers() {
        return this.users;
    }

    async getUserById(userId: string) {
        return this.users.find((user: { id: string; }) => user.id === userId);
    }

    // Update by overwriting the complete object.
    async putUserById(user: UserDto) {
        const objIndex = this.users.findIndex((obj: { id: string; }) => obj.id === user.id);
        this.users.splice(objIndex, 1, user);
        return `${user.id} updated via put`;
    }

    // Update by parts of the object.
    async patchUserById(user: UserDto) {
        const objIndex = this.users.findIndex((obj: { id: string; }) => obj.id === user.id);
        const currentUser = this.users[objIndex];
        const allowedPatchFields = ["password", "firstName", "lastName", "permissionLevel"];
        for (const field of allowedPatchFields) {
            if (field in user) {
                // @ts-ignore
                currentUser[field] = user[field];
            }
        }
        this.users.splice(objIndex, 1, currentUser);
        return `${user.id} patched`;
    }

    async removeUserById(userId: string) {
        const objIndex = this.users.findIndex((obj: { id: string; }) => obj.id === userId);
        this.users.splice(objIndex, 1);
        return `${userId} removed`;
    }

    async getUserByEmail(email: string) {
        const objIndex = this.users.findIndex((obj: { email: string; }) => obj.email === email);
        const currentUser = this.users[objIndex];
        if (currentUser) {
            return currentUser;
        } else {
            return null;
        }
    }
}

export default UsersDao.getInstance();