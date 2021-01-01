using HumanityService.Services;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            NotificationService notificationService = new NotificationService();
            notificationService.NotifyUser("rayanelghazzi@hotmail.com", "Test", "Hi");
        }
    }
}
