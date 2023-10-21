const jwt = require('jsonwebtoken');

function generateRefreshToken(len) {
    var text = "";
    var charset = "abcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < len; i++)
        text += charset.charAt(Math.floor(Math.random() * charset.length));

    return text;
};

function validateEmail(email) {
    return email.match(
        /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
};

async function validateJWT(req, res, redisClient) {
    let accesstoken = req.headers.authorization.split(' ')[1] || null;
    let refreshtoken = req.cookies.refresh_token || null;
    if (accesstoken && refreshtoken) {
        try {
            const payload = jwt.verify(accesstoken, process.env.ACCESS_SECRET);
            const userid = payload.id;
            let redis_token = JSON.parse(await redisClient.get(String(userid), function (err, val) {
                if (err) return err;
                else return val;
            }));
            if (!redis_token || redis_token.refresh_token != refreshtoken) {
                return false;
            } else {
                if (redis_token.expires > new Date()) {
                    let refresh_token = generateRefreshToken(64);
                    res.cookie("refresh_token", refresh_token, {
                        // secure: true,
                        httpOnly: true
                    });
                    redisClient.set(String(userid),
                        JSON.stringify({
                            refresh_token: refresh_token,
                            expires: new Date() + 60 * 60 * 24 * 30
                        })
                    );
                }
                return true;
            }
        } catch (error) {
            if (error instanceof jwt.TokenExpiredError) {
                const payload = jwt.decode(accesstoken);
                const userid = payload.id;
                let redis_token = JSON.parse(await redisClient.get(String(userid), function (err, val) {
                    if (err) return err;
                    else return val;
                }));
                if (!redis_token || redis_token.refresh_token != refreshtoken) {
                    return false;
                } else {
                    if (redis_token.expires > new Date()) {
                        let refresh_token = generateRefreshToken(64);
                        res.cookie("refresh_token", refresh_token, {
                            // secure: true,
                            httpOnly: true
                        });
                        redisClient.set(String(userid),
                            JSON.stringify({
                                refresh_token: refresh_token,
                                expires: new Date() + 60 * 60 * 24 * 30
                            })
                        );
                    }
                    let token = jwt.sign({ id: userid }, process.env.ACCESS_SECRET, {
                        expiresIn: '10m'
                    });
                    return token;
                }
            } else {
                return error;
            }
        }
    } else {
        return "Токена нет";
    };
};

module.exports = { generateRefreshToken, validateJWT, validateEmail }
