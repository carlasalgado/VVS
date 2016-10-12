*-------------------------*
* INTEGRANTES DEL GRUPO   *
*-------------------------*
	Pedro Fernández Luces
	Carla Salgado López

*----------------------------*
** DESCRIPCIÓN DEL SOFTWARE **
*----------------------------*

El sitio Web permite visualizar en cada momento el conjunto de eventos deportivos sobre los que se pueden realizar consultas y comentarios. 

De cada evento deportivo interesa almacenar su nombre (eg. Deportivo – Real Madrid), la fecha en la que tendrá lugar (día, mes, año, hora y minuto), la categoría a la que pertenece (eg. Fútbol, Baloncesto, Motor, etc.) y una pequeña reseña del mismo. En aras de la simplificación se asumirá un sistema de categorías plano, es decir, sin subcategorías. 

La búsqueda de eventos, y la consulta de información asociada a los mismos, estarán accesibles para todos los usuarios sin necesidad de registro previo. Si un usuario desea realizar comentarios acerca de algún evento deportivo, deberá registrarse en el sitio Web especificando cierta información de registro. 

La aplicación Web que se propone desarrollar consiste en un sitio que permite a múltiples usuarios intercambiar información acerca de eventos deportivos, que podría ser relevante de cara a las posibles apuestas sobre dichos eventos. Este sitio Web permitirá a los usuarios comentar aquellos eventos deportivos que todavía no hayan comenzado. Sólo los usuarios autenticados podrán añadir comentarios a los eventos deportivos. 

De cara a facilitar el intercambio de información de interés, la aplicación permitirá la creación de grupos de usuarios con intereses comunes. Cualquier usuario autenticado podrá crear un nuevo grupo. Un usuario podrá formar parte de ninguno, uno o varios grupos y podrá recomendar eventos a cualquiera de los grupos a los que pertenece. 

Un usuario se registra en la aplicación de comentarios sobre eventos deportivos especificando cierta información de registro, a saber, su seudónimo (login), contraseña, nombre, apellidos y dirección de correo electrónico. Opcionalmente podrá requerirle información relativa a su idioma y país para gestionar la internacionalización en base a las preferencias de usuario y no en base a la configuración del navegador. 

A continuación, se detalla la funcionalidad que deberá ofrecer la aplicación al usuario: 
1. Registro de usuarios. Debe permitir registrar nuevos usuarios, así como permitir actualizar la información de registro de los usuarios ya registrados. 
2. Autenticación y salida. Un usuario se autenticará indicando su seudónimo y contraseña, con la posibilidad de recordar la contraseña para no tener que introducirla la siguiente vez. El usuario podrá salir explícitamente de la aplicación, lo que provoca que ya no se recuerde su contraseña, en caso de haber seleccionado dicha opción con anterioridad. 
3. Gestión de grupos de usuarios. Un usuario autenticado podrá crear nuevos grupos. Un grupo consta de un nombre y una descripción. La aplicación dispondrá de un enlace “Ver grupos” que mostrará todos los grupos existentes en la aplicación, proporcionando para cada uno de ellos el número de miembros del grupo, el número de recomendaciones dirigidas al grupo y un enlace para que el usuario pueda darse de alta en el grupo. Si el usuario ya pertenece al grupo, no se mostrará el enlace “Darse de alta”. Cualquier usuario podrá ver los grupos existentes, pero únicamente los usuarios autenticados podrán darse de alta en un grupo. La aplicación dispondrá también de un enlace “Mis grupos” que mostrará los grupos a los que pertenece un usuario, proporcionando un enlace “Darse de baja” por cada grupo, que permitirá eliminar la suscripción del usuario a ese grupo. 
4. Búsqueda de eventos. El usuario podrá buscar eventos deportivos por palabras clave del nombre del evento, especificando opcionalmente la categoría mediante un desplegable. Las palabras clave especificadas en la búsqueda tienen que estar todas contenidas en el nombre del evento como palabras o parte de palabras, sin distinguir entre mayúsculas y minúsculas, y en cualquier orden. Opcionalmente, el usuario podrá especificar una categoría, en cuyo caso, la búsqueda se restringirá a los eventos de dicha categoría. El resultado de la búsqueda mostrará para cada evento: nombre, categoría, fecha, un enlace para añadir un nuevo comentario y un enlace para ver los comentarios existentes. Por último se incluirá un enlace para recomendar el evento a un grupo. 
5. Añadir comentario. Un usuario puede añadir comentarios relativos a un evento. Si el usuario no estaba autenticado, cuando selecciona el enlace para añadir un comentario, se le redirige al formulario de autenticación, y tras autenticarse correctamente, se le muestra el formulario para añadir comentario. Un usuario también podrá modificar o eliminar los comentarios realizados por él mismo. 
6. Ver comentarios de un evento. A través del enlace para ver los comentarios de un evento se mostrarán, en una nueva página, los comentarios ordenados por la fecha de realización, apareciendo el más reciente primero (orden cronológico descendente). Por cada comentario se mostrará el seudónimo del usuario que lo realizó, la fecha en la que se insertó y el texto del comentario. Esta opción aparecerá disponible únicamente cuando un evento tenga comentarios asociados. 
7. Recomendar un evento. En la línea de las aplicaciones de carácter social un usuario podrá recomendar eventos a los miembros de un grupo. Para ello, se mostrarán los grupos a los que pertenece el usuario y se le permitirá seleccionar aquellos a los que desea recomendar el evento. Cada recomendación puede llevar un texto asociado, justificando la recomendación. 
8. Mostrar recomendaciones. La aplicación dispondrá de un enlace “Mostrar recomendaciones” que permitirá a cada usuario consultar las recomendaciones de eventos recibidas, en función de los grupos a los que pertenezca el usuario. El nombre del evento será un enlace para visualizar los comentarios asociados al mismo. Las recomendaciones se mostrarán ordenadas, de más reciente a más antigua. 

Algunas opciones (como añadir un comentario; recomendar evento) requieren que el usuario esté autenticado. Sin embargo, estas opciones deben aparecer disponibles al usuario no autenticado, con el objetivo de que conozca su existencia. La aplicación debe proporcionar una navegación fácil, permitiendo acceder de forma sencilla a las opciones disponibles y proporcionando feedback al usuario sobre si una operación ha sido realizada con éxito o no. 
No se implementará una aplicación de administración. En una situación real, la aplicación debería actualizar el conjunto de eventos o las categorías permitidas. Dado que no se implementará la aplicación de administración, la información sobre eventos y categorías se podrá introducir mediante sentencias INSERT INTO, que se incluirán como parte del script SQL de creación de tablas e inserción de datos. 

*---------*
** BADGE **
*---------*

https://travis-ci.org/carlasalgado/VVS/builds/167146496