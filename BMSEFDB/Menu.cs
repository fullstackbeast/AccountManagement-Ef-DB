
using BMSEFDB.Domain.Repository;
using BMSEFDB.Domain.Service;
using BMSEFDB.Interface.Service;
using System;

namespace BMSEFDB
{
    public class Menu
    {
        static AccountHolderService accountHolderService = new AccountHolderService(new AccountHolderRepository(), new AccountRepository());
        //static ManagerService managerService = new ManagerService(new ManagerRepository());

        static LoanService loanService = new LoanService(new LoanRepository());

        public static void MenuSwitch()
        {
            bool check = true;

            do
            {
                ShowMainMenu();

                string option = Console.ReadLine();

                if (option == "0")
                {
                    check = false;
                    Console.WriteLine("Thank you for using our Banking App");
                }
                else
                {
                    HandleSubMenu(option);
                }



            } while (check);

        }

        public static void ShowContinueMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to continue.......");
            Console.ReadKey();
        }

        public static void ResetMenuSettings()
        {
        Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ShowMainMenu()
        {
            //ResetMenuSettings();
            Console.WriteLine("     MAIN MENU");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Account Holder Menu");
            Console.WriteLine("2. Account Manager Menu");
        }

        public static void ShowAccountHolderMenu()
        {
            ResetMenuSettings();
            Console.WriteLine("     ACCOUNT HOLDER MENU");
            Console.WriteLine("0. Return");
            Console.WriteLine("1. Create account");
            Console.WriteLine("2. Login");
        }

        public static void ShowLoggedInAccountHolderMenu()
        {
            //resetmenusettings();
            Console.WriteLine($"   welcome {AccountHolderService.loggedInAccountHolder.FirstName}");
            Console.WriteLine("0. LogOut");
            Console.WriteLine("1. Save");
            Console.WriteLine("2. Transfer");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Check Balance");
            Console.WriteLine("5. Loan Service");
            Console.WriteLine("6. Request OverDraft");
            Console.WriteLine("7. Update Details");
            Console.WriteLine("8. Change Password");
            Console.WriteLine("9. Set Pin");

        }

        //public static void ShowAccountManagerMenu()
        //{
        //    ResetMenuSettings();
        //    Console.WriteLine("     ACCOUNT MANAGER MENU");
        //    Console.WriteLine("0. Return");
        //    Console.WriteLine("1. Register Manager");
        //    Console.WriteLine("2. Login As Manager");

        //}

        //public static void ShowLoggedInManagerMenu()
        //{
        //    ResetMenuSettings();
        //    Console.WriteLine($"   Welcome ");
        //    Console.WriteLine("0. Log Out");
        //    Console.WriteLine("1. List Account Holders");
        //    Console.WriteLine("2. Delete Account Holder");
        //    Console.WriteLine("3. Update Account Holder");
        //    Console.WriteLine("4. List All Loans");
        //    Console.WriteLine("5. List All Overdrafts");
        //}

        public static void ShowAccountHolderUpdateMenu()
        {
            ResetMenuSettings();
            Console.WriteLine("     ACCOUNT UPDATE MENU");
            Console.WriteLine("1. Update First Name");
            Console.WriteLine("2. Update Last Name");
            Console.WriteLine("3. Update Middle Name");
            Console.WriteLine("4. Update Phone Number");
            Console.WriteLine("5. Update Address");
        }

        //public static void ShowAccountManagerUpdateMenu()
        //{
        //    ShowAccountHolderUpdateMenu();
        //    Console.WriteLine("6. Update Account Status");
        //}

        public static void ShowLoanServiceMenu()
        {
            ResetMenuSettings();
            Console.WriteLine("     LOAN SERVICE MENU");
            Console.WriteLine("0. Return");
            Console.WriteLine("1. Request Loan");
            Console.WriteLine("2. Pay Back Loan");
            Console.WriteLine("3. Check Loan Balance");
        }


        public static void ShowLoanMenu()
        {
            ResetMenuSettings();
            Console.WriteLine("     SELECT LOAN TYPE:");
            Console.WriteLine("0. Return");
            Console.WriteLine("1. House Loan");
            Console.WriteLine("2. Car Loan");
            Console.WriteLine("3. Business Loan");
        }

        public static void HandleSubMenu(string option)
        {
            var miniOption = string.Empty;

            try
            {
                if (option.Equals("0"))
                {
                    return;
                }
                else if (option.Equals("1"))
                {
                    ShowAccountHolderMenu();
                    miniOption = Console.ReadLine();
                    HandleAccountHolderMenu(miniOption);
                }
                else if (option.Equals("2"))
                {
                    //ShowAccountManagerMenu();
                    //miniOption = Console.ReadLine();
                    //HandleAccountManagerMenu(miniOption);
                }
                else
                {
                    throw new Exception("Input Not Recognized");
                }
            }
            catch (Exception em)
            {
                Console.WriteLine($"Error: {em.Message} ");
            }
        }

        public static void HandleAccountHolderMenu(string subOption)
        {

            if (subOption.Equals("0"))
            {
                return;
            }
            else if (subOption.Equals("1"))
            {
                Console.Clear();
                Console.WriteLine("     ACCOUNT HOLDER REGISTRATION");

                Console.Write("Enter your First Name: ");
                string firstName = Console.ReadLine();

                Console.Write("Enter your Last Name: ");
                string lastName = Console.ReadLine();

                Console.Write("Enter your Middle Name: ");
                string middleName = Console.ReadLine();

                Console.Write("Enter your Date Of Birth (yyyy/mm/dd): ");
                DateTime dateOfBirth = Convert.ToDateTime(Console.ReadLine());

                Console.Write("Enter your Email Address: ");
                string email = Console.ReadLine();

                Console.Write("Enter your Phone Number: ");
                string phoneNumber = Console.ReadLine();

                Console.Write("Enter your Address: ");
                string address = Console.ReadLine();

                Console.Write("Enter your Password: ");
                string password = Console.ReadLine();

                Console.Write("Confirm your Password:  ");
                string confirmPassword = Console.ReadLine();
                accountHolderService.CreateAccountHolder(firstName, lastName, middleName, dateOfBirth, email, phoneNumber, address, password, confirmPassword);
                ShowContinueMenu();
                HandleSubMenu("1");
            }
            else if (subOption.Equals("2"))
            {

                if (AccountHolderService.isAcountHolderLoggedIn)
                {

                    ShowLoggedInAccountHolderMenu();
                    string miniOption = Console.ReadLine();

                    if (miniOption.Equals("0"))
                    {
                        accountHolderService.Logoutaccountholder();
                        HandleSubMenu("1");
                    }
                    else
                    {
                        HandleLoggedInAccountHolderMenu(miniOption);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("    Account Holder Login");
                    Console.Write("enter your email: ");
                    string email = Console.ReadLine();
                    Console.Write("enter your password: ");
                    string password = Console.ReadLine();
                    int accountHolderId = accountHolderService.LogInAccountholder(email, password);
                    if(accountHolderId == 0)
                    {
                        ShowContinueMenu();
                    }
                    else
                    {
                        HandleAccountHolderMenu("2");
                    }


                }
            }
            else
            {
                HandleSubMenu("1");
            }
        }

        //public static void HandleAccountManagerMenu(string suboption)
        //{
        //    if (suboption.Equals("0"))
        //    {
        //        return;
        //    }
        //    else if (suboption.Equals("1"))
        //    {
        //        Auth.CreateManager();
        //        HandleSubMenu("2");

        //    }
        //    else if (suboption.Equals("2"))
        //    {
        //        if (Auth.isManagerLoggedIn)
        //        {

        //            ShowLoggedInManagerMenu();
        //            string miniOption = Console.ReadLine();

        //            if (miniOption.Equals("0"))
        //            {
        //                Auth.LogoutAccountManager();
        //                HandleSubMenu("1");
        //            }
        //            else
        //            {
        //                ShowLoggedInManagerMenu();

        //            }
        //        }
        //        else
        //        {
        //            Auth.LoginAccountManager();
        //            ShowLoggedInManagerMenu();
        //            string option = Console.ReadLine();
        //            HandleLoggedInManagerOperations(option);
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("input not recognized");
        //    }

        //}

        public static void HandleLoanMenu(string option)
        {


            double loanAmount;
            string loanType = string.Empty;
            bool isError = false;

            try
            {
                if (option.Equals("1"))
                {
                    loanAmount = 100000;
                    loanType = "House";
                }

                else if (option.Equals("2"))
                {
                    loanAmount = 500000;
                    loanType = "car";
                }

                else if (option.Equals("3"))
                {
                    loanAmount = 1000000;
                    loanType = "Business";
                }

                else
                {
                    isError = true;
                    throw new Exception("Input not recognized.");

                }
                if (!isError)
                {
                    loanService.AddLoan(AccountHolderService.loggedInAccountHolder.Id, loanAmount, loanType);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                ShowContinueMenu();
                HandleAccountHolderMenu("2");
            }

        }

        //public static void HandleLoggedInManagerOperations(string option)
        //{
        //    //AccountHolderRepository accountHolderRepository = new AccountHolderRepository();
        //    if (option.Equals("0"))
        //    {
        //        //CreateAccounts.LogoutAccountManager();
        //        HandleSubMenu("2");
        //    }
        //    else if (option.Equals("1"))
        //    {
        //        Console.Clear();
        //        Console.WriteLine("     LIST OF ACCOUNT HOLDERS");
        //        managerService.ListOfAccountHolders();
        //        ShowContinueMenu();
        //        HandleAccountManagerMenu("2");
        //    }

        //    else if (option.Equals("2"))
        //    {
        //        Console.Clear();
        //        Console.WriteLine("   ACCOUNT HOLDER DELETE MENU");
        //        Console.Write("Enter Account Id: ");
        //        int id = Convert.ToInt32(Console.ReadLine());

        //        accountHolderService.RemoveAccountHolder(id: id);
        //        ShowContinueMenu();
        //        HandleAccountManagerMenu("2");
        //    }
        //    else if (option.Equals("3"))
        //    {
        //        Console.Clear();
        //        Console.WriteLine("   ACCOUNT HOLDER UPDATE MENU");
        //        Console.Write("Enter Account Id: ");
        //        int id = Convert.ToInt32(Console.ReadLine());

        //        AccountHolder account = accountHolderService.FindById(id);
        //        if (account == null)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            Console.WriteLine("Account doesn't exist");
        //            Menu.ShowContinueMenu();
        //        }
        //        else
        //        {
        //            Menu.ShowAccountManagerUpdateMenu();

        //            string updateOption = Console.ReadLine();

        //            if (updateOption.Equals("6"))
        //            {
        //                Console.Write("Enter New Account Status(active/inactive): ");
        //                string changeAccountStatus = Console.ReadLine().ToLower();
        //                if (changeAccountStatus.Equals("active") || changeAccountStatus.Equals("inactive"))
        //                {
        //                    //accountHolderService.UpdateAccountHolder(id);
        //                }
        //                else
        //                {
        //                    Console.ForegroundColor = ConsoleColor.Red;
        //                    Console.WriteLine("Invalid input");

        //                }
        //                ShowContinueMenu();
        //                HandleAccountManagerMenu("2");

        //            }
        //            else
        //            {
        //                //UpdateAccountHolderMenu(id, updateOption);
        //            }


        //        }


        //    }

        //    else if (option.Equals("4"))
        //    {
        //        Console.Clear();
        //        Console.WriteLine("     LIST OF LOANS");
        //        managerService.ListLoans();
        //        ShowContinueMenu();
        //        HandleAccountManagerMenu("2");
        //    }
        //    else if (option.Equals("5"))
        //    {
        //        Console.Clear();
        //        Console.WriteLine("     LIST OF OVERDRAFTS");
        //        managerService.ListAllOverdrafts();
        //        ShowContinueMenu();
        //        HandleAccountManagerMenu("2");
        //    }
        //}

        //public static void UpdateAccountHolderMenu(int id, string updateOption)
        //        {

        //            try
        //            {
        //                if (updateOption.Equals("1"))
        //                {
        //                    Console.Write("Enter New First Name: ");
        //                    string changeFirstName = Console.ReadLine();
        //                    AccountHolderRepository.UpdateAccountHolder(id, firstName: changeFirstName);
        //                }
        //                else if (updateOption.Equals("2"))
        //                {
        //                    Console.Write("Enter New Last Name: ");
        //                    string changeLastName = Console.ReadLine();
        //                    AccountHolderRepository.UpdateAccountHolder(id, lastName: changeLastName);

        //                }
        //                else if (updateOption.Equals("3"))
        //                {
        //                    Console.Write("Enter New Middle Name: ");
        //                    string changeMiddleName = Console.ReadLine();
        //                    AccountHolderRepository.UpdateAccountHolder(id, middleName: changeMiddleName);


        //                }
        //                else if (updateOption.Equals("4"))
        //                {
        //                    Console.Write("Enter New Phone Number: ");
        //                    string changePhoneNumber = Console.ReadLine();
        //            //        //AccountHolderRepository.UpdateAccountHolder(id, phoneNumber: changePhoneNumber);


        //    }
        //    else if (updateOption.Equals("5"))
        //    {
        //        Console.Write("Enter Your New Address: ");
        //        string changeAddress = Console.ReadLine();
        //        // AccountHolderRepository.UpdateAccountHolder(id, address: changeAddress);

        //    }
        //    else
        //    {
        //        throw new Exception("Input  Not Recognized");
        //    }
        //}
        //catch (Exception e)
        //{
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.WriteLine($"Error: {e.Message}");
        //}
        //finally
        //{
        //    ShowContinueMenu();
        //}

        public static void HandleLoggedInAccountHolderMenu(string option)
        {
            //    //LoanManagement loanManagement = new LoanManagement();
            //    //OverdraftManagement overdraftManagement = new OverdraftManagement();
            switch (option)
            {
                case "0":
                    HandleSubMenu("1");
                    break;

                case "1":
                    accountHolderService.SaveMoney();
               
                    HandleAccountHolderMenu("2");
                    break;
                case "2":
                    Menu.ResetMenuSettings();
                    Console.WriteLine("    TRANSFER MENU");
                    Console.Write("Enter Recepients Account Number: ");
                    string accountNumber = Console.ReadLine();
                    Console.Write("Enter Amount To Transfer: ");
                    double amountToTransfer = Convert.ToDouble(Console.ReadLine());
                 
                    accountHolderService.Transfermoney(accountNumber,amountToTransfer);
                    HandleAccountHolderMenu("2");
                    break;
                case "3":
                    Menu.ResetMenuSettings();
                    Console.WriteLine("    WITHDRAWAL MENU");
                    Console.Write("Enter Amount to Withdraw: ");
                    double amountToWithdraw = Convert.ToDouble(Console.ReadLine());
                    accountHolderService.WithdrawMoney(amountToWithdraw);
                    ShowContinueMenu();
                    HandleAccountHolderMenu("2");
                    break;

                case "4":
                    Menu.ResetMenuSettings();
                    Console.WriteLine("    ACCOUNT BALANCE MENU");
                    accountHolderService.CheckBalance();
                    ShowContinueMenu();
                    HandleAccountHolderMenu("2");
                    break;

                case "5":
                    ShowLoanServiceMenu();
                    string loanOption = Console.ReadLine();
                    if (loanOption.Equals("0"))
                    {
                        HandleAccountHolderMenu("2");
                    }
                    else if (loanOption.Equals("1"))
                    {
                        ShowLoanMenu();
                        string minOption = Console.ReadLine();
                        if (minOption.Equals("0"))
                        {
                            HandleAccountHolderMenu("2");

                        }
                        else
                        {
                            HandleLoanMenu(minOption);
                        }

                    }
                    else if (loanOption.Equals("2"))
                    {

                        Menu.ResetMenuSettings();
                        Console.WriteLine("     LOAN PAYBACK MENU");
                        Console.Write("How much do you want to pay: ");
                        double amountToPay = Convert.ToDouble(Console.ReadLine());
                        loanService.PayLoan(AccountHolderService.loggedInAccountHolder.Id, amountToPay);
                        Menu.ShowContinueMenu();
                        HandleAccountHolderMenu("2");

                    }
                    else if (loanOption.Equals("3"))
                    {
                        double balance = loanService.checkLoanBalance(AccountHolderService.loggedInAccountHolder.Id);
                        string message = (balance == 0) ? $"You do not have an active loan" : $"You currently have an unpaid loan of {balance}";
                        Console.WriteLine(message);
                        Menu.ShowContinueMenu();
                        HandleAccountHolderMenu("2");
                    }
                    break;

                case "6":
                    ResetMenuSettings();
                    Console.WriteLine("     OVERDRAFT MENU");
                    Console.Write("How much overdraft?: ");
                    double overdraftAmount = Convert.ToDouble(Console.ReadLine());
                    if (overdraftAmount > 100000)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Overdraft limit exceeded. Maximum overdraft is 100,000");
                    }
                    else
                    {
                        accountHolderService.GetOverdraft(overdraftAmount);

                    }

                    ShowContinueMenu();
                    HandleAccountHolderMenu("2");
                    break;

                case "7":
                    ShowAccountHolderUpdateMenu();
                    string updateOption = Console.ReadLine();
                    accountHolderService.UpdateAccountHolderDetails(updateOption);
                    HandleAccountHolderMenu("2");
                    break;

                case "8":

                    accountHolderService.ChangePassword();
                    HandleAccountHolderMenu("2");
                    break;

                case "9":
                    accountHolderService.changePin();
                    HandleAccountHolderMenu("2");
                    break;

                default:
                    break;
            }

        }

    }
}







