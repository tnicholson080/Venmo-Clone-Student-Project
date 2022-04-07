using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoConsoleService : ConsoleService
    {



        /************************************************************
            Print methods
        ************************************************************/
        public void PrintLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Welcome to TEnmo!");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }

       
        public LoginUser PromptForLogin()
        {
            string username = PromptForString("User name");
            if (String.IsNullOrWhiteSpace(username))
            {
                return null;
            }
            string password = PromptForHiddenString("Password");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }
         public void PrintMainMenu(string username)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Hello, {username}!");
            Console.WriteLine("1: View your current balance");
            Console.WriteLine("2: View your past transfers");
            Console.WriteLine("3: View your pending requests");
            Console.WriteLine("4: Send TE bucks");
            Console.WriteLine("5: Request TE bucks");
            Console.WriteLine("6: Log out");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }
        
        //User selects 1 in main menu
        public void PrintBalanceMenu(decimal balance)
        {
            Console.WriteLine($"Your current account balance is: ${balance}");
        }

        //User selects 4 in main menu
        public RequestTransfer MakeTransferMenu (List<ApiUser> users, decimal balance)
        {          
            Console.WriteLine("|---------------Users---------------|");
            Console.WriteLine("|          | Id | Username |        |");
            Console.WriteLine("|-------+---------------------------|");
            
            PrintAllUsers(users);
            
            Console.WriteLine("|-----------------------------------|");
            
            return VerifyTransferIdAndAmount(users, balance); 
        }

        public void ViewMyTransfersMenu(List<DisplayTransfer> transfers)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Transfers");
            Console.WriteLine("ID          From/To Amount");
            Console.WriteLine("-------------------------------------------");
            PrintAllTransfers(transfers);
            Console.WriteLine("-------------------------------------------");
            int transferId = PromptForInteger("Please enter transfer ID to view details(0 to cancel)", 0, Int32.MaxValue);
            if(transferId != 0)
            {
                TransferDetailsMenu(transferId, transfers);
            }
        }

        public void TransferDetailsMenu(int transferId, List<DisplayTransfer> transfers)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Transfer Details:                           ");
            Console.WriteLine("--------------------------------------------");
            PrintTransferDetails(transferId, transfers);
            Pause();
            
        }
         
        public void PrintAllUsers(List<ApiUser> users)
        {
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"{users[i].UserId} | {users[i].Username}");
            }
        }

       
        public void PrintAllTransfers(List<DisplayTransfer> transfers)
        {
            for (int i = 0; i < transfers.Count; i++)
            {
                Console.WriteLine($"TransferID{transfers[i].TransferId}     From:{transfers[i].SenderUserName.PadRight(5)}   To:{transfers[i].RecieverUserName.PadRight(5)}   ${transfers[i].Amount}");
           
            }
        }
        
        public void PrintTransferDetails(int transferId, List<DisplayTransfer> transfers)
        {
            for (int i = 0; i < transfers.Count; i++)
            {
                if (transfers[i].TransferId == transferId)
                {
                    Console.WriteLine($"Id: {transfers[i].TransferId}");
                    Console.WriteLine($"From: {transfers[i].SenderUserName.PadRight(5)}");
                    Console.WriteLine($"To: {transfers[i].RecieverUserName.PadRight(5)}");
                    Console.WriteLine($"Type: {transfers[i].TransferType}");
                    Console.WriteLine($"Status: {transfers[i].TransferStatusId}");
                    Console.WriteLine($"Amount: ${transfers[i].Amount}");
                }
            }
        }

        public RequestTransfer VerifyTransferIdAndAmount(List<ApiUser> users, decimal balance)
        {
            RequestTransfer newTransfer = new RequestTransfer();
            bool isValid = false;

            while (!isValid)
            {
                newTransfer.AccountTo = PromptForInteger("ID of the user you are sending to[0]: ", 0, Int32.MaxValue);

                foreach (ApiUser apiUser in users)
                {
                    if (apiUser.UserId == newTransfer.AccountTo)
                    {
                        isValid = true;
                        break;
                    }
                }
                if (!isValid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter valid userID.");
                    Console.ResetColor();
                    Pause();
                }
            }

            newTransfer.Amount = PromptForDecimal("Amount you would like to send: ", 0);

            while ((balance < newTransfer.Amount) || newTransfer.Amount <= 0 )
            {
                Console.WriteLine("You cannot overdraw your account, or send non valid amount");
                Console.Write("To return to the previous menu press q");
                
                
                if (Console.ReadLine() == "q")
                {
                    return null;
                }
                newTransfer.Amount = PromptForDecimal("Amount you would like to send: ", 0);
            }

            return newTransfer;
        }
    }

}
