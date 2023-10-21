const hash = require('password-hash');
const sqlite3 = require('sqlite3').verbose();
const path = require('path');

class UserAuthModel {
    constructor() { }

    async signing(newAccount) {
        let data;
        let users = await new sqlite3.Database('./db/users.sqlite3');

        let query = "INSERT INTO User (name, phone_number, email, birthday, password, role) VALUES (?, ?, ?, ?, ?, ?)";
        try {
            await users.all(query, newAccount, (err, rows) => {
                if (err) {
                    console.log(err);
                } else {
                    users.all("SELECT * FROM User WHERE email = ?", newAccount[2], (err, rowss) => {
                        console.log(rowss);
                        if (err){
                            console.log(err);
                        } else {
                            data = rowss;
                            console.log(data);
                            return data;
                        }
                        users.close()
                    })
                };
            })
        } catch (err) {
            console.log(err)
        }
        
    };

    async logging(account, password) {
        let data;
        let users = await new sqlite3.Database('./db/users.sqlite3');

        let query = "SELECT * FROM User WHERE email = ?";
        try {
            await users.all(query, account, (err, rows) => {
                console.log(rows, 1)
                if (hash.verify(password, rows[0].password)) {
                    data = rows;
                } else {
                    data = -1;
                };
                console.log(data, 2222222222);
                users.close()
            })
        } catch (err) {
            console.log(err)
        }
        return data;
    }
}

module.exports = { UserAuthModel }
