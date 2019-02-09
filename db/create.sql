CREATE TABLE qualification(
	id SERIAL PRIMARY KEY,
	name text NOT NULL
);
INSERT INTO qualification (name) VALUES ('russian'), ('italian'), ('japanese');

CREATE TABLE restaurant(
    id serial PRIMARY KEY,
    title TEXT NOT NULL
);

CREATE TABLE cook(
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
	second_name TEXT,
	last_name TEXT NOT NULL,
	-- Values 'e' or 'm' of shift column mean if cook has evening shift or morning shift
    shift VARCHAR(1) CONSTRAINT shift_codes CHECK(shift = 'e' OR shift = 'm'),
    workday SMALLINT NOT NULL CONSTRAINT workday_length CHECK (workday >= 4 AND workday <= 10),
    workdays SMALLINT NOT NULL CONSTRAINT workdays_count CHECK (workdays = 5 OR workdays = 2),
    restaurant_id INTEGER NOT NULL REFERENCES restaurant(id)
);

CREATE TABLE cook_qualifications(
	cook_id INTEGER REFERENCES cook(id) ON DELETE CASCADE,
	qualification_id INTEGER REFERENCES qualification(id),
	PRIMARY KEY(cook_id, qualification_id)
);
