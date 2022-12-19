namespace VendingMachine;


interface IItem
{
    void Buy();
    void Use();
    string Description();
}

public abstract class Item : IItem
{
    public string Name { get; set; }
    public string _Description { get; set; }
    public int Price { get; set; }
    
    abstract public void Buy();
    abstract public void Use();
    
    public string Description()
    {
        return _Description;
    }
    
    public Item(string name, string description, int price)
    {
        Name = name;
        _Description = description;
        Price = price;
    }
}

class LiquidItem : Item
{
    public LiquidItem(string name, string description, int price) : base(name, description, price) {}
    
    public override void Buy() { Console.WriteLine("You bought a " + Name); }
    
    public override void Use() { Console.WriteLine("You drank the " + Name); }
}

class Coffee : Item
{
    public Coffee(string name, string description, int price) : base(name, description, price) {}
    
    public override void Buy() { Console.WriteLine("You bought a " + Name); }
    
    public override void Use() { Console.WriteLine("You drank the " + Name); }
}

class Milk : Item
{
    public Milk(string name, string description, int price) : base(name, description, price) {}
    
    public override void Buy() { Console.WriteLine("You bought a " + Name); }
    
    public override void Use() { Console.WriteLine("You drank the " + Name); }
}

class Pepsi : Item
{
    public Pepsi(string name, string description, int price) : base(name, description, price) {}
    
    public override void Buy() { Console.WriteLine("You bought a " + Name); }
    
    public override void Use() { Console.WriteLine("You drank the " + Name); }
}

class Burger : Item
{
    public Burger(string name, string description, int price) : base(name, description, price) {}
    
    public override void Buy() { Console.WriteLine("You bought a " + Name); }
    
    public override void Use() { Console.WriteLine("You ate the " + Name); }
}

class Pizza : Item
{
    public Pizza(string name, string description, int price) : base(name, description, price) {}
    
    public override void Buy() { Console.WriteLine("You bought a " + Name); }
    
    public override void Use() { Console.WriteLine("You ate the " + Name); }
}

class Sandwich : Item
{
    public Sandwich(string name, string description, int price) : base(name, description, price) {}
    
    public override void Buy() { Console.WriteLine("You bought a " + Name); }
    
    public override void Use() { Console.WriteLine("You ate the " + Name); }
}

class RainCoat : Item
{
    public RainCoat(string name, string description, int price) : base(name, description, price) {}
    
    public override void Buy() { Console.WriteLine("You bought a " + Name); }
    
    public override void Use() { Console.WriteLine("You equipped your " + Name); }
}

class Umbrella : Item
{
    public Umbrella(string name, string description, int price) : base(name, description, price) {}
    
    public override void Buy() { Console.WriteLine("You bought a " + Name); }
    
    public override void Use() { Console.WriteLine("You unfolded the " + Name); }
}

class RainShoes : Item
{
    public RainShoes(string name, string description, int price) : base(name, description, price) {}
    
    public override void Buy() { Console.WriteLine("You bought a " + Name); }
    
    public override void Use() { Console.WriteLine("You put on the " + Name); }
}
