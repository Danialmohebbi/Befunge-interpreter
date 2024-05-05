using DataStructures;
using utilities;

Utilities utilities = new();
Field field = new();

//main loop
try
{
    string fileName = utilities.processArguments(args);
    field.processFile(fileName); 
    field.eval(); 

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    return;
}



