using System.Globalization;
using Fruits;

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

List<Fruit> fruits = new List<Fruit>();
fruits.Add(new Fruit("Banana", "Yellow"));
fruits.Add(new Fruit("Apple", "Red"));
fruits.Add(new Fruit("Pear", "Yellow"));
fruits.Add(new Fruit("Kiwi", "Green"));
fruits.Add(new Fruit("Plum", "Purple"));
fruits.Add(new Citrus("Lemon", "Yellow", 0.053));
fruits.Add(new Citrus("Orange", "Orange", 0.045));
fruits.Add(new Citrus("Grapefruit", "Pink", 0.031));

try
{
    List<Fruit> fromFile = FruitHelper.ReadFromFile("fruits_input.txt");
    fruits.AddRange(fromFile);
}
catch (FileNotFoundException)
{
    Console.WriteLine("File fruits_input.txt not found");
}
catch (Exception ex)
{
    Console.WriteLine("Error reading file: " + ex.Message);
}

Console.Write("Add fruit from console? (y/n): ");
string answer = Console.ReadLine();
if (answer == "y")
{
    Console.Write("Is it citrus? (y/n): ");
    string isCitrus = Console.ReadLine();
    Fruit f;
    if (isCitrus == "y")
    {
        f = new Citrus();
    }
    else
    {
        f = new Fruit();
    }

    try
    {
        f.Input();
        fruits.Add(f);
    }
    catch (FormatException)
    {
        Console.WriteLine("Wrong number format");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine("Wrong data: " + ex.Message);
    }
}

Console.WriteLine();
Console.WriteLine("All fruits:");
foreach (Fruit f in fruits)
{
    f.Print();
}

Console.WriteLine();
Console.WriteLine("Yellow fruits:");
List<Fruit> yellow = FruitHelper.GetByColor(fruits, "yellow");
foreach (Fruit f in yellow)
{
    f.Print();
}

List<Fruit> sorted = FruitHelper.SortByName(fruits);

try
{
    FruitHelper.WriteToFile(sorted, "fruits_sorted.txt");
    Console.WriteLine();
    Console.WriteLine("Sorted list saved to fruits_sorted.txt");
}
catch (Exception ex)
{
    Console.WriteLine("Error writing file: " + ex.Message);
}

try
{
    FruitHelper.SaveXml(sorted, "fruits.xml");
    List<Fruit> fromXml = FruitHelper.LoadXml("fruits.xml");
    Console.WriteLine();
    Console.WriteLine("From XML:");
    foreach (Fruit f in fromXml)
    {
        f.Print();
    }
}
catch (Exception ex)
{
    Console.WriteLine("XML error: " + ex.Message);
}

try
{
    FruitHelper.SaveJson(sorted, "fruits.json");
    List<Fruit> fromJson = FruitHelper.LoadJson("fruits.json");
    Console.WriteLine();
    Console.WriteLine("From JSON:");
    foreach (Fruit f in fromJson)
    {
        f.Print();
    }
}
catch (Exception ex)
{
    Console.WriteLine("JSON error: " + ex.Message);
}
