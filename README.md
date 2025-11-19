Din uppgift är att med hjälp av TDD-principer implementera minst en av följande funktionaliteter i PasswordResetService:

1. Det ska bara gå att begära en lösenordsåterställning om e-postadressen redan är registrerad i systemet.
2. Det skall bara gå att begära en lösenordsåterställning om den senaste begäran gjordes för mer än en timme sedan.
3. AVANCERAD: Om du gjort övningen PasswordValidator, intregrera användandet av den i PasswordResetService så att nya lösenord måste uppfylla valideringsreglerna. Dvs, en metod för att sätta lösenordet för en viss användare (identifierad utifrån e-postadress) skall implementeras i PasswordResetService. Denna metod skall validera att lösenordet är korrekt enligt PasswordValidator SAMT att det finns en giltig `token` associerad med epostadressen.

> Tips för TDD-arbetet:
> * Börja med att skriva ett test som beskriver den nya funktionaliteten.
> * Samtliga funktionaliteter ovan kräver att nya interfaces skapas för att kunna ersättas med fejkade implementationer i testerna. Skapa dessa interfaces först.
> * Implementera sedan den nya funktionaliteten i PasswordResetService så att testet går igenom.

Lite tips för de olika funktionaliteterna:

1. För att kontrollera om en e-postadress är registrerad, skapa ett nytt interface `IUserRepository` med en metod `bool IsEmailRegistered(string email)`, samt en metod för att lägga till en användare: `void AddUser(string email, string password)`. Använd sedan detta interface i PasswordResetService för att kontrollera om e-postadressen är registrerad. Gör sedan en fejkad implementation av detta i dina tester. Kan vara supersimpel! Spara bara epostadresserna i en lista eller nått.
2. Denna går att göra på lite olika sätt, men ett sätt är att låta PasswordResetService använda ett interface till en klass som hindrar att för många tokens genereras för samma e-postadress inom en viss tid. Skapa ett interface `ITokenRequestLimiter` med en metod `bool CanRequestToken(string email)`. Implementera sedan detta interface i en fejkad klass som håller koll på när senaste token begärdes för varje e-postadress. Denna fejkklass kan ha extra hjälpmetoder för att sätta tiden en timme frammåt för att simulera att tiden går.
