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
            const string xmlFilePathWithDataContract = "booksWithDC.xml";
            const string xmlFilePathWithXMLBinary = "booksWitXMLbin.xml";
            const string binFilePathWithBinaryFormatter = "booksWithBF.bin";
            const string jsonFilePath = "books.JSON";
            const string jsonFilePathWithJSONSer = "booksWithJSONser.JSON";
            Book book1 = new Book("First Heretic",400,"Aaron","Dembski-Bowden");
            Book book2 = new Book("False Gods", 300, "Graham", "McNeill");

            BookSerialization bookSerialization = new BookSerialization(book1);
            bookSerialization.setJSONBookToFile(path+jsonFilePath);
            Book book11 = new BookSerialization().getBookFromJSONFile(path + jsonFilePath);
            Console.WriteLine(book11);
            
            bookSerialization.setXMLBookToFile(path+xmlFilePath);
            Book book22 = new BookSerialization().getBookFromXMLFile(path+xmlFilePath);
            Console.WriteLine("\n" + book22);
            
            
            //Serialization task
            bookSerialization.SerializeWithDataContract(path+xmlFilePathWithDataContract);
            Book bookAfterDataContractSerialization = new BookSerialization(book1)
                .DeserializeWithDataContract(path+xmlFilePathWithDataContract);
            Console.WriteLine("\nbookAfterDataContractSerialization:\n" 
                              + bookAfterDataContractSerialization);
            
            bookSerialization.SerializeWithBinaryFormatter(path+binFilePathWithBinaryFormatter);
            Book bookAfterBinarySerialization = new BookSerialization(book1)
                .DeserializeWtihBinaryFormatter(path + binFilePathWithBinaryFormatter);
            Console.WriteLine("\nbookAfterBinarySerialization:\n" + bookAfterBinarySerialization);
            
            bookSerialization.SerializationWithXMLBinary(path + xmlFilePathWithXMLBinary);
            Book bookAfterXMLBinarySerialization = new BookSerialization(book1)
                .DeserializationWithXMLBinary(path + xmlFilePathWithXMLBinary);
            Console.WriteLine("\nbookAfterXMLBinarySerialization:\n" + bookAfterXMLBinarySerialization);

            bookSerialization.SerializationWithJSON(path + jsonFilePathWithJSONSer);
            Book bookAfterJSONSerialization =
                new BookSerialization(book1).DeserializationWithJSON(path + jsonFilePathWithJSONSer);
            Console.WriteLine("\nbookAfterJSONSerialization:\n" + bookAfterJSONSerialization);

        }
    }
}