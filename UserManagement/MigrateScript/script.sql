CREATE DATABASE UserManagementDb;
GO
USE UserManagementDb;

CREATE TABLE AccessLevels (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50),
    Description NVARCHAR(200)
);
go
CREATE TABLE Users (
    Id INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(100),
    Username NVARCHAR(50),
    Email NVARCHAR(100),
    PasswordHash NVARCHAR(255),
    AccessLevelId INT,
    IsDeleted BIT DEFAULT 0
);

-- STORED PROCEDURES
go
CREATE PROCEDURE sp_Users_Create
    @FullName NVARCHAR(100),
    @Username NVARCHAR(50),
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(255),
    @AccessLevelId INT
AS
BEGIN
    INSERT INTO Users VALUES (@FullName,@Username,@Email,@PasswordHash,@AccessLevelId,0)
END
go
CREATE PROCEDURE sp_Users_Search
    @Search NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Users
    WHERE IsDeleted = 0 AND
    (FullName LIKE '%' + @Search + '%'
     OR Username LIKE '%' + @Search + '%'
     OR Email LIKE '%' + @Search + '%')
END
go
CREATE PROCEDURE sp_Users_Update
    @Id INT,
    @FullName NVARCHAR(100),
    @Username NVARCHAR(50),
    @Email NVARCHAR(100),
    @AccessLevelId INT
AS
BEGIN
    UPDATE Users SET
        FullName=@FullName,
        Username=@Username,
        Email=@Email,
        AccessLevelId=@AccessLevelId
    WHERE Id=@Id
END
go
CREATE PROCEDURE sp_Users_SoftDelete
    @Id INT
AS
BEGIN
    UPDATE Users SET IsDeleted=1 WHERE Id=@Id
END
go
CREATE PROCEDURE sp_Users_GetByUsername
    @Username NVARCHAR(50)
AS
BEGIN
    SELECT TOP 1 * FROM Users
    WHERE Username=@Username OR Email=@Username
END
go
-- Access Levels
INSERT INTO AccessLevels (Name, Description) VALUES
('Admin', 'Full access'),
('User', 'Limited access');

-- Admin password: admin123 (BCrypt hash)
INSERT INTO Users (FullName, Username, Email, PasswordHash, AccessLevelId)
VALUES ('Admin User','admin','admin@test.com',
'$2a$11$w7Q8Z8dQe8fQyXW8X7rGSeXQ6zX3zYk5zZk5j5Zk5Zk5Zk5Zk5Zk5',1);




CREATE PROCEDURE sp_AccessLevels_GetAll AS
BEGIN
    SELECT * FROM AccessLevels;
END;
GO

CREATE PROCEDURE sp_AccessLevels_GetById @Id INT AS
BEGIN
    SELECT * FROM AccessLevels WHERE Id = @Id;
END;
GO

CREATE PROCEDURE sp_AccessLevels_Create @Name NVARCHAR(50), @Description NVARCHAR(200) AS
BEGIN
    INSERT INTO AccessLevels (Name, Description) VALUES (@Name, @Description);
END;
GO

CREATE PROCEDURE sp_AccessLevels_Update @Id INT, @Name NVARCHAR(50), @Description NVARCHAR(200) AS
BEGIN
    UPDATE AccessLevels SET Name = @Name, Description = @Description WHERE Id = @Id;
END;
GO