CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE Player (
    PlayerID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    Currency INT NOT NULL DEFAULT 0,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    Username VARCHAR(255) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Upgrade (
    UpgradeID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    Name VARCHAR(255) NOT NULL,
    base_cost INT NOT NULL,
    cost_mult INT NOT NULL,
    description TEXT NOT NULL
);

CREATE TABLE Skill (
    SkillID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    Name VARCHAR(255) NOT NULL,
    base_cost INT NOT NULL,
    cost_mult INT NOT NULL,
    duration REAL NOT NULL,
    cooldown REAL NOT NULL,
    description TEXT NOT NULL
);

CREATE TABLE Weapon (
    WeaponID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    Name VARCHAR(255) NOT NULL,
    cost INT NOT NULL,
    description TEXT NOT NULL
);

CREATE TABLE PlayerUpgrades (
    PUpgradesID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    PlayerID UUID NOT NULL REFERENCES Player(PlayerID) ON DELETE CASCADE,
    UpgradeID UUID NOT NULL REFERENCES Upgrade(UpgradeID) ON DELETE CASCADE,
    level INT NOT NULL DEFAULT 1
);

CREATE TABLE PlayerSkills (
    PSkillsID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    PlayerID UUID NOT NULL REFERENCES Player(PlayerID) ON DELETE CASCADE,
    SkillID UUID NOT NULL REFERENCES Skill(SkillID) ON DELETE CASCADE,
    level INT NOT NULL DEFAULT 1
);

CREATE TABLE PlayerWeapons (
    PWeaponsID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    PlayerID UUID NOT NULL REFERENCES Player(PlayerID) ON DELETE CASCADE,
    WeaponID UUID NOT NULL REFERENCES Weapon(WeaponID) ON DELETE CASCADE,
    level INT NOT NULL DEFAULT 1
);

CREATE TABLE attack_log(
    LogID UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    AttackerID UUID NOT NULL REFERENCES Player(PlayerID) ON DELETE CASCADE,
    DefenderID UUID NOT NULL REFERENCES Player(PlayerID) ON DELETE CASCADE,
    attacked TIMESTAMP NOT NULL DEFAULT NOW()
)

INSERT INTO Upgrade (Name, base_cost, cost_mult, description) VALUES
('Upgrade 1', 100, 2, 'Description for Upgrade 1');

INSERT INTO Skill (Name, base_cost, cost_mult, duration, cooldown, description) VALUES
('Skill 1', 50, 1.5, 10.0, 5.0, 'Description for Skill 1');

INSERT INTO Weapon (Name, cost, description) VALUES
('Weapon 1', 200, 'Description for Weapon 1');

INSERT INTO Player (Username, Password, Email) VALUES
('player1', 'password1', 'player1@example.com');

INSERT INTO PlayerUpgrades (PlayerID, UpgradeID, level) VALUES
((SELECT PlayerID FROM Player WHERE Username = 'player1'),
 (SELECT UpgradeID FROM Upgrade WHERE Name = 'Upgrade 1'), 1);

INSERT INTO PlayerSkills (PlayerID, SkillID, level) VALUES
((SELECT PlayerID FROM Player WHERE Username = 'player1'),
 (SELECT SkillID FROM Skill WHERE Name = 'Skill 1'), 1);

INSERT INTO PlayerWeapons (PlayerID, WeaponID, level) VALUES
((SELECT PlayerID FROM Player WHERE Username = 'player1'),
 (SELECT WeaponID FROM Weapon WHERE Name = 'Weapon 1'), 1);

INSERT INTO attack_log (AttackerID, DefenderID) VALUES
((SELECT PlayerID FROM Player WHERE Username = 'player1'),
 (SELECT PlayerID FROM Player WHERE Username = 'player1'));

 

