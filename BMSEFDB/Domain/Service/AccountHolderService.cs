
using BMSEFDB.Domain.Repository;
using BMSEFDB.Interface.Repository;
using BMSEFDB.Interface.Service;

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSEFDB.Domain.Service
{
    class AccountHolderService : IAccountHolderService
    {
        private readonly IAccountHolderRepository _accountHolderRepository;
        private readonly IAccountRepository _accountRepository;


        OverdraftService OverdraftService = new OverdraftService(new OverdraftRepository());

        public static AccountHolder loggedInAccountHolder;

        public static bool isAcountHolderLoggedIn = false;


        public AccountHolderService(IAccountHolderRepository accountHolderRepository, IAccountRepository accountRepostiory)
        {
            _accountHolderRepository = accountHolderRepository;
            _accountRepository = accountRepostiory;
        }

        public int LogInAccountholder(string email, string password)
        {
            try
            {
                AccountHolder accountHolder = _accountHolderRepository.GetDetailsByEmail(email);

                if (accountHolder == null)
                {
                    throw new Exception($"Account Holder with email {email} Do Not Exist");
                }

                if (password != accountHolder.Password)
                {
                    throw new Exception("Wrong Password");
                }

                isAcountHolderLoggedIn = true;
                loggedInAccountHolder = accountHolder;
                return loggedInAccountHolder.Id;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }

        public void Logoutaccountholder()
        {
            isAcountHolderLoggedIn = false;
            loggedInAccountHolder = null;
        }

        public bool CreateAccountHolder(string firstName, string lastName, string middleName, DateTime dateOfBirth, string email, string phoneNumber, string address, string password, string checkPassword)
        {
            if (password.Equals(checkPassword))
            {
                AccountHolder newAccountHolder = new AccountHolder
                {
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName,
                    DateOfBirth = dateOfBirth,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    Password = password
                };
                int accountHolderId = _accountHolderRepository.CreateAccountHolder(newAccountHolder);

                if (accountHolderId != 0)
                {
                    return CreateAccount(accountHolderId);
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }


        public void SaveMoney()
        {

            Console.Clear();
            Console.WriteLine("     ACCOUNT DEPOSIT MENU");
            Console.Write("How Much Do You Want To Save: ");

            double amountToSave = Convert.ToDouble(Console.ReadLine());




            OverdraftService.PayOverdraft(loggedInAccountHolder.Id, amountToSave);
            loggedInAccountHolder.Account.AccountBalance += amountToSave;

            _accountRepository.Update(loggedInAccountHolder.Account);

            GetAccountDetails(loggedInAccountHolder.Id);

        }

        public void Transfermoney(string accountNumber, double amountToTransfer)
        {
            Console.WriteLine("Enter Your Pin");
            int pin = Convert.ToInt32(Console.ReadLine());
            if (pin != loggedInAccountHolder.Account.Pin)
            {
                Console.WriteLine("Transfer Failed Due To Wrong Pin");
            }
            else
            {
                if (amountToTransfer > loggedInAccountHolder.Account.AccountBalance)
                {
                    Console.WriteLine("Insufficient Funds");
                }
                else
                {
                    Account recepientAccount = _accountRepository.FindByAccountNumber(accountNumber);
                    if (recepientAccount == null)
                    {
                        Console.WriteLine($"Account Holder With Account Number {accountNumber} Does Not Exist");
                    }
                    else
                    {
                        Console.Write($"Are You Sure You Want To Transfer {amountToTransfer} to {recepientAccount.AccountHolder.FirstName} {recepientAccount.AccountHolder.LastName} ? (y/n): ");
                        string reply = Console.ReadLine();
                        if (reply == "y")
                        {
                            loggedInAccountHolder.Account.AccountBalance -= amountToTransfer;
                            recepientAccount.AccountBalance += amountToTransfer;
                            OverdraftService.PayOverdraft(recepientAccount.AccountHolderId, amountToTransfer);
                            _accountRepository.UpdateMultiple(new List<Account>
                        {
                            loggedInAccountHolder.Account,
                            recepientAccount
                        });
                            Console.WriteLine($"You Have Succesfully Transffered {amountToTransfer} to {recepientAccount.AccountHolder.FirstName}");
                        }
                        else
                        {
                            Console.WriteLine("Operation Cancelled");
                        }
                    }

                }
            }

        }



        public void WithdrawMoney(double amountToWithdraw)
        {
            if (amountToWithdraw > loggedInAccountHolder.Account.AccountBalance)
            {
                Console.WriteLine("Withdrawal Failed Due To Insufficient Funds");
            }
            else
            {
                Console.WriteLine($"Withdrawal Of {amountToWithdraw} Succesfull... Thanks For Trusting Us");
                loggedInAccountHolder.Account.AccountBalance -= amountToWithdraw;
                _accountRepository.Update(loggedInAccountHolder.Account);
            }
        }

        public void CheckBalance()
        {
            Console.WriteLine($"Your Account Balance is {loggedInAccountHolder.Account.AccountBalance}");
        }


        public void Test()
        {
            var accountHolder = _accountHolderRepository.GetAccountHolderOverdraftDetails(loggedInAccountHolder.Id);

            foreach (var item in accountHolder.OverDrafts)
            {
                Console.WriteLine(item.Amount);
            }
        }




        public void ChangePassword()
        {
            Console.Clear();
            Console.WriteLine("    Update Password menu");
            Console.Write("  Enter Old Password : ");
            string pass = Console.ReadLine();
            if (pass == loggedInAccountHolder.Password)
            {
                Console.Write("  Enter New Password : ");
                string newPass = Console.ReadLine();
                Console.Write("  Confirm New Password : ");
                string confirmPass = Console.ReadLine();

                if (confirmPass != newPass)
                {
                    Console.WriteLine("Password Update Failed");
                }
                else
                {
                    loggedInAccountHolder.Password = newPass;
                    _accountHolderRepository.Update(loggedInAccountHolder);

                }
            }
        }

        public void UpdateAccountHolderDetails(string option)
        {
            switch (option)
            {
                case "1":
                    Console.Write("Enter new firstName: ");
                    string newFirstName = Console.ReadLine();
                    loggedInAccountHolder.FirstName = newFirstName;
                    break;
                case "2":
                    Console.Write("Enter new lastName: ");
                    string newlastName = Console.ReadLine();
                    loggedInAccountHolder.LastName = newlastName;
                    break;
                case "3":
                    Console.Write("Enter new middleName: ");
                    string newMiddleName = Console.ReadLine();
                    loggedInAccountHolder.MiddleName = newMiddleName;
                    break;
                case "4":
                    Console.Write("Enter new PhoneNUmber: ");
                    string newPhoneNumber = Console.ReadLine();
                    loggedInAccountHolder.PhoneNumber = newPhoneNumber;
                    break;
                case "5":
                    Console.Write("Enter new Address: ");
                    string newAddress = Console.ReadLine();
                    loggedInAccountHolder.Address = newAddress;
                    break;

                default:
                    break;
            }

            _accountHolderRepository.Update(loggedInAccountHolder);


        }


        public void GetOverdraft(double amount)
        {

            if (OverdraftService.HasActiveOverdraft(loggedInAccountHolder.Id))
            {
                Console.WriteLine("You currently have an unpaid overdraft");
                return;
            }
            if (loggedInAccountHolder.Account.AccountBalance >= amount)
            {
                Console.WriteLine($"Your Current Balance Is Greater than {amount} kindly proceed to the Withdrawal Menu");
            }
            else
            {
                double overdraftAmountLeft = amount - loggedInAccountHolder.Account.AccountBalance;

                OverdraftService.GetOverdraft(loggedInAccountHolder.Id, amount, overdraftAmountLeft);
                loggedInAccountHolder.Account.AccountBalance -= amount;
                _accountRepository.Update(loggedInAccountHolder.Account);
                GetAccountDetails(loggedInAccountHolder.Id);
                Console.WriteLine("Your Application For Overdraft Is Succesful");
            }



        }


        public void changePin()
        {

            if (loggedInAccountHolder.Account.Pin != 0)
            {
                if (!ComparePassword()) return;
            }

            Console.Write("Enter your new pin: ");
            int newPin = Convert.ToInt32(Console.ReadLine());
            Console.Write("Confirm pin: ");
            int confirmnewPin = Convert.ToInt32(Console.ReadLine());


            if (newPin == confirmnewPin)
            {
                ChangePin(newPin);
                Console.WriteLine("You Have Succesfully Updated Your Pin");
            }



        }

        public bool ComparePassword()
        {
            Console.Write("Enter your password: ");
            string enteredpassword = Console.ReadLine();
            return enteredpassword.Equals(loggedInAccountHolder.Password);
        }


    

        public void GetAccountDetails(int id)
        {
            loggedInAccountHolder = _accountHolderRepository.GetDetailsByEmail(loggedInAccountHolder.Email);
        }

   



        public bool CreateAccount(int accountHolderId, double accountBalance = 0, int accountPin = 0, int accountStatus = 1)
        {
            string accountNumber = GenerateAccountNumber();

            Account newAccount = new Account
            {
                AccountHolderId = accountHolderId,
                AccountNumber = accountNumber,
                AccountBalance = accountBalance,
                Pin = accountPin,
                AccountStatus = accountStatus
            };

            bool hasCreate = _accountRepository.Create(newAccount);

            if (hasCreate)
            {
                Console.WriteLine($"Account Created Successfully \n Your Account Number is {accountNumber}");
            }

            return hasCreate;

        }

        public void ChangePin(int newPin)
        {
            loggedInAccountHolder.Account.Pin = newPin;
            _accountRepository.Update(loggedInAccountHolder.Account);
        }


        protected string GenerateAccountNumber()
        {
            Random random = new Random();

            string firstFive = random.Next(1, 10000).ToString("00000");
            string secondFive = random.Next(1, 10000).ToString("00000");

            string generatedNumber = $"{firstFive}{secondFive}";

            return generatedNumber;
        }
    }
}
