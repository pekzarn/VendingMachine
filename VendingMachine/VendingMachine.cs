using System.Threading.Channels;

namespace VendingMachine;

public class VendingMachine
{
    private int _moneyInMachine = 0;

    BankAccount ba = new BankAccount();
    
    public void DisplayMenu(Inventory inventory)
    {
        
        WelcomeMessage();
        
        while (true)
        {
            Console.WriteLine("[1] Display products");
            Console.WriteLine("[2] Insert money");
            Console.WriteLine("[I] Inventory");
            Console.WriteLine("[Q] Quit");
            Console.WriteLine();
            Console.WriteLine($"Money in Machine: {_moneyInMachine.ToString("C")}");
            Console.WriteLine($"Your account balance is {ba.GetBalance().ToString("C")}.");
            Console.WriteLine();
            Console.Write("Select an option: ");
            string? input = Console.ReadLine();

            switch (input?.ToUpper())
            {
                case "1":
                    ShowOptions(inventory);
                    break;
                case "2":
                    HandleDeposit(ba);
                    break;
                case "I":
                    HandleInventory(inventory);
                    break;
                case "Q":
                    Console.Clear();
                    HandleExit(ba);
                    return;
                default:
                    Console.WriteLine("Your input was not valid, Please try again");
                    break;
            }
            
            Console.Clear();
        }
    }
    private static void WelcomeMessage()
    {
        Console.WriteLine("Welcome to the Vending Machine");
    }
    
    private List<Item> Items = new List<Item>();
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
    }

    //Method to show options
    public void ShowOptions(Inventory inventory)
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
                    ShowLiquidProducts(inventory);
                    break;
                case "2":
                    Console.Clear();
                    ShowMealProducts(inventory);
                    break;
                case "3":
                    Console.Clear();
                    ShowRoughWeatherProducts(inventory);
                    break;
                case "Q":
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Your input was not valid, Please try again");
                    break;
            }

            Console.ReadLine();
            Console.Clear();
        }
    }
    
    public bool AskToBuy(int index, Inventory inventory)
    {
        Console.WriteLine("Do you want to buy this item? [Y/N]");
        var buyChoise = Console.ReadLine();
        
        if (buyChoise is "Y" or "y")
        {
            Console.Clear();
            Buy(index, inventory);
            return true;
        }
        
        return false;
    }

    public bool Buy(int index, Inventory inventory)
    {
        
        Item item = Items[index];
        if (item.Price <= _moneyInMachine)
        {
            item.Buy();
            inventory.items.Add(item);
            _moneyInMachine -= item.Price;
            return true;
        }
        
        Console.WriteLine("Please insert more money in the machine to buy this item");
        return false;
    }

    public void HandleDeposit(BankAccount account)
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
        if (!success || !allowedTypes.Contains(type))
        {
            Console.Clear();
            HandleDeposit(account);
            return;
        }

        bool withdraw = account.Withdraw(type);
        if (!withdraw)
        {
            Console.Clear();
            HandleDeposit(account);
            return;
        }

        _moneyInMachine += type;
        HandleDeposit(account);

    }

    public void ReturnChange(BankAccount account)
    {
        List<int> allowedTypes = new List<int> {100, 50, 20, 10, 5, 1};
        int moneyCopy = _moneyInMachine;
        foreach (var type in allowedTypes)
        {
            int found = _moneyInMachine / type;
            if (found > 0)
            {
                Console.WriteLine(found + "of " + type + "kr");
                account.Deposit(type, _moneyInMachine / type);
                _moneyInMachine = _moneyInMachine - type;
            }
        }

        Console.WriteLine("Total: " + moneyCopy + "kr!");
    }

    public void HandleExit(BankAccount account)
    {
        Console.Clear();
        Console.WriteLine("Your change: ");
        ReturnChange(account);
        Console.ReadLine();
        Environment.Exit(0);
    }

    public void HandleInventory(Inventory inventory)
    {
        Console.Clear();
        Console.WriteLine("Here you can see all your current items");
        Console.WriteLine("To use one, select it and press enter");
        Console.WriteLine("To go back to the main menu, press [Q]");
        Console.WriteLine();
        int i = 0;
        foreach (Item item in inventory.items)
        {
            Console.WriteLine($"[{i}] {item.Name} {item._Description}");
            i++;
        }
        string key = Console.ReadLine();
        if (key == "Q")
        {
            return;
        }

        int index;
        bool success = int.TryParse(key, out index);
        if (!success)
        {
            Console.Clear();
            return;
        }

        Console.Clear();
        inventory.items[index].Use();
        inventory.items.RemoveAt(index);
        Console.ReadLine();
        HandleInventory(inventory);
    }


    public class Inventory
    {
        public List<Item> items = new List<Item>();

        public Inventory() { }
    }

    //Method to show liquid products
    public void ShowLiquidProducts(Inventory inventory)
    {
        Console.WriteLine("Liquid Products");
        Console.WriteLine($"[1] {Items[0].Name}: {Items[0].Price}kr");
        Console.WriteLine($"[2] {Items[1].Name}: {Items[1].Price}kr");
        Console.WriteLine($"[3] {Items[2].Name}: {Items[2].Price}kr");
        Console.WriteLine("Choose item to show the description: ");
        var valAvVara = Console.ReadLine();
        switch (valAvVara?.ToUpper())
        {
            case "1":
                Console.WriteLine($"{Items[0]._Description}");
                AskToBuy(0, inventory);
                break;
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

    //Method to show meal products
    public void ShowMealProducts(Inventory inventory)
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

    //Method to show rough weather products
    public void ShowRoughWeatherProducts(Inventory inventory)
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