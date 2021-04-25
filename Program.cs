using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace LB3
{
    class Program
    {

        static void Main(string[] args)
        {
            string name = "";
            string surname = "";
            double homeworks_average = 0;
            string[] all_names = new string[10000000];
            string[] all_surnames = new string[10000000];
            double[] all_Final_points = new double[10000000];
            double[] all_homeworks = new double[10000000];
            double[] all_Final_points_median = new double[10000000];
            int exam = 0;
            bool x = false;
            int choice;
            string choice2;
            int choice3;
            int count = 0;
            double Final_points;
            double Final_points_median;
            int examchoice;
            String filepath = @"C:\Users\Dewivis\Documents\GitHub\PaskutinisLD\inputdata.txt";
            String resultpath = @"C:\Users\Dewivis\Documents\GitHub\PaskutinisLD\students.txt";
            Random rnd = new Random();
            while (x != true)
            {
                try
                {
                    Console.WriteLine("Press 1 to write a student");
                    Console.WriteLine("Press 2 to print students");
                    Console.WriteLine("press 3 to read from file");
                    Console.WriteLine("Press 4 to exit");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        bool y = false;
                        int homework_count = 0;
                        Console.WriteLine("Enter students name:");
                        name = Console.ReadLine();
                        all_names[count] = name;
                        Console.WriteLine("Enter students surname:");
                        surname = Console.ReadLine();
                        all_surnames[count] = surname;
                        while (y != true)
                        {
                            Console.WriteLine("Enter students homework grade , type y to random the grade or type x to stop");
                            choice2 = Console.ReadLine();
                            if (choice2 == "x")
                            {
                                homeworks_average = all_homeworks.Average();
                                y = true;
                            }
                            else if (choice2 == "y")
                            {
                                all_homeworks[homework_count] = rnd.Next(1, 10);
                                homework_count = homework_count + 1;
                            }
                            else
                            {
                                all_homeworks[homework_count] = int.Parse(choice2);
                                homework_count = homework_count + 1;
                            }
                        }
                        int grade_count = all_homeworks.Count();
                        int half = all_homeworks.Count() / 2;
                        var sorted_grades = all_homeworks.OrderBy(n => n);
                        double median;
                        if ((grade_count % 2) == 0)
                        {
                            median = ((sorted_grades.ElementAt(half) + sorted_grades.ElementAt((half - 1))) / 2);

                        }
                        else
                        {
                            median = sorted_grades.ElementAt(half);
                        }

                        Console.WriteLine("Press 1 to enter students exam grade or 2 to random the grade");
                        examchoice = int.Parse(Console.ReadLine());
                        switch (examchoice)
                        {
                            case 1:
                                Console.WriteLine("enter exam grade");
                                exam = int.Parse(Console.ReadLine());
                                break;
                            case 2:
                                exam = rnd.Next(1, 10);
                                break;
                        }
                        Final_points = 0.3 * homeworks_average + 0.7 * exam;
                        Final_points_median = 0.3 * median + 0.7 * exam;
                        all_Final_points[count] = Final_points;
                        all_Final_points_median[count] = Final_points_median;
                        count = count + 1;
                        break;
                    case 2:
                        Console.WriteLine("Press 1 for average");
                        Console.WriteLine("Press 2 for median ");
                        Console.WriteLine("Press 3 to write to file");
                        choice3 = int.Parse(Console.ReadLine());
                        switch (choice3)
                        {
                            case 1:
                                Console.WriteLine("Name    Surname    Final points");
                                for (int i = 0; i < count; i++)
                                {
                                    string temp = all_Final_points[i].ToString("F");
                                    String.Format("{0:0.00}", temp);
                                    Console.WriteLine(all_names[i] + "  " + all_surnames[i] + " " + temp);
                                }
                                break;
                            case 2:
                                Console.WriteLine("Name    Surname    Final points");
                                for (int i = 0; i < count; i++)
                                {

                                    Console.WriteLine(all_names[i] + "  " + all_surnames[i] + " " + all_Final_points_median[i]);
                                }
                                break;
                            case 3:
                                List<string> lmaos = new List<string>();
                                for (int i = 0; i < count; i++)
                                {
                                    lmaos.Add(all_names[i] + "," + all_surnames[i] + "," + all_Final_points[i] + "," + all_Final_points_median[i]);
                                }
                                Console.WriteLine(lmaos);
                                File.WriteAllLines(resultpath, lmaos);
                                break;

                        }
                        break;
                    case 3:
                        List<string> lines = new List<string>();
                        lines = File.ReadAllLines(filepath).ToList();
                        Console.WriteLine("Name    Surname    Final points(avg)    Final points(med)");
                        int counter = count;
                        foreach (string line in lines)
                        {
                            string[] items = line.Split(',');
                            name = items[0];
                            surname = items[1];
                            int[] homewrk = new int[5];
                            homewrk[0] = Int32.Parse(items[2]);
                            homewrk[1] = Int32.Parse(items[3]);
                            homewrk[2] = Int32.Parse(items[4]);
                            homewrk[3] = Int32.Parse(items[5]);
                            homewrk[4] = Int32.Parse(items[6]);
                            int hmsum = 0;
                            for (int i = 0; i < 5; i++)
                            {
                                hmsum = hmsum + homewrk[i];
                            }
                            double avghm = hmsum / 5;
                            int egzam = Int32.Parse(items[7]);
                            double fpa = 0.3 * avghm + 0.7 * egzam;

                            int halfhm = homewrk.Count() / 2;
                            var sortedhm = homewrk.OrderBy(n => n);
                            double hmmedian = 0;
                            hmmedian = sortedhm.ElementAt(halfhm);
                            double fpm = 0.3 * hmmedian + 0.7 * egzam;
                            Console.WriteLine(name + "  " + surname + "  " + fpa + "             " + fpm);
                            all_names[counter] = name;
                            all_surnames[counter] = surname;
                            all_Final_points[counter] = fpa;
                            all_Final_points_median[counter] = fpm;
                            counter = counter + 1;
                        }
                        count = counter;
                        break;
                    case 4:
                        x = true;
                        break;
                }
            }
        }
    }
}
