using System.Globalization;

namespace Fruits;

public class Citrus : Fruit
{
    private double vitaminC;

    public double VitaminC
    {
        get { return vitaminC; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Vitamin C can not be negative");
            }
            vitaminC = value;
        }
    }

    public Citrus()
    {
    }

    public Citrus(string name, string color, double vitaminC) : base(name, color)
    {
        VitaminC = vitaminC;
    }

    public override void Input()
    {
        base.Input();
        Console.Write("Enter vitamin C (g): ");
        VitaminC = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
    }

    public override void Input(StreamReader sr)
    {
        base.Input(sr);
        VitaminC = double.Parse(sr.ReadLine(), CultureInfo.InvariantCulture);
    }

    public override void Print()
    {
        Console.WriteLine(ToString());
    }

    public override void Print(StreamWriter sw)
    {
        sw.WriteLine(ToString());
    }

    public override string ToString()
    {
        return "Citrus: " + Name + ", color: " + Color + ", vitamin C: " + VitaminC + " g";
    }
}
