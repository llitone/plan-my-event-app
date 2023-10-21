const jwt = require("jsonwebtoken");
const hash = require('password-hash');
const { generateRefreshToken, validateJWT, validateEmail } = require("@helpers");
const { UserAuthModel } = require('../models');
let model = new UserAuthModel();
let redisClient;

function setRedisClient(redis) {
    redisClient = redis;
}

const root = {
    async createAccount({ name, phone_number, email, birthday, password, role }, context) {
        try {
            const newAccount = [
                name, phone_number, email, birthday, hash.generate(password), role
            ];
            let result = await model.signing(newAccount);
            console.log(result, 11221212);
            if (result != null && result.id != null) {
                console.log(result, 1);
                let token = jwt.sign({ id: result.id }, process.env.ACCESS_SECRET, { expiresIn: '10m' });
                const refresh_token = generateRefreshToken(64);
                const refresh_token_maxage = new Date() + 60 * 60 * 24 * 30;
                context.res.cookie("refresh_token", refresh_token, {
                    // secure: true,
                    httpOnly: true
                });
                await redisClient.set(String(result.id),
                    JSON.stringify({
                        refresh_token: refresh_token,
                        expires: refresh_token_maxage
                    }));
                result['token'] = token;
            } else {
                result['error'] = "Invalid email, name or password";
            }
            return result;
        } catch (err) {
            result['error'] = err;
            return result;
        }

    },
    async checkLogin({ email, password }, context) {
        try {
            let account = [email];
            // model.logging((err, arr) => {
            //     if (err) {
            //         console.log(err);
            //     } else {
            //         result = arr;
            //     };
            // }, account, password)

            // let promise = new Promise((resolve, reject) => {
            //     resolve();
            // })

            let result = await model.logging(account, password);
            console.log(result);
            if (result != null && result.id != null) {
                let token = jwt.sign({ id: result.id }, process.env.ACCESS_SECRET, { expiresIn: '10m' });
                const refresh_token = generateRefreshToken(64);
                const refresh_token_maxage = new Date() + 60 * 60 * 24 * 30;
                context.res.cookie("refresh_token", refresh_token, {
                    // secure: true,
                    httpOnly: true
                });
                await redisClient.set(String(result.id),
                    JSON.stringify({
                        refresh_token: refresh_token,
                        expires: refresh_token_maxage
                    }));
                result['token'] = token;
            } else {
                result['error'] = "Invalid email/name or password";
            }
            return result;
        } catch (err) {
            result['error'] = err;
            return result;
        }
    }
}

class UserAuthController {
    constructor() { }

    async logout(req, res) {
        try {
            let payload = jwt.verify(req.headers.authorization.split(' ')[1], process.env.ACCESS_SECRET);
            await redisClient.del(String(payload.id));
            res.clearCookie("refresh_token");
            res.send(true);
        } catch (err) {
            res.send(err);
        }
    }
}

module.exports = { UserAuthController, setRedisClient, root };