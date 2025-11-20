# Unittestlab

Två delar:

1. En del där du ska skriva tester till PasswordResetService enligt TDD-principer.
2. Analysera föregående labb för att undersöka testbarheten i projektet samt prova att skriva tester för valda delar. **Svara på några korta frågor i slutet av detta dokument!**

## Del 1: Implementera ny funktionalitet i PasswordResetService med TDD

Din uppgift är att med hjälp av TDD-principer implementera minst en av följande funktionaliteter i PasswordResetService:

1. Det ska bara gå att begära en lösenordsåterställning om e-postadressen redan är registrerad i systemet.
2. Det skall bara gå att begära en lösenordsåterställning om den senaste begäran gjordes för mer än en timme sedan.
3. AVANCERAD: Om du gjort övningen PasswordValidator, intregrera användandet av den i PasswordResetService så att nya lösenord måste uppfylla valideringsreglerna. Dvs, en metod för att sätta lösenordet för en viss användare (identifierad utifrån e-postadress) skall implementeras i PasswordResetService. Denna metod skall validera att lösenordet är korrekt enligt PasswordValidator SAMT att det finns en giltig `token` associerad med epostadressen.

> Tips för TDD-arbetet:
> * Börja med att skriva ett test som beskriver den nya funktionaliteten.
> * Samtliga funktionaliteter ovan kräver att nya interfaces skapas för att kunna ersättas med fejkade implementationer i testerna. Skapa dessa interfaces först.
> * Implementera sedan den nya funktionaliteten i PasswordResetService så att testet går igenom.

Lite tips för de olika funktionaliteterna:

1. För att kontrollera om en e-postadress är registrerad, skapa ett nytt interface `IUserRepository` med en metod `bool IsEmailRegistered(string email)`, samt en metod för att lägga till en användare: `void AddUser(string email, string password)`. Använd sedan detta interface i PasswordResetService för att kontrollera om e-postadressen är registrerad.
2. Denna går att göra på lite olika sätt, men ett sätt är att låta PasswordResetService använda ett interface till en klass som hindrar att för många tokens genereras för samma e-postadress inom en viss tid. Skapa ett interface `ITokenRequestLimiter` med en metod `bool CanRequestToken(string email)`. Implementera sedan detta interface i en fejkad klass som håller koll på när senaste token begärdes för varje e-postadress.

## Del 2: Analysera testbarheten i föregående labb

**Fråga 1:**
Identifierade du någon del av koden i föregående labb som direkt gick att testa? Vilken/vilka och varför?

**Fråga 2:**
Identifierade du någon del av koden i föregående labb som var svår eller omöjlig att testa? Vilken/vilka och varför?

**Fråga 3:**
Om du identifierade delar som var svåra eller omöjliga att testa, hur skulle du kunna ändra koden för att göra den mer testbar?

**Fråga 4:**
Ändrade du någon kod i föregående labb för att göra den testbar? I så fall, vad ändrade du och hur påverkade det testbarheten?