# IBAS Support

## Formål

IBAS Support er en webapplikation til håndtering af supporthenvendelser.

Systemet gør det muligt at oprette supporthenvendelser med oplysninger om kunden, kontaktoplysninger, beskrivelse og kategori. Henvendelserne gemmes i Azure Cosmos DB og kan efterfølgende vises i en samlet oversigt.

## Funktioner

- Oprette nye supporthenvendelser
- Validere oplysninger i formularen
- Gemme supporthenvendelser i Azure Cosmos DB
- Vise alle registrerede supporthenvendelser i en oversigt
- Navigere mellem oprettelse og oversigt via navigationen

## Teknologier

Projektet er udviklet med:

- .NET 10
- Blazor Web App
- C#
- Azure Cosmos DB
- Microsoft Azure
- Git og GitHub

## Cosmos DB

Projektet bruger Azure Cosmos DB til at gemme supporthenvendelser.

Databasen består af:

- Cosmos DB account: `ibas-db-account-2076`
- Resource group: `IBasSupportRG`
- Database: `IBasSupportDB`
- Container: `ibassupport`
- Partition key: `/category`

### Oprettelse med Azure CLI

Cosmos DB kan oprettes med følgende Azure CLI-kommandoer:

```bash
az provider register --namespace Microsoft.DocumentDB --wait

export RESGRP="IBasSupportRG"

az group create --location denmarkeast --name $RESGRP

export DBACCOUNT="ibas-db-account-"$RANDOM

export RESGRP="IBasSupportRG"

az cosmosdb create --name $DBACCOUNT --resource-group $RESGRP \
--enable-free-tier true

export DATABASE="IBasSupportDB"

az cosmosdb sql database create --account-name $DBACCOUNT \
--resource-group $RESGRP --name $DATABASE

export CONTAINER="ibassupport"

az cosmosdb sql container create --account-name $DBACCOUNT \
--resource-group $RESGRP --database-name $DATABASE \
--name $CONTAINER --partition-key-path "/category"
```

På Mac anvendes `/category` som partition key-path.

## Konfiguration

For at applikationen kan forbinde til Cosmos DB, skal connection string konfigureres lokalt.

Connection string skal ikke gemmes direkte i GitHub-repositoriet.

Projektet bruger .NET User Secrets til at gemme connection string lokalt:

```bash
dotnet user-secrets init

dotnet user-secrets set "CosmosDb:ConnectionString" "min connectionstring"
```

Connection string til den eksisterende Cosmos DB sendes separat som en note til afleveringen. 

Database- og container-navnene findes i `appsettings.json`.

## Status

Projektet er implementeret som en Blazor Web App, der kan oprette og vise supporthenvendelser.

Supporthenvendelser bliver gemt i Azure Cosmos DB og kan efterfølgende hentes og vises i systemets oversigt.

Formularen indeholder validering af de nødvendige felter, og navigationen mellem systemets sider fungerer.

Projektet er testet lokalt, og funktionaliteten fungerer som forventet.

## Næste trin

Mulige næste trin for løsningen kunne være:

- Mulighed for at redigere eksisterende supporthenvendelser
- Mulighed for at slette supporthenvendelser
- Filtrering efter kategori
- Søgefunktion
- Login og brugerhåndtering
- Status på supporthenvendelser, eksempelvis åben, under behandling og afsluttet