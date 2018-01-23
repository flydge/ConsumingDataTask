using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ConsumingDataTask
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const string path = @"C:\Users\thunderFlydgePC\RiderProjects\ConsumingDataTask\";
            const string xmlFilePath = "books.xml";
            const string jsonFilePath = "books.JSON";
            Book book1 = new Book("First Heretic",400,"Aaron","Dembski-Bowden");
            Book book2 = new Book("False Gods", 300, "Graham", "McNeill");

            book1.setJSONBookToFile(path+jsonFilePath);
            Book book11 = new Book().getBookFromJSONFile(path+jsonFilePath);
            Console.WriteLine(book11);
            
            book2.setXMLBookToFile(path+xmlFilePath);  
            Book book22 = new Book().getBookFromXMLFile(path+xmlFilePath);
            Console.WriteLine("\n" + book22);
            
            
        }
    }
}