# MastoAltText

MatoAltText és una aplicació .Net disenyada per incentivar els usuaris de Mastodon, d'una determinada instància, a incloure descripcions als seus continguts multimedia. El que anomanem, [text alternatiu]([url](https://ca.wikipedia.org/wiki/Viquip%C3%A8dia:Text_alternatiu)). 

Aquesta aplicació envia recordatoris a aquells usuaris que publiquen contingut multimèdia sense descripció, premia als que sí que ho fan per incentivar-los i publica missatges públics en Mastodon per reconèixer a aquells usuaris que sistemàticament ho fan.

Aquest projecte requereix permisos per llegir els missatges locals, aquests permisos els pot atorgar un administrador de la instància.

## Requisits

2. La instal·lació està preparada per funcionar amb Docker. Caldria tenir Docker a la màquina on es farà la instal·lació.

## Instalació

1. **Crear un compte Mastodon** amb privilegis d'administrador. Aquest compte serà qui enviï missatges a la instància. Recorda marcar aquest compte com a "Bot" per tal que els usuaris tinguin constància que el compte no està operat per una persona.
2. **Crea una aplicació a la teva instància**: Es necessita un compte amb permisos d'administrador per crear una nova aplicació a Mastodon. Aquest compte serà qui enviï els toots. Aquesta aplicació cal que tingui permisos administratius suficients com per llegir missatges de la instància, enviar missatges directes (DM) a altres usuaris i enviar missatges àmbit "públic".
3. **Modifica l'arxiu appsettings.Production.json**: Aquest fitxer el trobareu a l'arrel del projecte `src/MastoAltText/appsettings.json`. Cal canviar el nom de la instància i el token d'accés dins el node "MastodonParms" de manera que s'adiguin amb l'aplicació creada i la URL de la instància Mastodon.
4. **Personalitza els missatges al node "AppMessages"**: Aquí pots afegir, editar o eliminar els missatges segons les teves preferències. El format de condicions és intuïtiu i es basa en el total de toots amb multimèdia enviats i quins tenen descripció. També hi ha un camp per indicar quants toots consecutius amb descripció han estat enviats. A continuació podeu veure un exemple d'un fragment del fitxer tal com el tenim configurat al nostre node:

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

Com es veu a l'exemple, els camps que es poden usar són:
- "TootsWithDescription": El nombre de toots multimedia que aquest usuari ha enviat amb descripció.
- "TotalToots": El nombre total de toots amb contingut multimedia que l'usuari ha publicat.
- "LastConsecutivesWithDescription": El nombre de toots consecutivos que l'usuari ha enviat i que contenien multimèdia amb descripció.

Amb aquests tres camps i el sistema de condicions podem definir els missatges que vulguem enviar. Això serveix tant per a missatges a l'usuari com per missatges públics. El placeholder `{name}` serà substituït, en temps d'execució, pel nom del compte de l'usuari.

>Les condicions s'avaluen sempre com un `and`. Actualment l'operador `or` per aniuar condicions no està suportat.


## Execució

Engega l'aplicació mitjançant docer-compose, executa la següent comanda:

```bash
docker compose up
```

Això engega tots els serveis necessaris, incloent la base de dades Postgress i el sistema d'escolta.

>Pots afegir aquesta comanda al sistema d'arracanda del teu sistema operatiu per tal que s'inicii automàticament a l'inici i puguis manegar-lo com un servei
>[Aquí tens un exemple de com afegir la comanda docker-compose en systemctl amb Ubuntu](https://gist.github.com/mosquito/b23e1c1e5723a7fd9e6568e5cf91180f).


## Contribucions

Desitgem que aquesta aplicació et sembli d'utilitat. 

Pots col·laborar fent difusió d'aquesta aplicació, ajudant a instal·lar-la en nodes Mastodon o mitjançant PR. Tota col·laboració és benvinguda.

Si instal·les aquesta aplicació al teu node no dubtis a obrir una issue explicant la teva experiència.
