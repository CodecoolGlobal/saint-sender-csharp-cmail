namespace SaintSender.Core.Models
{
    using SaintSender.Core.Services;
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="Isolate" />.
    /// </summary>
    static public class Isolate
    {

        public static IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
        /// <summary>
        /// The SaveIntoIsolatedStorage.
        /// </summary>
        /// <param name="account">The account<see cref="Account"/>.</param>
        static public void SaveIntoIsolatedStorage(Account account)
        {
            isoStore.DeleteFile("AccountStore.txt");

            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("AccountStore.txt", FileMode.CreateNew, isoStore))
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

        static public Account ReadFromIsolatedStorage()
        {
            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("AccountStore.txt", FileMode.Open, isoStore))
            {
                using (StreamReader reader = new StreamReader(isoStream))
                {
                    Console.WriteLine("Reading contents:");
                    
                    return new Account(reader.ReadLine(), reader.ReadLine());
                }
            }
        }
    }
}
