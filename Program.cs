using DataStructures;
using static Utilities;

Field field = new();

//main loop
try
{
    string fileName = Utilities.processArguments(args);
    field.processFile(fileName); 
    field.eval(); 

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    return;
}



