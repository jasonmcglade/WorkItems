
INSERT INTO work_item 
    (title, description, created_date, version)
VALUES
    ('Work Item Title One', 'Some detailed description on the work item.', date('now','-1 day'), 1);

INSERT INTO comment
    (text, work_item_id, version)
VALUES
    ('Comment text', 1, 1);

INSERT INTO work_item 
    (title, description, created_date, version)
VALUES
    ('Work Item Title Two', 'Some detailed description on the work item.', date('now','-1 day'), 1);

INSERT INTO work_item 
    (title, description, created_date, version)
VALUES
    ('Work Item Title Three', 'Some detailed description on the work item.', date('now'), 1);

INSERT INTO work_item 
    (title, description, created_date, version)
VALUES
    ('Work Item Title Four', 'Some detailed description on the work item.', date('now','-2 day'), 1);

INSERT INTO work_item 
    (title, description, created_date, version)
VALUES
    ('Work Item Title Five', 'Some detailed description on the work item.', date('now'), 1);

INSERT INTO work_item 
    (title, description, created_date, version)
VALUES
    ('Work Item Title Six', 'Some detailed description on the work item.', date('now'), 1);

INSERT INTO work_item 
    (title, description, created_date, version)
VALUES
    ('Work Item Title Seven', 'Some detailed description on the work item.', date('now'), 1);

