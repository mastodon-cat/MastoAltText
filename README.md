# MastoAltText

Boot per gestionar l'AltText (descripció) dels Toots

### Execució

Dins la carpeta `src/MastoAltText` executar:

```bash
dotnet run --MastodonParms:Instance mastodon.cat --MastodonParms:AccessToken _G_IUGzKminjtvm0DHpr5qPvmIcHvXtxpJORnqxr0tY
```

També es pot crear fitxer `mastodoncredentials.json` amb les credencials.

O podeu optar per fer servir variables d'entorn:

```bash
export MASTOALTTEXT_MastodonParms__Instance="mastodon.cat"
export MASTOALTTEXT_MastodonParms__AccessToken="_G_IUGzKminjtvm0DHpr5qPvmIcHvXtxpJORnqxr0tY"
```
