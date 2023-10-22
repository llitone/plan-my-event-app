const { EventsModel } = require('../models');
const { GraphQLJSON } = require('graphql-type-json');
let model = new EventsModel();

const resolvers = {
    Query: {
        post: (_, { id }) => {
            return result = model.getPostById(id)
            // Возвращаем пост по его id
        },
        account: (_, { id }) => {
            return result = model.getAccountById(id)
            // Возвращаем пользователя по его id
        },
        async allposts() {
            let result = await model.getPosts();
            return result
        },
    },
    Post: {
        async account(post) {
            let result = await model.getAccountById(post.owner_id);
            return result
        }
    },
    JSON: {
        __serialize(value) {
            return GraphQLJSON.parseValue(value);
        }
    }
}

module.exports = { resolvers };
