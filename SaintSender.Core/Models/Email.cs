﻿using System;

namespace SaintSender.Core.Models
{
    public class Email
    {
        public string Message { get; set; }
        public string Sender { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public bool Read { get; set; }

        public Email(string message, string sender, DateTime date, string subject, bool read)
        {
            Message = message;
            Sender = sender;
            Date = date;
            Subject = subject;
            Read = read;
        }
    }
}