const express = require('express');
const Router = express.Router();
const { resolvers } = require('../services');
const typeDefs = require('@models/scheme');
const { makeExecutableSchema } = require('graphql-tools');
const { graphqlHTTP } = require("express-graphql");

const schema = makeExecutableSchema({
    typeDefs,
    resolvers,
});

class MainPage {
    constructor() {
        Router.post('/', graphqlHTTP((res) => ({
            schema: schema,
            graphiql: true,
            context: res
        })));
    }
}

new MainPage();

module.exports = Router;
