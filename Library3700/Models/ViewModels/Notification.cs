using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3700.Models.ViewModels
{
    public class Notification
    {
        private static int _count;

        private int _id;
        private string _text;

        static Notification()
        {
            _count = 0;
        }

        public Notification(string text)
        {
            _id = _count++;
            _text = text;
        }

        public int Id { get; }

        public string GetNotification => _text;
    }
}