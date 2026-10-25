using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Fruits;

[XmlInclude(typeof(Citrus))]
[JsonDerivedType(typeof(Fruit), "fruit")]
[JsonDerivedType(typeof(Citrus), "citrus")]
public class Fruit
{
    private string name = "";
    private string color = "";

    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name can not be empty");
            }
            name = value;
        }
    }

    public string Color
    {
        get { return color; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Color can not be empty");
            }
            color = value;
        }
    }

    public Fruit()
    {
    }

    public Fruit(string name, string color)
    {
        Name = name;
        Color = color;
    }

    public virtual void Input()
    {
        Console.Write("Enter name: ");
        Name = Console.ReadLine();
        Console.Write("Enter color: ");
        Color = Console.ReadLine();
    }

    public virtual void Input(StreamReader sr)
    {
        Name = sr.ReadLine();
        Color = sr.ReadLine();
    }

    public virtual void Print()
    {
        Console.WriteLine(ToString());
    }

    public virtual void Print(StreamWriter sw)
    {
        sw.WriteLine(ToString());
    }

    public override string ToString()
    {
        return "Fruit: " + Name + ", color: " + Color;
    }
}
