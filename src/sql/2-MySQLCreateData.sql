-- ----------------------------------------------------------------------------
-- Put here INSERT statements for inserting data required by the application
-- in the "pojo" database.
-------------------------------------------------------------------------------

-- Categorias -- 

INSERT INTO CATEGORIA VALUES (1, "Tenis");
INSERT INTO CATEGORIA VALUES (2, "Baloncesto");
INSERT INTO CATEGORIA VALUES (3, "Futbol");

-- Eventos que aun no comenzaron --

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

-----------------------------------
------- Evento que finalizo -------
-----------------------------------
INSERT INTO EVENTO VALUES (13, "Arsenal-Bayern", '20150101090000', 3);
-- No se determino el ganador: Multiples opciones ganadoras --
INSERT INTO TIPOAPUESTA VALUES (1, "Quien marca gol", 13, true);
INSERT INTO OPCION VALUES (4, "Jugador 1", 1, null, 1);
INSERT INTO OPCION VALUES (5, "Jugador 2", 1.25, null, 1);
INSERT INTO OPCION VALUES (6, "Jugador 3", 1.5, null, 1);
-- No se determino el ganador: Unica opcion ganadora --
INSERT INTO TIPOAPUESTA VALUES (2, "Quien gana", 13, false);
INSERT INTO OPCION VALUES (7, "Arsenal", 1, null, 2);
INSERT INTO OPCION VALUES (8, "Bayern", 1.25, null, 2);
-----------------------------------
------- Evento que finalizo -------
-----------------------------------
INSERT INTO EVENTO VALUES (14, "Real Sociedad-Deportivo", '20151118213000', 3);
-- Se determino el ganador: Unica opcion ganadora --
INSERT INTO TIPOAPUESTA VALUES (4, "Quien gana", 14, false);
INSERT INTO OPCION VALUES (9, "Real Sociedad", 1, true, 4);
INSERT INTO OPCION VALUES (10, "Deportivo", 1.25, false, 4);
-- Se determino el ganador: Multiples opciones ganadoras --
INSERT INTO TIPOAPUESTA VALUES (3, "Quien marca gol", 14, true);
INSERT INTO OPCION VALUES (11, "Jugador 1", 1, true, 3);
INSERT INTO OPCION VALUES (12, "Jugador 2", 1.25, true, 3);
INSERT INTO OPCION VALUES (13, "Jugador 3", 1.5, false, 3);
-------------------------------------------------
-- Apuestas realizadas por el usuario 6 'Pepe' --
-------------------------------------------------
-- Eventos finalizados --
INSERT INTO APUESTA VALUES (1, '20141212124500', 25, 6, 9);
INSERT INTO APUESTA VALUES (2, '20140312124500', 10, 6, 13);
-- Eventos no comenzados --
INSERT INTO APUESTA VALUES (3, '20141212124500', 11, 6, 4);
INSERT INTO APUESTA VALUES (4, '20140312124500', 3, 6, 7);
-- Se insertan tipos de apuesta y opcion para evento 1 --
INSERT INTO TIPOAPUESTA VALUES (5, "Quien gana", 1, false);
INSERT INTO OPCION VALUES (14, "Madrid", 1, null, 5);
INSERT INTO OPCION VALUES (15, "Valencia", 1.25, null, 5);
---usuarios por la aplicacion

