# hada-ProyectoGrupo

Nombre Proyecto : Portal de Torneos de Esports

Miembros : 

Héctor García García (Coordinador) --> 48770607G
Jesús Villena Gómis --> 48775415M
Farouk Naalamene --> Z0289003J
Maksim Grines --> 760563479
Carlos Alguacil Delgado --> 48722539Y

Descripción : 

Hay tres tipos de usuario: visitante, jugador y administrador.

Cuando te registras creas tu cuenta y tu perfil de jugador a la vez (nickname, juego favorito, rol). Una vez dentro puedes crear un equipo o unirte a uno existente, y desde ahí inscribirte en torneos.
El administrador es un usuario especial que se crea directamente en la base de datos. Cuando alguien se registra ese campo se pone a false automáticamente, nadie puede asignarse admin desde la web. Al hacer login el sistema detecta si eres admin y te manda a un panel distinto con opciones de gestión.

Parte pública (sin cuenta)

- Ver torneos activos con imagen, videojuego, fecha y premio
- Buscar torneos por videojuego, fecha o premio
- Ver equipos y jugadores participantes
- Noticias y info general de la plataforma
- Formulario de registro

Listado EN Pública : 

- Torneo
- Videojuego
- Premio
- Equipo 
- Jugador
- Noticia
- Usuario

Parte privada - Jugador (con cuenta)

- Crear o unirse a un equipo
- Inscribir tu equipo en torneos
- Ver tus torneos y resultados
- Editar tu perfil o darte de baja

Listado EN Privada - Jugador : 

- Jugador
- Equipo
- Inscripción
- Torneo
- Partida
- Usuario

Parte privada - Administrador (panel separado)

- Gestionar torneos, videojuegos y premios
- Gestionar patrocinadores y noticias
- Panel de informes (participación, torneos más populares...)

Listado EN Privada - Administrador : 

- Torneo
- VideoJuego
- Premio
- Patrocinador
- Noticia
- Inscripción
- Panel de Informes(todas las entidades para generar estadisticas)

Entidades (2 por alumno, somos 5) : 

- Usuario
- Jugador
- Equipo
- Torneo
- Videojuego
- Inscripción
- Partida
- Premio
- Patrocinador
- Noticia


Posibles Mejoras : 

- Mejora de Monetización como la creación de diferentes suscripciones para acceder a los torneos , sistemas de patrocinios de torneos y pagos con diferentes tipos de moneda.
- Mejora de Funcionalidad : Sistemas de notificaciones para los Jugadores y generación automatica de emparejamientos .
- Mejora Tecnica : Exportación de Informe , logotipos personalizados de equipos y conexion a APIs para verificar perfiles.


######################
Notas del profesor
######################
* No os falta en la parte publica ver los patrocinadores?
* Habeis considerado las un perfil adicional de cuenta de patrocinador para crear torneos y establecer los premios y el coste de apuntarse?
* Para mayor moetizacion y potencial de interes, habeis pensado en añadir lista de videojuegos de interes para votaciones?
* Metricas de monetizacion como por ejemplo: torneos más populares por inscripciones, equipos registrados por mes, patrocinadores con mayor actividad...*

#####################################################
SEGUNDA ENTREGA :  Entrega esquema de la BB.DD

NOMBRE DE FICHERO Y UBICACIÓN

FICHERO : Esquema_EERR.pdf

UBICACIÓN : /Esquema_EERR.pdf

###########################################################

MEMORIA ENTREGA FINAL

#############################################################

CAMBIOS HECHOS : 

-	EN y CAD de Equipo : añadir nuevos atributos :  max_jugadores y cantidad de miembros actuales y métodos para su manejo en su aspx : ReadAllConMiiembros , GetLastId , ReadByCapitan y la clase auxiliar Equipo con miembros
-	EN y CAD de Jugador : añadir nuevos atributos :  int juego y nombreEquipo junto con sus implementaciones y métodos para su manejo en su aspx:   QuitarCapitania y QuitardeEquipo
-	Eliminación de la Entidad “Premio” y conversión a un atributo de la Entidad Torneo
-	Implementación del CAD de Estadisticas junto su aspx y su .cs
-	EN Y CAD de Noticia : añadir nuevos atributos y métodos para hacer likes
-	CAD Patrocinador : añadir métodos auxiliares :  ReadTorneos , ReadFiltrado , ReadPatrocinios.
-	CAD de Torneo : añadido método ReadWithVideoJuego para mostrar nombre de videojuego en DetallesToreno.aspx e implementado atributo de capacidad de equipos .
-	Control de Inscripción para que un Equipo no se puedo Inscribir al mismo Torneo.

