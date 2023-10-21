const { buildSchema } = require("graphql");

// Схема для работы с GraphQL
const typeDefs = buildSchema(`
        scalar JSON

        type Account {
            id: ID
            name: String
            password: String
            email: String
            role: String
            phone_number: String
            createdAt: String
            updatedAt: String
            token: String
            error: String
        }

        type Post {
            id: ID
            type: String
            header: String
            description: String
            owner_id: ID
            blog_id: ID
            configuration: JSON
            photo_path: String
            hash: String
            user: JSON
        }

        type Query {
            post(id: ID): [Post]
            allposts(type: String): [Post!]!
            account: [Account!]
        }

        type Mutation {
            checkLogin(email: String!, password: String!): Account
            createAccount(name: String!, phone_number: String!, email: String!, birthday: String!, password: String!, role: String!): Account   
        }
    `)

module.exports = typeDefs;
