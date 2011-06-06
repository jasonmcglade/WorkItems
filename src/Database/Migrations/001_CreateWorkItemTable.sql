CREATE TABLE [work_item] (
    [id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
    [title] VARCHAR(250)  NULL,
    [description] VARCHAR(500)  NULL,
    [created_date] DATE NOT NULL
);