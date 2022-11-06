using System;
using System.Collections.Generic;
using TenmoClient.Models;
using TenmoClient.Services;


namespace TenmoClient
{
    public class TenmoApp
    {
        private readonly TenmoConsoleService console = new TenmoConsoleService();
        private readonly TenmoApiService tenmoApiService;
        private readonly TenmoConsoleService tenmoConsole;

        public TenmoApp(string apiUrl)
        {
            tenmoApiService = new TenmoApiService(apiUrl);
        }

        public void Run()
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                // The menu changes depending on whether the user is logged in or not
                if (tenmoApiService.IsLoggedIn)
                {
                    keepGoing = RunAuthenticated();
                }
                else // User is not yet logged in
                {
                    keepGoing = RunUnauthenticated();
                }
            }
        }

        private bool RunUnauthenticated()
        {
            console.PrintLoginMenu();
            int menuSelection = console.PromptForInteger("Please choose an option", 0, 2, 1);
            while (true)
            {
                if (menuSelection == 0)
                {
                    return false;   // Exit the main menu loop
                }

                if (menuSelection == 1)
                {
                    // Log in
                    Login();
                    return true;    // Keep the main menu loop going
                }

                if (menuSelection == 2)
                {
                    // Register a new user
                    Register();
                    return true;    // Keep the main menu loop going
                }
                console.PrintError("Invalid selection. Please choose an option.");
                console.Pause();
            }
        }

        private bool RunAuthenticated()
        {
            console.PrintMainMenu(tenmoApiService.Username);
            int menuSelection = console.PromptForInteger("Please choose an option", 0, 6);
            if (menuSelection == 0)
            {
                // Exit the loop
                return false;
            }

            if (menuSelection == 1)
            {
                ViewBalance();
                console.Pause();

            }

            if (menuSelection == 2)
            {
                DisplayTransfers();
                console.Pause();
            }

            if (menuSelection == 3)
            {
                // View your pending requests
            }

            if (menuSelection == 4)
            {
                DisplayUsers();
                console.SelectReceiver();
                int input = Convert.ToInt32(Console.ReadLine());
                console.SelectAmount();
                int amount = Convert.ToInt32(Console.ReadLine());

                Account account = tenmoApiService.GetAccount();
                if (account.Balance > amount && amount != 0 && amount >0)
                {
                    tenmoApiService.MakeTransfer(input, amount);
                    Console.WriteLine("Your transfer was successful! :)");
                    console.Pause();
                }
                else
                {
                    Console.WriteLine("Error, cannot send more money than you have!");
                    console.Pause();
                }
            }

            if (menuSelection == 5)
            {
                // request money
            }

            if (menuSelection == 6)
            {
                // Log out
                tenmoApiService.Logout();
                console.PrintSuccess("You are now logged out");
            }

            return true;    // Keep the main menu loop going
        }

        private void Login()
        {
            LoginUser loginUser = console.PromptForLogin();
            if (loginUser == null)
            {
                return;
            }

            try
            {
                ApiUser user = tenmoApiService.Login(loginUser);
                if (user == null)
                {
                    console.PrintError("Login failed.");
                }
                else
                {
                    console.PrintSuccess("You are now logged in");
                }
            }
            catch (Exception)
            {
                console.PrintError("Login failed.");
            }
            console.Pause();
        }

        private void Register()
        {
            LoginUser registerUser = console.PromptForLogin();
            if (registerUser == null)
            {
                return;
            }
            try
            {
                bool isRegistered = tenmoApiService.Register(registerUser);
                if (isRegistered)
                {
                    console.PrintSuccess("Registration was successful. Please log in.");
                }
                else
                {
                    console.PrintError("Registration was unsuccessful.");
                }
            }
            catch (Exception)
            {
                console.PrintError("Registration was unsuccessful.");
            }
            console.Pause();
        }

        public void ViewBalance()
        {

            Account account;
            try
            {

                account = tenmoApiService.GetAccount();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("Your current balance is: " + account.Balance.ToString("C"));

            // Account account = tenmoApiService.GetAccount();


        }

        public void DisplayUsers()
        {
            List<ApiUser> users;
            try
            {
                users = tenmoApiService.GetUsers();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine("The current users are: ");
            foreach (ApiUser user in users)
            {
                Console.WriteLine(user.Username + " " + user.UserId);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        public void DisplayTransfers()
        {
            List<ReturnTransfer> transfers;
            try
            {
                transfers = tenmoApiService.GetTransfers();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine("All transfers: ");
            foreach (ReturnTransfer transfer in transfers)
            {
                Console.WriteLine("Transfer ID: " + transfer.TransferId + " " + "|" + "Transfer From: " +  transfer.UserFrom + " " + "|" + "Transfer To: " + transfer.UserTo + " " + "|" + "Transfer Amount: " + transfer.Amount);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
