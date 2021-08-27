using SaintSender.Core.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Collections.Generic;

namespace SaintSender.Core.Models
{
    public static class Isolate
    {
        public const string _accountFilePath = "AccountStore.txt";

        public static IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

        public static void SaveIntoIsolatedStorage(Account account)
        {
            if (isoStore.FileExists(_accountFilePath))
            {
                isoStore.DeleteFile(_accountFilePath);
            }

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
            if (!isoStore.FileExists(_accountFilePath))
            {
                return null;
            }

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
            foreach (string s in isoStore.GetFileNames($"{Authentication.GetAddress()}//*"))
            {
                isoStore.DeleteFile($"{Authentication.GetAddress()}//{s}");         // why doesn't it return full path when it searches for full path?
            }

            isoStore.DeleteDirectory(Authentication.GetAddress());
        }

        internal static ObservableCollection<Email> ReadCache(string email)
        {
            string[] rawEmails = isoStore.GetFileNames($"{email}//*");
            List<Email> parsedEmails = new List<Email>();
            foreach (string s in rawEmails)
            {
                System.Diagnostics.Debug.WriteLine(s);
                using (FileStream stream = isoStore.OpenFile($"{email}//{s}", FileMode.Open))
                {
                    byte[] buffer = new byte[4];
                    byte[] stringBuffer = new byte[0];
                    int chars;
                    int readCount = 0;

                    readCount += stream.Read(buffer, 0, 1);
                    bool read = buffer[0] > 0x0;
                    System.Diagnostics.Debug.WriteLine("Read flag parsed");

                    string sender = ReadString(stream, buffer, ref stringBuffer, ref readCount);
                    string subject = ReadString(stream, buffer, ref stringBuffer, ref readCount);
                    string message = ReadString(stream, buffer, ref stringBuffer, ref readCount);

                    chars = (int)stream.Length - readCount;
                    stringBuffer = new byte[chars];
                    readCount += stream.Read(stringBuffer, 0, chars);
                    DateTime arrival = DateTime.Parse(Encoding.Default.GetString(stringBuffer));

                    string id = s;
                    parsedEmails.Add(new Email(message, sender, arrival, subject, read, id));
                }
            }
            return new ObservableCollection<Email>(parsedEmails);
        }

        private static string ReadString(FileStream stream, byte[] buffer, ref byte[] stringBuffer, ref int readCount)
        {
            readCount += stream.Read(buffer, 0, 4);
            int chars = FromBytes(buffer);
            string temp = "";
            System.Diagnostics.Debug.WriteLine($"chars: {chars}");
            for (int i = 0; i * 100 < chars; i++)
            {
                System.Diagnostics.Debug.WriteLine($"{i}");
                int length = Math.Min(100, chars - (i * 100));
                stringBuffer = new byte[length];
                readCount += stream.Read(stringBuffer, 0, length);
                temp += Encoding.Default.GetString(stringBuffer);
            }
            return temp;
        }

        public static string[] GetOfflineAccounts()
        {
            return isoStore.GetDirectoryNames();
        }

        public static void SaveMail(ObservableCollection<Email> emails, string address)
        {
            isoStore.CreateDirectory(address);
            foreach (Email e in emails)
            {
                string filepath = $"{address}//{e.ID}";
                if (isoStore.FileExists(filepath))
                {
                    continue;
                }

                FileStream stream = isoStore.OpenFile(filepath, FileMode.Create);
                stream.WriteByte((byte)(e.Read ? 0x1 : 0x0));
                stream.Write(BytesOf(e.Sender.Length), 0, 4);
                stream.Write(Encoding.Default.GetBytes(e.Sender), 0, e.Sender.Length);
                stream.Write(BytesOf(e.Subject.Length), 0, 4);
                stream.Write(Encoding.Default.GetBytes(e.Subject), 0, e.Subject.Length);
                stream.Write(BytesOf(e.Message.Length), 0, 4);
                stream.Write(Encoding.Default.GetBytes(e.Message), 0, e.Message.Length);
                string date = e.Date.ToString();
                stream.Write(Encoding.Default.GetBytes(date), 0, date.Length);
            }
        }

        private static byte[] BytesOf(int i)
        {
            byte[] x = new byte[4];
            for (int j = 0; j < 4; j++)
            {
                x[j] = (byte)(i % 0x100);
                i /= 0x100;
            }
            return x;
        }

        private static int FromBytes(byte[] buffer)
        {
            int x = 0;
            for (int i = 0; i < 4; i++)
            {
                x *= 0x100;
                x += buffer[3 - i];
            }
            return x;
        }
    }
}
