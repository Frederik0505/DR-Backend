# DR Music Records REST API

Projektet er en direkte tilpasning af `CarsRESTproviderV3-master` til DR-casen.

## Hvad er ændret?
- `CarsRESTproviderV3` -> `MusicRecords` (projektnavn/namespace).
- `Car` -> `MusicRecord` og `CarsController` -> `MusicRecordsController`.
- `Model`, `Vendor`, `Price` -> `Title`, `Artist`, `Duration` samt `PublicationYear`.
- `Id` er bevaret til at kunne hente, rette og slette et enkelt objekt.
- De tre eksempler er musikplader i stedet for biler.
- Den tomme `Put` i originalen er udfyldt, så den opdaterer en musikplade.
- `WeatherForecast`-eksemplet er fjernet, da det ikke vedrører DR-casen.
- Swagger/OpenAPI og `AllowAll`-CORS er bevaret fra originalen.

## Start i Visual Studio
1. Åbn `MusicRecords.slnx` i Visual Studio 2026.
2. Brug .NET 10 SDK og lad NuGet hente pakkerne.
3. Start via HTTP-profilen. Swagger åbner på `http://localhost:5102/` i udviklingsmiljøet.
4. Prøv `http://localhost:5102/api/MusicRecords` og `/api/MusicRecords/1`.
5. Test POST, PUT og DELETE via Swagger eller `MusicRecords.http`.

## Model
```csharp
public int Id { get; set; }
public string Title { get; set; } = string.Empty;
public string Artist { get; set; } = string.Empty;
public int Duration { get; set; }  // sekunder
public int PublicationYear { get; set; }
```

## REST-metoder
| Metode | Endpoint | Funktion |
|---|---|---|
| GET | `/api/MusicRecords` | Hent alle musikplader |
| GET | `/api/MusicRecords/1` | Hent en musikplade |
| POST | `/api/MusicRecords` | Tilføj musikplade, API genererer Id |
| PUT | `/api/MusicRecords/1` | Opdater musikplade |
| DELETE | `/api/MusicRecords/1` | Slet musikplade |

Eksempel på JSON til POST:
```json
{
  "title": "The Dark Side of the Moon",
  "artist": "Pink Floyd",
  "duration": 2580,
  "publicationYear": 1973
}
```

## Bemærk
- `MusicRecords` er en **statisk liste i controlleren**, nøjagtig som `Cars` var i originalen. Ingen database; data nulstilles når programmet genstartes.
- Controllerens metoder og returværdier følger originalens enkle undervisningseksempel. `Get(id)` giver `null` hvis objektet ikke findes; `Put` gør intet, hvis Id ikke findes; `Delete` returnerer antallet af slettede elementer.
- `AllowAll` tillader browserkald fra alle origins. Det er praktisk til øvelse og lokal udvikling, men bør begrænses før produktion.
- De oprindelige NuGet-pakker og deres versioner er bevaret. Løsningen er ikke build-testet her, da .NET SDK ikke er installeret i miljøet.
