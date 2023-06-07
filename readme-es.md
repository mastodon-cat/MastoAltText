# MastoAltText

MastoAltText es una aplicación .Net diseñada para incentivar a los usuarios de Mastodon de una determinada instancia a incluir descripciones en su contenido multimedia. Esta aplicación envía recordatorios a aquellos que publican contenido multimedia sin descripciones, premia a los que sí lo hacen para incentivarlos, y publica mensajes públicos en Mastodon para reconocer a aquellos que lo hacen.

Este proyecto requiere que un administrador de Mastodon lo instale, ya que necesita que una aplicación tenga permisos en la cuenta que mande mensajes para leer los mensajes locales que llegan a la instancia.

## Prerequisitos

1. Tener Docker instalado en la máquina donde se instalará la aplicación.

## Instalación

1. Crea una cuenta de Mastodon con privilegios de administrador. Esta cuenta será la que envíe mensajes en la instancia. Márcala como "Bot" para que quede claro que la cuenta no está operada directamente por una persona.
2. **Crea una aplicación en tu instancia**: Se necesita una cuenta con permisos de administrador para crear una nueva aplicación en Mastodon. Esta cuenta será la que envíe los toots. Debe otorgarse a esta aplicación suficientes permisos administrativos para leer los mensajes de la instancia, enviar mensajes directos a otros usuarios y enviar mensajes como "público".
3. **Modifica el archivo appsettings.Production.json**: Este archivo se encuentra en la raíz del proyecto. Deberás cambiar el nombre de la instancia y el token de acceso dentro del nodo "MastodonParms" para que coincidan con la aplicación creada y la URL de tu instancia.
4. **Personaliza los mensajes en el nodo "AppMessages"**: Aquí puedes agregar, editar o eliminar los mensajes que prefieras. El formato de condiciones es bastante intuitivo y se basa en el total de toots con multimedia enviados, y cuáles de ellos tienen descripción multimedia. También hay un campo para indicar cuántos toots consecutivos se han enviado con descripción. A continuación se muestra un ejemplo de un fragmento de este nodo con dos mensajes:

```json
  "appMessages": [
    {
      "Conditions": [
        {
          "Field": "TotalToots",
          "Operator": "==",
          "Value": "1"
        },
        {
          "Field": "TootsWithDescription",
          "Operator": "==",
          "Value": "0"
        }
      ],
      "Message": "You've posted a multimedia Toot without a description. This node wants to be inclusive. Remember to put a description on your multimedia Toots so everyone can enjoy them. (This is a private message, only you can see it)",
      "MessageType": "Warn"
    },
  {
      "Conditions": [
        {
          "Field": "LastConsecutivesWithDescription",
          "Operator": "==",
          "Value": "5"
        }
      ],
      "Message": "Great! 5 consecutive Toots with description to multimedia content!! This makes this Mastodon node much more inclusive, keep it up!",
      "MessageType": "Reward",
      "PublicMessage": "{name} has sent five toots with description to multimedia content. Thanks to people like {name} this node is more inclusive! Long live {name}! 🎆🎆"
    }
  ]
```

Como ves, los campos que se pueden usar son:
  
- "TootsWithDescription": El número de toos multimedia que este usuario ha mandado con descripción.
- "TotalToots": El número total de toots con contenido multimedia que el usuario ha mandado.
- "LastConsecutivesWithDescription": El número de toots consecutivos que el usuario ha mandado con multimedia y descripción".

Con estos tres campos y el sistema de condiciones, podemos definir los mensajes que queremos enviar, tanto al usuario como de forma pública. El placeholder {name}, se modificará en tiempo de ejecución por el nombre de la cuenta de la que estamos hablando.

Si te fijas, las condiciones que están dentro del array se evalúan como "and". Actualmente no hay forma de anidar condiciones o usar operadores "or".

## Ejecución

Arranca la aplicación usando docker-compose: Ejecuta el siguiente comando:

```bash
docker-compose up
```

Esto arrancará todos los servicios necesarios, incluyendo la base de datos de Postgres y el sistema de escucha. 

>Como sugerencia, puedes agregar este comando de arranque al sistema de arranque de tu sistema operativo para que se inicie automáticamente al inicio, y puedas manejarlo como un servicio. [Aquí tienes un ejemplo de cómo conseguir añadir tu comando docker-compose en systemctl usando Ubuntu](https://gist.github.com/mosquito/b23e1c1e5723a7fd9e6568e5cf91180f).

## Contribuciones

¡Esperamos que la aplicación te parezca útil!

Cualquier comentario o contribución será más que bienvenida. 

Si lo instalas en tu nodo, y te apetece, abre una issue explicando tu experiencia.

