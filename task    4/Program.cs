namespace task____4_
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public static double operator +(Account a1, Account a2)
        {
            return a1.Balance + a2.Balance;
        }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount < 0)
                return false;

            Balance += amount;
            return true;
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class SavingsAccount : Account
    {
        public double interestrate { get; set; }

        public SavingsAccount(string name = "Unnamed Account", double balance = 0.0, double interestrate = 0) : base(name, balance)
        {
            this.interestrate = interestrate;
        }

        public void addInterest(double interestrate)
        {
            double interest = this.Balance * ((interestrate/100)*Balance);
            Balance += interest;
        }
    }

    public class CheckingAccount : Account
    {
        private const double fee = 1.5;

        public CheckingAccount(string name = "Unnamed Account", double balance = 0) : base(name, balance)
        {
        }

        public override bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                double totalamount = amount + fee;
                Balance -= totalamount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class TrustAccount : SavingsAccount
    {
        private int withdrawCount = 0;
        private const int bonus = 50;
        private const double limit = 0.2;

        public TrustAccount(string name = "Unnamed Account", double balance = 0, double interestrate = 0) : base(name, balance, interestrate)
        {
        }

        public override bool Deposit(double amount)
        {
            if (amount >= 5000)
            {
                Balance += amount + bonus;
                return true;
            }

            Balance += amount;
            return true;
        }

        public override bool Withdraw(double amount)
        {
            if (withdrawCount > 3)
                return false;

            if (amount > Balance * limit)
                return false;

            withdrawCount++;
            Balance -= amount;
            return true;
        }
    }
    public static class AccountUtil
    {
        public static void Deposit<Acc>(List<Acc> accounts, double amount) where Acc: Account
        {
            Console.WriteLine("\n=== Depositing =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc.Name}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc.Name}");
            }
        }

        public static void Withdraw<Acc>(List<Acc> accounts, double amount) where Acc : Account
        {
            Console.WriteLine("\n=== Withdrawing ================================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc.Name}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc.Name}");
            }
        }
        public static void PrintDetails(List<Account> accounts)
        {
            Console.WriteLine("\n=== Account Info =============================");
            foreach (var acc in accounts)
            {
                Console.WriteLine($"{acc.Name} , has balance {acc.Balance}");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var accounts = new List<Account>
            {
                new Account(),
                new Account("Larry"),
                new Account("Moe", 2000),
                new Account("Curly", 5000)
            };

            var savAccounts = new List<Account>
            {
                new SavingsAccount(),
                new SavingsAccount("Superman"),
                new SavingsAccount("Batman", 2000),
                new SavingsAccount("Wonderwoman", 5000, 5.0)
            };

            var checAccounts = new List<Account>
            {
                new CheckingAccount(),
                new CheckingAccount("Larry2"),
                new CheckingAccount("Moe2", 2000),
                new CheckingAccount("Curly2", 5000)
            };

            var trustAccounts = new List<Account>
            {
                new TrustAccount(),
                new TrustAccount("Superman2"),
                new TrustAccount("Batman2", 2000),
                new TrustAccount("Wonderwoman2", 5000, 5)
            };

            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);

            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);
            AccountUtil.PrintDetails(accounts);
            AccountUtil.PrintDetails(savAccounts);
            AccountUtil.PrintDetails(checAccounts);
            AccountUtil.PrintDetails(trustAccounts);
            Console.WriteLine();
        }




    }
    }

