const fs = require('fs');
const { Pool } = require('pg');
const { Sequelize } = require('sequelize');

const password = 'Awscda543' // замените на ваш пароль для пользователя postgres(!)

const pool = new Pool({
    user: 'postgres',
    host: 'localhost',
    database: 'postgres',
    password: password,
    port: 5432,
});

// Определите путь к файлу SQL
const sqlFilePath = './structure.sql';

// Функция для чтения содержимого SQL-файла
function readSqlFile(filePath) {
    return new Promise((resolve, reject) => {
        fs.readFile(filePath, 'utf8', (error, data) => {
            if (error) {
                reject(error);
                return;
            }
            resolve(data);
        });
    });
};

// Функция для выполнения сценария SQL
async function executeSqlScript() {
    try {
        const sqlScript = await readSqlFile(sqlFilePath);
        const sqlQueries = sqlScript.split(';');
        const queries = sqlQueries.filter((query) => query.trim());
        for (const query of queries) {
            await pool.query(query);
            console.log('Выполнен SQL-запрос:', query);
        }
        console.log('Импорт SQL успешно завершен.');
    } catch (error) {
        console.error('Ошибка импорта SQL:', error);
    } finally {
        pool.end();
    }
};

executeSqlScript().then(() => {
    const sequelize = new Sequelize('users_db', 'postgres', password, {
        logging: false,
        host: 'localhost',
        dialect: 'postgres',
        pool: {
            max: 10,
            min: 0,
            acquire: 20000,
            idle: 5000
        }
    });
    require('@db/usermodel')(sequelize);
    sequelize.sync().then(() => {
        console.log("All models were synchronized successfully.");
        sequelize.close();
    }).catch((err) => {
        sequelize.close();
        console.error(err)
    });
})
