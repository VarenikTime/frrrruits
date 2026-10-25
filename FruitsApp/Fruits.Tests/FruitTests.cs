using Fruits;

namespace Fruits.Tests;

[TestClass]
public class FruitTests
{
    [TestMethod]
    public void Constructor_SetsNameAndColor()
    {
        Fruit f = new Fruit("Apple", "Red");

        Assert.AreEqual("Apple", f.Name);
        Assert.AreEqual("Red", f.Color);
    }

    [TestMethod]
    public void EmptyName_Throws()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new Fruit("", "Red"));
    }

    [TestMethod]
    public void ToString_Fruit()
    {
        Fruit f = new Fruit("Apple", "Red");

        Assert.AreEqual("Fruit: Apple, color: Red", f.ToString());
    }

    [TestMethod]
    public void Input_FromFile()
    {
        string path = Path.GetTempFileName();
        File.WriteAllText(path, "Banana\nYellow\n");

        Fruit f = new Fruit();
        using (StreamReader sr = new StreamReader(path))
        {
            f.Input(sr);
        }
        File.Delete(path);

        Assert.AreEqual("Banana", f.Name);
        Assert.AreEqual("Yellow", f.Color);
    }
}

[TestClass]
public class CitrusTests
{
    [TestMethod]
    public void Constructor_SetsVitaminC()
    {
        Citrus c = new Citrus("Lemon", "Yellow", 0.053);

        Assert.AreEqual("Lemon", c.Name);
        Assert.AreEqual(0.053, c.VitaminC);
    }

    [TestMethod]
    public void NegativeVitaminC_Throws()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new Citrus("Lemon", "Yellow", -1));
    }

    [TestMethod]
    public void ToString_Citrus()
    {
        Citrus c = new Citrus("Orange", "Orange", 2);

        Assert.AreEqual("Citrus: Orange, color: Orange, vitamin C: 2 g", c.ToString());
    }

    [TestMethod]
    public void Input_FromFile()
    {
        string path = Path.GetTempFileName();
        File.WriteAllText(path, "Lime\nGreen\n0.029\n");

        Citrus c = new Citrus();
        using (StreamReader sr = new StreamReader(path))
        {
            c.Input(sr);
        }
        File.Delete(path);

        Assert.AreEqual("Lime", c.Name);
        Assert.AreEqual(0.029, c.VitaminC);
    }
}

[TestClass]
public class FruitHelperTests
{
    private List<Fruit> MakeList()
    {
        List<Fruit> fruits = new List<Fruit>();
        fruits.Add(new Fruit("Banana", "Yellow"));
        fruits.Add(new Fruit("Apple", "Red"));
        fruits.Add(new Citrus("Lemon", "Yellow", 0.053));
        fruits.Add(new Citrus("Orange", "Orange", 0.045));
        return fruits;
    }

    [TestMethod]
    public void GetByColor_Yellow()
    {
        List<Fruit> yellow = FruitHelper.GetByColor(MakeList(), "yellow");

        Assert.HasCount(2, yellow);
        Assert.AreEqual("Banana", yellow[0].Name);
        Assert.AreEqual("Lemon", yellow[1].Name);
    }

    [TestMethod]
    public void SortByName()
    {
        List<Fruit> sorted = FruitHelper.SortByName(MakeList());

        Assert.AreEqual("Apple", sorted[0].Name);
        Assert.AreEqual("Banana", sorted[1].Name);
        Assert.AreEqual("Lemon", sorted[2].Name);
        Assert.AreEqual("Orange", sorted[3].Name);
    }

    [TestMethod]
    public void ReadFromFile_UnknownType_Throws()
    {
        string path = Path.GetTempFileName();
        File.WriteAllText(path, "Vegetable\nCarrot\nOrange\n");

        Assert.ThrowsExactly<FormatException>(() => FruitHelper.ReadFromFile(path));
        File.Delete(path);
    }

    [TestMethod]
    public void Xml_SaveAndLoad()
    {
        string path = Path.GetTempFileName();
        FruitHelper.SaveXml(MakeList(), path);
        List<Fruit> loaded = FruitHelper.LoadXml(path);
        File.Delete(path);

        Assert.HasCount(4, loaded);
        Assert.AreEqual("Banana", loaded[0].Name);
        Assert.IsInstanceOfType(loaded[2], typeof(Citrus));
    }

    [TestMethod]
    public void Json_SaveAndLoad()
    {
        string path = Path.GetTempFileName();
        FruitHelper.SaveJson(MakeList(), path);
        List<Fruit> loaded = FruitHelper.LoadJson(path);
        File.Delete(path);

        Assert.HasCount(4, loaded);
        Assert.AreEqual("Banana", loaded[0].Name);
        Assert.IsInstanceOfType(loaded[2], typeof(Citrus));
    }
}
