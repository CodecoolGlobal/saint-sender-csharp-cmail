using SaintSender.Core.Services;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading;

namespace SaintSender.Core.Models
{
    public static class Isolate
    {
        private const string _accountFilePath = "AccountStore.txt";

        public static IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

        public static void SaveIntoIsolatedStorage(Account account)
        {
            if (isoStore.FileExists(_accountFilePath))
                isoStore.DeleteFile(_accountFilePath);

            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(_accountFilePath, FileMode.CreateNew, isoStore))
            {
                using (StreamWriter writer = new StreamWriter(isoStream))
                {
                    AccountService accountService = new AccountService();
                    writer.WriteLine(account.Email);
                    writer.WriteLine(account.Password);
                    Console.WriteLine("Account stored");
                }
            }
        }

        public static Account ReadFromIsolatedStorage()
        {
            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(_accountFilePath, FileMode.Open, isoStore))
            {
                using (StreamReader reader = new StreamReader(isoStream))
                {
                    Console.WriteLine("Reading contents:");
                    return new Account(reader.ReadLine(), reader.ReadLine());
                }
            }
        }


        public static void DeleteFromIsolatedStorage()
        {
            isoStore.DeleteFile(_accountFilePath);
        }
    }
}
