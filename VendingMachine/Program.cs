using System.Xml.Schema;
using VendingMachine;

static class Program
{
    static void Main(string[] args)
    {
        //imports the vending machine class and runs the program
        VendingMachine.VendingMachine vm = new VendingMachine.VendingMachine();
        VendingMachine.VendingMachine.Inventory inv = new VendingMachine.VendingMachine.Inventory();
        vm.DisplayMenu(inv); //Runs the program and displays the menu

    }
}