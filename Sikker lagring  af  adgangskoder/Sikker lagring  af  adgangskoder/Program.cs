using MySql.Data.MySqlClient;
using Sikker_lagring__af__adgangskoder;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("----------------------------------------------------------------------");
Console.WriteLine("                                Systemet");
Console.WriteLine("----------------------------------------------------------------------");
Console.WriteLine();
Console.WriteLine("            1: Log Ind");
Console.WriteLine("            2: Opret Bruger");
Console.WriteLine();
Console.WriteLine("----------------------------------------------------------------------");

//Valg menu switch
LogInd logInd = new LogInd();

var input = Console.ReadKey();
switch (input.Key)
{
    case ConsoleKey.D1:
    case ConsoleKey.NumPad1:
        LogIndUi();
        break;
    case ConsoleKey.D2:
    case ConsoleKey.NumPad2:
        OpretUI();
        break;
}

//opret menu
static void OpretUI()
{
    string? username, password;
    do
    {
        Console.Clear();
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine("                                Opret.");
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine();
        Console.Write("Indtast Brugernavn : ");
        username = Console.ReadLine();
        Console.WriteLine();
        Console.Write("Indtast Kodeord : ");
        password = Console.ReadLine();
        Console.Clear();
    }
    while (username == "" || password == "");

    OpretBruger opretBruger = new OpretBruger();
    opretBruger.Opretbruger(username, password);
}



static void LogIndUi()
{
    int count = 0;
    do
    {
        Console.Clear();
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine("                                Log Ind.");
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine();
        Console.Write("Input username : ");
        string? username = Console.ReadLine();
        Console.WriteLine();
        Console.Write("Input Password : ");
        string? password = Console.ReadLine();
        Console.Clear();

        LogInd logind = new LogInd();
        bool logindstatus = logind.Logind(username, password);

        if (logindstatus == true)
        {
            count = 5;
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("                                Logged ind i systemet.");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine();
            Thread.Sleep(2000);

        }
        else
        {
            count++;
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("                                brugernavn eller kodeord er forkert.");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine();
            Thread.Sleep(2000);
        }

    } while (count < 5);

    Console.ReadKey();
}



//connection del fra en side ting jeg laved for en månded siden.
//opdatered connectionstring til det nye så det hurtig copy paste

//string connectionString = "server=localhost;user=root;password=Kode1234!;database=logindkryptering;";
//using (MySqlConnection connection = new MySqlConnection(connectionString))
//{
//    connection.Open();
//    // Perform database operations here
//    Console.WriteLine("open");
//    connection.Close();

//}
