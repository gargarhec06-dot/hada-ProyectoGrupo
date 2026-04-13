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




