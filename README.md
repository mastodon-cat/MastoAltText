# MastoAltText

Boot per gestionar l'AltText (descripció) dels Toots

### Execució

Dins la carpeta `src/MastoAltText` executar:

```bash
dotnet run --Instance mastodon.cat --AccessToken _G_IUGzKminjtvm0DHpr5qPvmIcHvXtxpJORnqxr0tY
```

També es pot crear fitxer `mastodoncredentials.json` amb les credencials.

O podeu optar per fer servir variables d'entorn:

```bash
export MASTOALTTEXT_INSTANCE="mastodon.cat"
export MASTOALTTEXT_ACCESSTOKEN="_G_IUGzKminjtvm0DHpr5qPvmIcHvXtxpJORnqxr0tY"
```
