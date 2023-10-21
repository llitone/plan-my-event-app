const express = require('express');
const Router = express.Router()
const { UserAuthController, root } = require('../services')
const schema = require('@models/scheme');
const { graphqlHTTP } = require("express-graphql");
const controller = new UserAuthController();
// Класс UserAuthPage используется для определения путей запросов авторизации
class UserAuthPage {
    constructor() {
        Router.post('/', graphqlHTTP((res) => ({
            schema: schema,
            rootValue: root,
            graphiql: true,
            context: res
        })));
        Router.post('/logout', this.logout);
    }

    async logout(req, res) {
        controller.logout(req, res);
    }
}

new UserAuthPage();

module.exports = Router;
