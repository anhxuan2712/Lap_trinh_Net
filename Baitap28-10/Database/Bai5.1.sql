USE login_db;

INSERT IGNORE INTO Users (Username, Password)
VALUES ('admin', '123456');

SELECT * FROM Users;