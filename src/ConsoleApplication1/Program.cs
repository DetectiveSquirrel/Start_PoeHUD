using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Start_PoeHUD
{
    class Program
    {
        public static string CurrentDirectory;
        public static string ApplicationName;
        public static string FullLocation;
        public static List<string> OtherEXEs = new List<string>();

        static void Main(string[] args)
        {
            // Setting app name and location it started from
            CurrentDirectory = Directory.GetCurrentDirectory();
            ApplicationName = Assembly.GetCallingAssembly().GetName().Name;
            FullLocation = CurrentDirectory + "\\" + ApplicationName + ".exe";

            // gather exe's in current folder
            var exeFiles = Directory.EnumerateFiles(CurrentDirectory, "*.exe", SearchOption.TopDirectoryOnly);
            foreach (var exe in exeFiles)
            {
                // if its not the current application add to list
                if (exe != FullLocation)
                    OtherEXEs.Add(exe);
            }

            // if we found more than one other exe (suspect multiple poehuds) give warning and choose to continue
            if (OtherEXEs.Count > 1)
            {
                DialogResult Clickhandle = MessageBox.Show(
                    string.Format("We have found {0} EXE's within this folder\nThis might result is multiple PoEHUDs opening\nWould you like to continue?", OtherEXEs.Count),
                    "Warning", MessageBoxButtons.YesNo);

                if (Clickhandle == DialogResult.No)
                    Environment.Exit(0);
            }

            foreach (var exe in OtherEXEs)
            {
                Process.Start(exe);
            }

            // when debugging it stops the console from closing instantly
            //Console.ReadLine();
        }

    }
}
