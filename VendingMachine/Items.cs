namespace VendingMachine;


interface IItem //Interface for items
{
    void Buy(); //Method for buying items
    void Use(); //Method for using items
    string Description(); //Method for getting the description of the item
}

public abstract class Item : IItem //Abstract class that inherits from the interface
{
    public string Name { get; set; } //Property for the name of the item
    public string _Description { get; set; } //Property for the description of the item
    public int Price { get; set; } //Property for the price of the item
    
    abstract public void Buy(); //Abstract method for buying items
    abstract public void Use(); //Abstract method for using items
    
    public string Description() //Method for getting the description of the item
    {
        return _Description; //Returns the description of the item
    }
    
    public Item(string name, string description, int price) //Constructor for the item
    {
        Name = name; //Sets the name of the item
        _Description = description; //Sets the description of the item
        Price = price; //Sets the price of the item
    }
}

class Coffee : Item //Class for the coffee item that inherits from the item class
{
    public Coffee(string name, string description, int price) : base(name, description, price) {} //Constructor for the coffee item
    
    public override void Buy() { Console.WriteLine("You bought a " + Name); } //Method for buying the coffee item
    
    public override void Use() { Console.WriteLine("You drank the " + Name); } //Method for using the coffee item
}

class Milk : Item //Class for the milk item that inherits from the item class
{
    public Milk(string name, string description, int price) : base(name, description, price) {} //Constructor for the milk item
    
    public override void Buy() { Console.WriteLine("You bought a " + Name); } //Method for buying the milk item
    
    public override void Use() { Console.WriteLine("You drank the " + Name); } //Method for using the milk item
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
