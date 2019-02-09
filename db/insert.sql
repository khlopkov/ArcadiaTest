INSERT INTO restaurant(title) VALUES ('restaurant1');
INSERT INTO restaurant(title) VALUES ('restaurant2');
INSERT INTO restaurant(title) VALUES ('restaurant3');

INSERT INTO cook(name, last_name, shift, workday, workdays, restaurant_id) 
	VALUES ('cook0', 'cook', 'e', 10, 5, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook1', 'cook', 'e', 7, 5, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook2', 'cook', 'e', 7, 5, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook3', 'cook', 'e', 7, 2, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook4', 'cook', 'e', 7, 2, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook5', 'cook', 'e', 7, 2, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook6', 'cook', 'm', 7, 5, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook7', 'cook', 'm', 7, 5, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook8', 'cook', 'm', 7, 5, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook9', 'cook', 'm', 7, 2, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook10', 'cook', 'm', 7, 2, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook11', 'cook', 'm', 7, 2, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook12', 'cook', 'm', 7, 5, (SELECT id FROM restaurant where title = 'restaurant1')),
	('cook13', 'cook', 'e', 7, 5, (SELECT id FROM restaurant where title = 'restaurant1'));

INSERT INTO cook_qualifications 
	VALUES ((SELECT id FROM cook WHERE name='cook0'), 1),
	((SELECT id FROM cook WHERE name='cook1'), 2),
	((SELECT id FROM cook WHERE name='cook2'), 3),
	((SELECT id FROM cook WHERE name='cook3'), 1),
	((SELECT id FROM cook WHERE name='cook4'), 2),
	((SELECT id FROM cook WHERE name='cook5'), 3),
	((SELECT id FROM cook WHERE name='cook6'), 1),
	((SELECT id FROM cook WHERE name='cook7'), 2),
	((SELECT id FROM cook WHERE name='cook8'), 3),
	((SELECT id FROM cook WHERE name='cook9'), 1),
	((SELECT id FROM cook WHERE name='cook10'), 2),
	((SELECT id FROM cook WHERE name='cook11'), 3),
	((SELECT id FROM cook WHERE name='cook12'), 1),
	((SELECT id FROM cook WHERE name='cook12'), 2),
	((SELECT id FROM cook WHERE name='cook12'), 3),
	((SELECT id FROM cook WHERE name='cook13'), 1),
	((SELECT id FROM cook WHERE name='cook13'), 2),
	((SELECT id FROM cook WHERE name='cook13'), 3);
