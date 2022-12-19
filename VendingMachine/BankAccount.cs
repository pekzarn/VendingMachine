namespace VendingMachine;

public class BankAccount
{
    public static int Balance { get; set; }

    private Dictionary<int, int> money = new Dictionary<int, int>();
    
    public BankAccount()
    {
        money.Add(1, 10);
        money.Add(5, 10);
        money.Add(10, 10);
    }
    
    public int GetBalance()
    {
        int sum = 0;
        foreach (var moneyType in money)
        {
            sum += moneyType.Key * moneyType.Value;
        }
        return sum;
    }

    public void Deposit(int type, int amount)
    {
        Balance += amount;
    }

    public bool Withdraw(int moneyType)
    {
        try
        {
            int currentAmount = money[moneyType];
            if (currentAmount >= 1)
            {
                money[moneyType] -= 1;
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            throw new Exception("JAG KOMMER HIT");
        }
        return false;

    }
    
}