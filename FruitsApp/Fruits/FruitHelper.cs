using System.Text.Json;
using System.Xml.Serialization;

namespace Fruits;

public static class FruitHelper
{
    public static List<Fruit> GetByColor(List<Fruit> fruits, string color)
    {
        List<Fruit> result = new List<Fruit>();
        foreach (Fruit f in fruits)
        {
            if (f.Color.ToLower() == color.ToLower())
            {
                result.Add(f);
            }
        }
        return result;
    }

    public static List<Fruit> SortByName(List<Fruit> fruits)
    {
        List<Fruit> result = new List<Fruit>(fruits);
        result.Sort((a, b) => a.Name.CompareTo(b.Name));
        return result;
    }

    public static void WriteToFile(List<Fruit> fruits, string path)
    {
        using (StreamWriter sw = new StreamWriter(path))
        {
            foreach (Fruit f in fruits)
            {
                f.Print(sw);
            }
        }
    }

    public static List<Fruit> ReadFromFile(string path)
    {
        List<Fruit> result = new List<Fruit>();
        using (StreamReader sr = new StreamReader(path))
        {
            string type = sr.ReadLine();
            while (type != null)
            {
                Fruit f;
                if (type == "Citrus")
                {
                    f = new Citrus();
                }
                else if (type == "Fruit")
                {
                    f = new Fruit();
                }
                else
                {
                    throw new FormatException("Unknown type: " + type);
                }
                f.Input(sr);
                result.Add(f);
                type = sr.ReadLine();
            }
        }
        return result;
    }

    public static void SaveXml(List<Fruit> fruits, string path)
    {
        XmlSerializer xml = new XmlSerializer(typeof(List<Fruit>));
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            xml.Serialize(fs, fruits);
        }
    }

    public static List<Fruit> LoadXml(string path)
    {
        XmlSerializer xml = new XmlSerializer(typeof(List<Fruit>));
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            return (List<Fruit>)xml.Deserialize(fs);
        }
    }

    public static void SaveJson(List<Fruit> fruits, string path)
    {
        string json = JsonSerializer.Serialize(fruits);
        File.WriteAllText(path, json);
    }

    public static List<Fruit> LoadJson(string path)
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<Fruit>>(json);
    }
}
