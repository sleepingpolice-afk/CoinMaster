CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE Player (
    PlayerID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    Currency BIGINT NOT NULL DEFAULT 0,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    Username VARCHAR(255) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Upgrades (
    UpgradeID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    Name VARCHAR(255) NOT NULL,
    base_cost BIGINT NOT NULL,
    cost_mult REAL NOT NULL,
    description TEXT NOT NULL
);

CREATE TABLE Skill (
    SkillID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    Name VARCHAR(255) NOT NULL,
    duration REAL NOT NULL,
    cooldown REAL NOT NULL,
    description TEXT NOT NULL
);

CREATE TABLE Weapon (
    WeaponID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    Name VARCHAR(255) NOT NULL,
    cost BIGINT NOT NULL,
    description TEXT NOT NULL
);

CREATE TABLE player_upgrades (
    PUpgradesID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    PlayerID UUID NOT NULL REFERENCES Player(PlayerID) ON DELETE CASCADE,
    UpgradeID UUID NOT NULL REFERENCES Upgrades(UpgradeID) ON DELETE CASCADE,
    Level INT NOT NULL DEFAULT 1
);

CREATE TABLE player_skills (
    PSkillsID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    PlayerID UUID NOT NULL REFERENCES Player(PlayerID) ON DELETE CASCADE,
    SkillID UUID NOT NULL REFERENCES Skill(SkillID) ON DELETE CASCADE
);

CREATE TABLE attack_log(
    LogID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    AttackerID UUID NOT NULL REFERENCES Player(PlayerID) ON DELETE CASCADE,
    DefenderID UUID NOT NULL REFERENCES Player(PlayerID) ON DELETE CASCADE,
    attacked TIMESTAMP NOT NULL DEFAULT NOW()
);

INSERT INTO Upgrades (Name, base_cost, cost_mult, description) VALUES
('Production', 100, 2, 'Increase Production Value'),
('Click', 10, 1.5, 'Increase Click Value');

INSERT INTO Skill (Name, duration, cooldown, description) VALUES
('DoubleProduction', 10.0, 5.0, 'Doubles production for 5 seconds. Cooldown 5 seconds.'),
('DoubleClick', 10.0, 5.0, 'Doubles click value for 5 seconds. Cooldown 5 seconds.');

INSERT INTO Weapon (Name, cost, description) VALUES
('Weapon 1', 200, 'Description for Weapon 1');

INSERT INTO Player (Currency, Username, Password, Email) VALUES
(100, 'player1', 'password1', 'player1@example.com'),
(200, 'player2', 'password2', 'player2@example.com'),
(300, 'player3', 'password3', 'player3@example.com');

INSERT INTO player_upgrades (PlayerID, UpgradeID, Level) VALUES
((SELECT PlayerID FROM Player WHERE Username = 'player1'),
 (SELECT UpgradeID FROM Upgrades WHERE Name = 'Click'), 1),
((SELECT PlayerID FROM Player WHERE Username = 'player2'),
 (SELECT UpgradeID FROM Upgrades WHERE Name = 'Click'), 1),
((SELECT PlayerID FROM Player WHERE Username = 'player3'),
 (SELECT UpgradeID FROM Upgrades WHERE Name = 'Click'), 1),
((SELECT PlayerID FROM Player WHERE Username = 'player1'),
 (SELECT UpgradeID FROM Upgrades WHERE Name = 'Production'), 1),
((SELECT PlayerID FROM Player WHERE Username = 'player2'),
 (SELECT UpgradeID FROM Upgrades WHERE Name = 'Production'), 1),
((SELECT PlayerID FROM Player WHERE Username = 'player3'),
 (SELECT UpgradeID FROM Upgrades WHERE Name = 'Production'), 1);

INSERT INTO player_skills (PlayerID, SkillID) VALUES
((SELECT PlayerID FROM Player WHERE Username = 'player1'),
 (SELECT SkillID FROM Skill WHERE Name = 'DoubleClick')),
((SELECT PlayerID FROM Player WHERE Username = 'player1'),
 (SELECT SkillID FROM Skill WHERE Name = 'DoubleProduction')),
((SELECT PlayerID FROM Player WHERE Username = 'player2'),
 (SELECT SkillID FROM Skill WHERE Name = 'DoubleClick')),
((SELECT PlayerID FROM Player WHERE Username = 'player2'),
 (SELECT SkillID FROM Skill WHERE Name = 'DoubleProduction')),
((SELECT PlayerID FROM Player WHERE Username = 'player3'),
 (SELECT SkillID FROM Skill WHERE Name = 'DoubleClick')),
((SELECT PlayerID FROM Player WHERE Username = 'player3'),
 (SELECT SkillID FROM Skill WHERE Name = 'DoubleProduction'));

INSERT INTO attack_log (AttackerID, DefenderID) VALUES
((SELECT PlayerID FROM Player WHERE Username = 'player1'),
 (SELECT PlayerID FROM Player WHERE Username = 'player1'));
