class CRUD {
    constructor( Model ){
        this.model = Model;
    }

    async getAll() {
        try {
            const allData = await this.model.findAll({});
            return allData;
        } catch (err) {
            return err.parent;
        }
    }

    async addOne(object) {
        try {
            let newObject = await this.model.create(object);
            return newObject
        } catch (err) {
            return err.parent;
        }
    }

    async getById(id) {
        try {
            let data = await this.model.findByPk(id);
            return data;
        } catch (error) {
            return err.parent
                ? err.parent
                : "Error retrieving data with id=" + id;

        }
    }

    async getOne(objectToFind) {
        let query = {};
        query.where = objectToFind;
        try {
            let data = await this.model.findOne(query);
            return data;
        } catch (err) {
            console.error(err);
            return err.parent
                ? err.parent
                : "Some error occused on finding data " +
                field +
                " and value" +
                fieldData;
        }
    }
}

module.exports = CRUD;