*-------------------------*
* INTEGRANTES DEL GRUPO   *
*-------------------------*
	Pedro Fern�ndez Luces
	Carla Salgado L�pez

*----------------------------*
** DESCRIPCI�N DEL SOFTWARE **
*----------------------------*

El sitio Web permite visualizar en cada momento el conjunto de eventos deportivos sobre los que se pueden realizar consultas y comentarios. 

De cada evento deportivo interesa almacenar su nombre (eg. Deportivo � Real Madrid), la fecha en la que tendr� lugar (d�a, mes, a�o, hora y minuto), la categor�a a la que pertenece (eg. F�tbol, Baloncesto, Motor, etc.) y una peque�a rese�a del mismo. En aras de la simplificaci�n se asumir� un sistema de categor�as plano, es decir, sin subcategor�as. 

La b�squeda de eventos, y la consulta de informaci�n asociada a los mismos, estar�n accesibles para todos los usuarios sin necesidad de registro previo. Si un usuario desea realizar comentarios acerca de alg�n evento deportivo, deber� registrarse en el sitio Web especificando cierta informaci�n de registro. 

La aplicaci�n Web que se propone desarrollar consiste en un sitio que permite a m�ltiples usuarios intercambiar informaci�n acerca de eventos deportivos, que podr�a ser relevante de cara a las posibles apuestas sobre dichos eventos. Este sitio Web permitir� a los usuarios comentar aquellos eventos deportivos que todav�a no hayan comenzado. S�lo los usuarios autenticados podr�n a�adir comentarios a los eventos deportivos. 

De cara a facilitar el intercambio de informaci�n de inter�s, la aplicaci�n permitir� la creaci�n de grupos de usuarios con intereses comunes. Cualquier usuario autenticado podr� crear un nuevo grupo. Un usuario podr� formar parte de ninguno, uno o varios grupos y podr� recomendar eventos a cualquiera de los grupos a los que pertenece. 

Un usuario se registra en la aplicaci�n de comentarios sobre eventos deportivos especificando cierta informaci�n de registro, a saber, su seud�nimo (login), contrase�a, nombre, apellidos y direcci�n de correo electr�nico. Opcionalmente podr� requerirle informaci�n relativa a su idioma y pa�s para gestionar la internacionalizaci�n en base a las preferencias de usuario y no en base a la configuraci�n del navegador. 

A continuaci�n, se detalla la funcionalidad que deber� ofrecer la aplicaci�n al usuario: 
1. Registro de usuarios. Debe permitir registrar nuevos usuarios, as� como permitir actualizar la informaci�n de registro de los usuarios ya registrados. 
2. Autenticaci�n y salida. Un usuario se autenticar� indicando su seud�nimo y contrase�a, con la posibilidad de recordar la contrase�a para no tener que introducirla la siguiente vez. El usuario podr� salir expl�citamente de la aplicaci�n, lo que provoca que ya no se recuerde su contrase�a, en caso de haber seleccionado dicha opci�n con anterioridad. 
3. Gesti�n de grupos de usuarios. Un usuario autenticado podr� crear nuevos grupos. Un grupo consta de un nombre y una descripci�n. La aplicaci�n dispondr� de un enlace �Ver grupos� que mostrar� todos los grupos existentes en la aplicaci�n, proporcionando para cada uno de ellos el n�mero de miembros del grupo, el n�mero de recomendaciones dirigidas al grupo y un enlace para que el usuario pueda darse de alta en el grupo. Si el usuario ya pertenece al grupo, no se mostrar� el enlace �Darse de alta�. Cualquier usuario podr� ver los grupos existentes, pero �nicamente los usuarios autenticados podr�n darse de alta en un grupo. La aplicaci�n dispondr� tambi�n de un enlace �Mis grupos� que mostrar� los grupos a los que pertenece un usuario, proporcionando un enlace �Darse de baja� por cada grupo, que permitir� eliminar la suscripci�n del usuario a ese grupo. 
4. B�squeda de eventos. El usuario podr� buscar eventos deportivos por palabras clave del nombre del evento, especificando opcionalmente la categor�a mediante un desplegable. Las palabras clave especificadas en la b�squeda tienen que estar todas contenidas en el nombre del evento como palabras o parte de palabras, sin distinguir entre may�sculas y min�sculas, y en cualquier orden. Opcionalmente, el usuario podr� especificar una categor�a, en cuyo caso, la b�squeda se restringir� a los eventos de dicha categor�a. El resultado de la b�squeda mostrar� para cada evento: nombre, categor�a, fecha, un enlace para a�adir un nuevo comentario y un enlace para ver los comentarios existentes. Por �ltimo se incluir� un enlace para recomendar el evento a un grupo. 
5. A�adir comentario. Un usuario puede a�adir comentarios relativos a un evento. Si el usuario no estaba autenticado, cuando selecciona el enlace para a�adir un comentario, se le redirige al formulario de autenticaci�n, y tras autenticarse correctamente, se le muestra el formulario para a�adir comentario. Un usuario tambi�n podr� modificar o eliminar los comentarios realizados por �l mismo. 
6. Ver comentarios de un evento. A trav�s del enlace para ver los comentarios de un evento se mostrar�n, en una nueva p�gina, los comentarios ordenados por la fecha de realizaci�n, apareciendo el m�s reciente primero (orden cronol�gico descendente). Por cada comentario se mostrar� el seud�nimo del usuario que lo realiz�, la fecha en la que se insert� y el texto del comentario. Esta opci�n aparecer� disponible �nicamente cuando un evento tenga comentarios asociados. 
7. Recomendar un evento. En la l�nea de las aplicaciones de car�cter social un usuario podr� recomendar eventos a los miembros de un grupo. Para ello, se mostrar�n los grupos a los que pertenece el usuario y se le permitir� seleccionar aquellos a los que desea recomendar el evento. Cada recomendaci�n puede llevar un texto asociado, justificando la recomendaci�n. 
8. Mostrar recomendaciones. La aplicaci�n dispondr� de un enlace �Mostrar recomendaciones� que permitir� a cada usuario consultar las recomendaciones de eventos recibidas, en funci�n de los grupos a los que pertenezca el usuario. El nombre del evento ser� un enlace para visualizar los comentarios asociados al mismo. Las recomendaciones se mostrar�n ordenadas, de m�s reciente a m�s antigua. 

Algunas opciones (como a�adir un comentario; recomendar evento) requieren que el usuario est� autenticado. Sin embargo, estas opciones deben aparecer disponibles al usuario no autenticado, con el objetivo de que conozca su existencia. La aplicaci�n debe proporcionar una navegaci�n f�cil, permitiendo acceder de forma sencilla a las opciones disponibles y proporcionando feedback al usuario sobre si una operaci�n ha sido realizada con �xito o no. 
No se implementar� una aplicaci�n de administraci�n. En una situaci�n real, la aplicaci�n deber�a actualizar el conjunto de eventos o las categor�as permitidas. Dado que no se implementar� la aplicaci�n de administraci�n, la informaci�n sobre eventos y categor�as se podr� introducir mediante sentencias INSERT INTO, que se incluir�n como parte del script SQL de creaci�n de tablas e inserci�n de datos. 

*---------*
** BADGE **
*---------*

https://travis-ci.org/carlasalgado/VVS/builds/167146496