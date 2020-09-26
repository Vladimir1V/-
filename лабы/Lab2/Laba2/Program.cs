using System;

internal class Program
{
    public static void Main(string[] args)
    {
        Program2.MyString str = new Program2.MyString("Добро ");
        Program2.MyString str1 = new Program2.MyString("подаловать!");
        str = str + str1;
        str.Replace("од", "ож");
        Console.WriteLine(str);
        Console.ReadLine();
    }
}