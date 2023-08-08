# Logind-velse-sikker_lagering

har lidt problem med logind salt bliver ændret ikke helt sikker på hvorfor, henter det og laver byte[] salt = Encoding.ASCII.GetBytes(fetchedSalt);
så jeg kan bruge det i byte[] form men for 2 forskellige ud.
D7BD4867DF5FE8F71684 før men hvis jeg Console.WriteLine(BitConverter.ToString(salt).Replace("-", "")); får jeg 4437424434383637444635464538463731363834
sikker missed noget men fortræt til og se problemet så kigger imorgen men du må godt kigge koden
