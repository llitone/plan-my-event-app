const sqlite3 = require('sqlite3').verbose();
const db = new sqlite3.Database("./users.sqlite3", (err) => {
    if (err) {
        console.log(err.message);
    }
    console.log("Connected");
});

db.serialize(() => {
    db.run(`
    CREATE TABLE IF NOT EXISTS User (
        id INTEGER UNIQUE,
        name TEXT NOT NULL,
        phone_number TEXT NOT NULL,
        email TEXT NOT NULL UNIQUE,
        birthday TEXT NOT NULL,
        password TEXT NOT NULL,
        image_user TEXT,
        role TEXT NOT NULL,
        createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
        updatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
        PRIMARY KEY (id)
    )
    `, e => console.log(e))
})

db.close()