DIFICULTADES :

-	Implementación de imágenes y subida de estas a la base de datos
-	Uso de la base de datos para implementar, modificar o eliminar equipos según las restricciones empleadas
-	Guardar datos auxiliares para comprobar si se es un usuario u otro
-	Eliminación de cuenta de usuario al tener que controlar todas las relaciones existentes en la base de datos , como sus equipos , sus Jugadores , si estaban inscritos a un Torneo eliminarlo …
-	Sistema de creación y edición de Patrocinadores para que aparecieran correctamente los Torneos disponibles para patrocinar y que se guardaran bien los datos. 
-	Aplicación de filtros
-	Búsqueda de información por otros medios al no tener la documentación del .NET la información necesaria a las soluciones buscadas por los miembros del proyecto
-	Estilo del CSS
-	Relación usuario y noticia con “LikesNoticia”
-	Manejar Bootstrap
-	Aplicación de variable application y su método : application_Error de global.asax


APRENDIZAJES : 

-	Uso y creación de filtros 
-	CAD con SQL relacional entre 3 tablas
-	Uso básico de Bootstrap
-	Uso básico de CSS
-	Graficas para las Estadísticas
-	Variables y métodos del global.asax
-	Estructuras HTML
	
INSTRUCCIONES

1. Una vez clonado el proyecto se deberá establecer como proecto de inicio "hada-ProyectoGrupo"
2. Respecto la BD , esta se deberá extraer del script de la propia entrega y llamarla HadaEsports
3. Además ofrecemos una serie de datos incluyendo 4 usuarios , usuario1@gmail.com , usuario2@gmail.com ,  usuario3@gmail.com , usuario4@gmail.com y 
 sus contraseñas son el numero de usuario que son . Ademas proporcionamos una cuenta se administrador llamada admin@gmail.com cuya contraseña es admin123


PROBLEMAS CON PERSONAS EN EL GRUPO : NINGUNO

PROBLEMAS DE PLANIFICACIÓN : 

En la tercera y quinta entrega , en los issues se comentó que el trabajo de dicho momento tenia que haber estado antes del dia antes de la entrega oficial
por la noche pero por mejorar y terminar el codigo se terminó el mismo el dia de la entrega por la tarde , cosa que fue una falla de planificación de las personas que
tardarón pero por lo demás bien . 
Se intentó hacer el reparto de tareas lo mas equitativo posible pero al no tener todas el mismo contenido , algunos compañeros aumentaron su carga de trabajo.


Tareas hechas por cada miembro : 

-	Héctor :
	- EN y CAD de Torneo y VideoJuego
	- aspx y aspx.cs de : Jugadores , DetallesJugador , Equipos , DetallesEquipo
	- Desplegable de Acceso e Imagen de Fondo
-	Farouk : 
	- EN y CAD de Patrocinador y Noticia y EN de TorneoPatrocinador
	- aspx y aspx.cs de: GestionPatrocinador, EditarPatrocinador, DetallePatrocinador, EditarPerfil, PerfilUsuario
	- Registro y ayudar con el siteMater
-	Carlos :  
	- EN y CAD de Premio , Partida y modificaciones en VideoJuego.
	- Frontend inicial de Login. Frontend y backend de Videojuegos, Videojuego y Partidas.
	- Añadir el listado de partidas según el torneo.
	- Crud de administrados para dichas secciones. 
-	Maksim : 
	- EN y CAD Equipo e Inscripción 
	- aspx y cs de : Noticias DetallesNoticias ,  CAD de : Estadisticas y LikesNoticias .
	- Modificaciones de default aspx y cs para prepararlo para Estadisiticas.
-	Jesús : 
	- EN y CAD Usuario y Jugador
	- AnadirSaldo.aspx y AnadirSaldo.aspx.cs, GestionTorneo.aspx y GestionTorneo.aspx.cs, DetalleTorneo.aspx y DetalleTorneo.aspx.cs,
	- Torneos.aspx y Torneos.aspx.cs, Error.aspx y Error.aspx.cs y Global.asax.












