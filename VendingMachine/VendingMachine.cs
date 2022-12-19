using System.Threading.Channels;

namespace VendingMachine;

public class VendingMachine
{
    private int _moneyInMachine = 0; //Variable for the amount of money in the machine
    
    BankAccount ba = new BankAccount(); //Variable for the users bankaccount
    
    public void DisplayMenu(Inventory inventory) //Method for the main menu
    {
        
        WelcomeMessage();
        
        while (true)
        {
            Console.WriteLine("[1] Display products");
            Console.WriteLine("[2] Insert money");
            Console.WriteLine("[I] Inventory");
            Console.WriteLine("[Q] Quit");
            Console.WriteLine();
            Console.WriteLine($"Money in Machine: {_moneyInMachine.ToString("C")}"); //Displays the amount of money in the machine
            Console.WriteLine($"Your account balance is {ba.GetBalance().ToString("C")}."); //Displays the users bankaccount balance
            Console.WriteLine();
            Console.Write("Select an option: ");
            string? input = Console.ReadLine(); //Reads the users input

            switch (input?.ToUpper())
            {
                case "1":
                    ShowOptions(inventory); //Takes the user to the next menu
                    break;
                case "2":
                    HandleDeposit(ba); //Takes the user to the deposit menu
                    break;
                case "I":
                    HandleInventory(inventory); //Takes the user to the inventory menu
                    break;
                case "Q":
                    Console.Clear();
                    HandleExit(ba); //Returns change and exits the program
                    return;
                default:
                    Console.WriteLine("Your input was not valid, Please try again"); //If the user inputs something else than the options
                    break;
            }
            
            Console.Clear();
        }
    }
    
    private static void WelcomeMessage() //Method for the welcome message
    {
        Console.WriteLine("Welcome to the Vending Machine");
    } 
    
    private List<Item> Items = new List<Item>(); //List for the items
    public VendingMachine()
    {
        Items.Add(new Coffee("Coffee", "A brewed drink prepared from roasted coffee beans", 5));
        Items.Add(new Milk("Milk", "A nutrient-rich, white liquid produced from the cow", 10));
        Items.Add(new Pepsi("Pepsi", "A carbonated soft drink manufactured by PepsiCo", 15));
        Items.Add(new Burger("Burger", "A cooked beef patti, placed inside a sliced bun", 20));
        Items.Add(new Pizza("Pizza", "A flattened base of leavened wheat-based dough topped with tomatoes, cheese and ham", 15));
        Items.Add(new Sandwich("Sandwich", "Sliced cheese and meat, placed between two slices of bread", 10));
        Items.Add(new Umbrella("Umbrella", "A folding canopy supported by wooden or metal ribs", 15));
        Items.Add(new RainCoat("Raincoat", "A waterproof garment worn to keep the body dry in wet weather", 25));
        Items.Add(new RainShoes("Rain shoes", "Shoes that are designed to be worn in wet conditions", 20));
    } //Method that contains the items
    
    public void ShowOptions(Inventory inventory) //Second menu with display options
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose a type of product to show an extended list of options");
            Console.WriteLine();
            Console.WriteLine("[1] Liquid Products");
            Console.WriteLine("[2] Meal Products");
            Console.WriteLine("[3] Rough Weather Products");
            Console.WriteLine();
            Console.WriteLine("[Q] to go back to main menu");
        
            Console.WriteLine("Select an option: ");
            string input = Console.ReadLine();

            switch (input?.ToUpper())
            {
                case "1":
                    Console.Clear();
                    ShowLiquidProducts(inventory); //Takes the user to the liquid products menu
                    break;
                case "2":
                    Console.Clear();
                    ShowMealProducts(inventory); //Takes the user to the meal products menu
                    break;
                case "3":
                    Console.Clear();
                    ShowRoughWeatherProducts(inventory); //Takes the user to the rough weather products menu
                    break;
                case "Q":
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Your input was not valid, Please try again"); //If the user inputs something else than the options
                    break;
            }

