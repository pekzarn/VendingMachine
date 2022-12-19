namespace VendingMachine;

public class BankAccount
{
    public static int Balance { get; set; } // static property Balance

    private Dictionary<int, int> money = new Dictionary<int, int>(); // dictionary for money
    
    public BankAccount() //Constructor for money
    {
        money.Add(1, 10); // 10 x 1 kr
        money.Add(5, 10); // 10 x 5 kr 
        money.Add(10, 10); // 10 x 10 kr
    }
    
    public int GetBalance() //Method for getting balance
    {
        int sum = 0;
        foreach (var moneyType in money) //Loop for adding all money in dictionary
        {
            sum += moneyType.Key * moneyType.Value; //Adding money
        }
        return sum; //Returning sum
    }

    public void Deposit(int type, int amount) //Method for depositing money
    {
        Balance += amount;
    }

    public bool Withdraw(int moneyType) //Method for withdrawing money 
    {
        try 
        {
            int currentAmount = money[moneyType]; //Getting current amount of money with types of money
            if (currentAmount >= 1) //If the current amount is more than 1
            {
                money[moneyType] -= 1; //Remove 1 from the current amount
                return true;
            }
            return false;
        }
        catch (Exception e) 
        {
            throw new Exception("Invalid money type"); //If the money type is invalid
        }
        return false;

    }
    
}