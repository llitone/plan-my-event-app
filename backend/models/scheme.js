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
            creator_id: ID
            name: String
            description: String
            created_at: String
            start_at: String
            address: String
            price: String
            category: JSON
            user: Account
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