            Console.ReadLine();
            Console.Clear();
        }
    } 
    
    public bool AskToBuy(int index, Inventory inventory) //Method that asks the user if they want to buy the selected product
    {
        Console.WriteLine("Do you want to buy this item? [Y/N]");
        var buyChoise = Console.ReadLine();
        
        if (buyChoise is "Y" or "y") //If the user says Y = yes, it will use the Buy method
        {
            Console.Clear();
            Buy(index, inventory);
            return true;
        }
        
        return false;
    }

    public bool Buy(int index, Inventory inventory) //Buy method that completes the purchase
    {
        
        Item item = Items[index]; //Variable for the item that the user wants to buy
        if (item.Price <= _moneyInMachine) //If you got enough money in the machine you are able to buy
        {
            item.Buy(); //Uses the Buy method from the item class
            inventory.items.Add(item); //Adds the item to the inventory
            _moneyInMachine -= item.Price; //Removes the price of the item from the money in the machine
            return true; 
        }
        
        Console.WriteLine("Please insert more money in the machine to buy this item");
        return false; 
    }

    public void HandleDeposit(BankAccount account) //Method for the deposit menu 
    {
        List<int> allowedTypes = new List<int> {1, 5, 10};
        Console.Clear();
        Console.WriteLine($"Money in Machine: {_moneyInMachine.ToString("C")}");
        Console.WriteLine("Enter the amount of money you want to deposit");
        Console.WriteLine("1 | 5 | 10 | [Q] to go back to main menu");
        string moneyInput = Console.ReadLine();
        if (moneyInput.ToUpper() == "Q")
        {
            return;
        }

        int type;
        bool success = Int32.TryParse(moneyInput, out type); 
        if (!success || !allowedTypes.Contains(type)) //If the user inputs something else than the options
        {
            Console.Clear();
            HandleDeposit(account); //Takes the user back to the deposit menu
            return;
        }

        bool withdraw = account.Withdraw(type); //Withdraws the amount of money from the users bankaccount
        if (!withdraw)
        {
            Console.Clear();
            HandleDeposit(account); //Takes the user back to the deposit menu
            return;
        }

        _moneyInMachine += type; //Adds the amount of money to the money in the machine
        HandleDeposit(account); //Takes the user back to the deposit menu

    }

    public void ReturnChange(BankAccount account) //Method that returns the change to the user
    {
        List<int> allowedTypes = new List<int> {100, 50, 20, 10, 5, 1}; //List of the allowed types of money
        int moneyCopy = _moneyInMachine; //Copies the variable for the money in the machine
        foreach (var type in allowedTypes) 
        {
            int found = _moneyInMachine / type; //Finds the amount of money of the type
            if (found > 0) 
            {
                Console.WriteLine(found + "of " + type + "kr"); //Displays the amount of money of the type
                account.Deposit(type, _moneyInMachine / type); //Deposits the amount of money of the type to the users bankaccount
                _moneyInMachine = _moneyInMachine - type; //Removes the amount of money of the type from the money in the machine
            }
        }

        Console.WriteLine("Total: " + moneyCopy + "kr!"); //Displays the total amount of money that the user got back
    }

    public void HandleExit(BankAccount account) //Method that handles the exit menu
    {
        Console.Clear();
        Console.WriteLine("Your change: "); //Displays the change
        ReturnChange(account); //Uses the ReturnChange method
        Console.ReadLine();
        Environment.Exit(0); //Exits the program
    }

    public void HandleInventory(Inventory inventory) //Method that handles the inventory menu
    {
        Console.Clear();
        Console.WriteLine("Here you can see all your current items");
        Console.WriteLine("To use one, select it and press enter");
        Console.WriteLine("To go back to the main menu, press [Q]");
        Console.WriteLine();
        int i = 0;
        foreach (Item item in inventory.items) //Displays all the items in the inventory
        {
            Console.WriteLine($"[{i}] {item.Name} {item._Description}"); //Displays the name and description of the item
            i++; //Increases the index by 1
        }
        string key = Console.ReadLine(); //Reads the users input
        if (key == "Q")
        {
            return; //Takes the user back to the main menu
        }

        int index;
        bool success = int.TryParse(key, out index); 
        if (!success) 
        {
            Console.Clear();
            return;
        }

        Console.Clear();
        inventory.items[index].Use(); //Uses the Use method from the item class
        inventory.items.RemoveAt(index); //Removes the item from the inventory
        Console.ReadLine();
        HandleInventory(inventory); //Takes the user back to the inventory menu
    }
    
    public class Inventory //class for the inventory
    {
        public List<Item> items = new List<Item>(); //list for the items in the inventory

        public Inventory() { } //Constructor for the inventory
    }
    
    public void ShowLiquidProducts(Inventory inventory) //Method to show liquid products
    {
        Console.WriteLine("Liquid Products");
        Console.WriteLine($"[1] {Items[0].Name}: {Items[0].Price}kr"); //Displays the name and price of the item
        Console.WriteLine($"[2] {Items[1].Name}: {Items[1].Price}kr");
        Console.WriteLine($"[3] {Items[2].Name}: {Items[2].Price}kr");
        Console.WriteLine("Choose item to show the description: ");
        var valAvVara = Console.ReadLine();
        switch (valAvVara?.ToUpper())
        {
            case "1":
                Console.WriteLine($"{Items[0]._Description}"); //Displays the description of the item
                AskToBuy(0, inventory); //Uses the AskToBuy method
                break; //Breaks the switch
            case "2":
                Console.WriteLine($"{Items[1]._Description}");
                AskToBuy(1, inventory);
                break;
            case "3":
                Console.WriteLine($"{Items[2]._Description}");
                AskToBuy(2, inventory);
                break;
            case "Q":
                Console.Clear();
                return;
            default:
                Console.WriteLine("Your input was not valid, Please try again");
                break;
        }
        
    }

    public void ShowMealProducts(Inventory inventory) //Method to show meal products
    {
        Console.WriteLine("Meal Products");
        Console.WriteLine($"[1] {Items[3].Name}: {Items[3].Price}kr");
        Console.WriteLine($"[2] {Items[4].Name}: {Items[4].Price}kr");
        Console.WriteLine($"[3] {Items[5].Name}: {Items[5].Price}kr");
        Console.WriteLine("Choose item to show the description: ");
        var valAvVara = Console.ReadLine();
        switch (valAvVara?.ToUpper())
        {
            case "1":
                Console.WriteLine($"{Items[3]._Description}");
                AskToBuy(3, inventory);
                break;
            case "2":
                Console.WriteLine($"{Items[4]._Description}");
                AskToBuy(4, inventory);
                break;
            case "3":
                Console.WriteLine($"{Items[5]._Description}");
                AskToBuy(5, inventory);
                break;
            case "Q":
                Console.Clear();
                return;
            default:
                Console.WriteLine("Your input was not valid, Please try again");
                break;
        }
        
    }

    public void ShowRoughWeatherProducts(Inventory inventory) //Method to show rough weather products
    {
        Console.WriteLine("Rough Weather Products");
        Console.WriteLine($"[1] {Items[6].Name}: {Items[6].Price}kr");
        Console.WriteLine($"[2] {Items[7].Name}: {Items[7].Price}kr");
        Console.WriteLine($"[3] {Items[8].Name}: {Items[8].Price}kr");
        Console.WriteLine("Choose item to show the description: ");
        var valAvVara = Console.ReadLine();
        switch (valAvVara?.ToUpper())
        {
            case "1":
                Console.WriteLine($"{Items[6]._Description}");
                AskToBuy(6, inventory);
                break;
            case "2":
                Console.WriteLine($"{Items[7]._Description}");
                AskToBuy(7, inventory);
                break;
            case "3":
                Console.WriteLine($"{Items[8]._Description}");
                AskToBuy(8,inventory);
                break;
            case "Q":
                Console.Clear();
                return;
            default:
                Console.WriteLine("Your input was not valid, Please try again");
                break;
        }
    } 

}