const hash = require('password-hash');
const crud = require('@db/crud.js')

function setDB(db) {
    UsersModel = new crud(db.users);
}

class UserAuthModel {
    constructor() { }

    async signing(newAccount) {
        let data = await UsersModel.addOne(newAccount);
        return data;
    };

    async logging(objectToFind, password) {
        let data = await UsersModel.getOne(objectToFind)
        let user = data['dataValues'];
        console.log(user);
        if (hash.verify(password, user.password)) {
            return data
        } else {
            return -1;
        }
    }
}

module.exports = { UserAuthModel, setDB }
