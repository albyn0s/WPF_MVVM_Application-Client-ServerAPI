--DELETE FROM Employee
--DELETE FROM Department

SELECT * FROM Department
SELECT * FROM Employee

CREATE TABLE [dbo].[Employee] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (20) NULL,
    [SurName]    NVARCHAR (20) NULL,
    [Age]        NVARCHAR (20) NULL,
    [Department] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Department] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [DepName] NVARCHAR (15) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);