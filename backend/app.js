const express = require('express');
const path = require('path');
require('module-alias/register');
const redis = require('redis');
const cookieParser = require('cookie-parser');
const { Sequelize } = require("sequelize");
const cors = require('cors');
require('dotenv').config();

const { authRoutes } = require('@user-auth');
const { authService } = require('@user-auth');
const { userAuthModel } = require('@user-auth');

const password = process.env.password;

const sequelize = new Sequelize('users_db', 'postgres', password, {
    // logging: false, после релиза строку ДОБАВИТЬ
    host: 'localhost',
    dialect: 'postgres',
    pool: {
        max: 10,
        min: 0,
        acquire: 20000,
        idle: 5000
    }
});
const db = {
    sequelize: sequelize,
    Sequelize: Sequelize,
}; 
db.users = require('@db/usermodel')(sequelize);
db.sequelize.sync().then(() => {
    console.log("All models were synchronized successfully.");
});

const app = express();
const client = redis.createClient('6379', '127.0.0.1');
userAuthModel.setDB(db);
authService.setRedisClient(client);

app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));
app.use(cors({
    origin: '*',
    methods: ['GET', 'POST', 'DELETE', 'UPDATE', 'PUT', 'PATCH']
}));
app.use('/auth', authRoutes);

client.on("connect", function () {
    console.log("Redis работает");
});
client.connect();

module.exports = app;