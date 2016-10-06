-- ----------------------------------------------------------------------------
-- Put here INSERT statements for inserting data required by the application
-- in the "pojo" database.
-------------------------------------------------------------------------------

INSERT INTO CATEGORIA VALUES (1, "Tenis");
INSERT INTO CATEGORIA VALUES (2, "Baloncesto");
INSERT INTO CATEGORIA VALUES (3, "Futbol");

INSERT INTO EVENTO VALUES (1, "Madrid-Valencia", '20180101090000', 3);
INSERT INTO EVENTO VALUES (2, "Nadal-Federer", '20180101090000', 1);
INSERT INTO EVENTO VALUES (3, "Lakers-Raptors", '20180101090000', 2);

INSERT INTO EVENTO VALUES (4, "Madrid-Deportivo", '20170101090000', 3);
INSERT INTO EVENTO VALUES (5, "Nadal-Murray", '20170101090000', 1);
INSERT INTO EVENTO VALUES (6, "Lakers-Chicago Bulls", '20170101090000', 2);

INSERT INTO EVENTO VALUES (7, "Madrid-Bayern", '20180501090000', 3);
INSERT INTO EVENTO VALUES (8, "Nadal-Pouille", '20180801090000', 1);
INSERT INTO EVENTO VALUES (9, "Lakers-Kings", '20180601090000', 2);

INSERT INTO EVENTO VALUES (10, "Madrid-Arsenal", '20180307090000', 3);
INSERT INTO EVENTO VALUES (11, "Nadal-Nishkori", '20181201090000', 1);
INSERT INTO EVENTO VALUES (12, "Lakers-Suns", '20180101090000', 2);

INSERT INTO EVENTO VALUES (13, "Arsenal-Bayern", '20150101090000', 3);
INSERT INTO EVENTO VALUES (14, "Real Sociedad-Deportivo", '20151118213000', 3);


INSERT INTO TIPOAPUESTA VALUES (1, "Quien marca gol", 13, true);
INSERT INTO OPCION VALUES (4, "Jugador 1", 1, null, 1);
INSERT INTO OPCION VALUES (5, "Jugador 2", 1.25, null, 1);
INSERT INTO OPCION VALUES (6, "Jugador 3", 1.5, null, 1);

INSERT INTO TIPOAPUESTA VALUES (4, "Quien gana", 14, false);

INSERT INTO OPCION VALUES (9, "Real Sociedad", 1, null, 4);
INSERT INTO OPCION VALUES (10, "Deportivo", 1.25, null, 4);

INSERT INTO TIPOAPUESTA VALUES (3, "Quien gana", 2, false);

INSERT INTO OPCION VALUES (7, "Nadal", 2, null, 3);
INSERT INTO OPCION VALUES (8, "Federer", 3, null, 3);

INSERT INTO APUESTA VALUES (6, '20141212124500', 25, 6, 4);
INSERT INTO APUESTA VALUES (7, '20141212125000', 25, 6, 5);
---usuarios por la aplicacion

