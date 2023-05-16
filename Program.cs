using System;
using System.IO;
using System.Runtime.InteropServices;
using MailSender;
namespace HDD_check
{
    class Program
    {
        static void Main()
        {
            ConsoleExtension.Hide();
            
            Mailsender mailsender = new Mailsender();

            DriveInfo[] drives = DriveInfo.GetDrives();
            ulong totalFreeSpaceInBytes = 0;

            foreach (DriveInfo drive in drives)
            {
                
                if (drive.IsReady)
                {
                    ulong totalSizeInBytes = (ulong)drive.TotalSize;
                    ulong freeSpaceInBytes = (ulong)drive.AvailableFreeSpace;
                    ulong usedSpaceInBytes = totalSizeInBytes - freeSpaceInBytes;
                    totalFreeSpaceInBytes += freeSpaceInBytes;

                  /* Console.WriteLine($"Toplam Boyut: {ConvertBytesToGB(totalSizeInBytes)} GB");
                    Console.WriteLine($"Boş alan: {ConvertBytesToGB(freeSpaceInBytes)} GB");
                    Console.WriteLine($"Kullanılan Alan: {ConvertBytesToGB(usedSpaceInBytes)} GB");*/
                }

                if (drive.Name=="C:\\"&& ConvertBytesToGB(totalFreeSpaceInBytes) < 15)
                {
                    
                       string content=($"\n C Diski 15 GB'ın altına düştü ");
                    mailsender.Yandex(" ALICI MAİL ADRESİLERİ",null,"DİSK UYARISI ",content, "smtp.yandex.com",587, "gönderen mail adresi", "gönderen şifre");
                    
                }

               
            }
           
        }
        #region Show OR Hide Console
        static class ConsoleExtension
        {
            const int SW_HIDE = 0;
            const int SW_SHOW = 5;
            readonly static IntPtr handle = GetConsoleWindow();
            [DllImport("kernel32.dll")] static extern IntPtr GetConsoleWindow();
            [DllImport("user32.dll")] static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            public static void Hide()
            {
                ShowWindow(handle, SW_HIDE); //hide the console
            }
            public static void Show()
            {
                ShowWindow(handle, SW_SHOW); //show the console
            }
        }
        #endregion
        static double ConvertBytesToGB(ulong bytes)
        {
            return (double)bytes / (1024 * 1024 * 1024);
        }

    }
}
