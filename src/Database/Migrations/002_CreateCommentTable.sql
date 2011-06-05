CREATE TABLE [comment] (
    [id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
    [text] VARCHAR(500)  NULL,
    [user] VARCHAR(255)  NULL,
    [added_date] DATE  NULL,
    [work_item_id] INTEGER NOT NULL,
    FOREIGN KEY(work_item_id) REFERENCES work_item(id)
);