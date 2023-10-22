const crud = require('@db/crud')
const axios = require('axios')

function setDB(db) {
    UserModel = new crud(db.users);
}

class MainPageModel {
    constructor() { }

    async getPosts() {
        let datas = await axios.get(`${process.env.EVENT_HOST}:${process.env.EVENT_PORT}`)
        return datas.data;
    }

    async getAccountById(id) {
        console.log(id);
        let data = await UserModel.getOne({ 'id': id });
        console.log(data);
        return data;
    }
}

module.exports = { MainPageModel, setDB };
