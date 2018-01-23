using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace ConsumingDataTask
{
    public class BookSerialization
    {
        private Book _book;

        public BookSerialization()
        {
        }

        public BookSerialization(Book book)
        {
            _book = book;
        }
        

        public  XElement getBookXElement()
        {
            XElement bookXElement = new XElement("book",
                new XElement("bookTitle", _book.BookTitle),
                new XElement("pagesNumb", _book.PagesNumb),
                new XElement("author",
                    new XElement("authorFirstName",_book.AuthorFirstName),
                    new XElement("authorLastName",_book.AuthorLastName)));

            return bookXElement;
        }
        
        public void setXMLBookToFile(string path)
        {
            getBookXElement().Save(path);
        }
        
        public Book getBookFromXMLFile(String path)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            Book book = new Book();
            using (XmlReader xmlReader = XmlReader.Create(path,settings))
            {
                xmlReader.MoveToContent();
                xmlReader.ReadStartElement("book");
                book.BookTitle = xmlReader.ReadElementContentAsString("bookTitle", "");
                book.PagesNumb = xmlReader.ReadElementContentAsInt("pagesNumb", "");
                xmlReader.ReadStartElement("author");
                book.AuthorFirstName = xmlReader.ReadElementContentAsString("authorFirstName", "");
                book.AuthorLastName = xmlReader.ReadElementContentAsString("authorLastName", "");
                xmlReader.ReadEndElement();
                xmlReader.ReadEndElement();
            }
          
            return book;
        }
       
        public string getJSONBook()
        {
            return JsonConvert.SerializeObject(_book);
        }

        public void setJSONBookToFile(string path)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine(getJSONBook());
            }
        }

        public Book getBookFromJSONFile(string path)
        {
            Book deserializedBook  = new Book();
            using (StreamReader streamReader = new StreamReader(path))
            {
                string bookJson = "";
                while ((bookJson = streamReader.ReadLine())!=null)
                {
                    deserializedBook = JsonConvert.DeserializeObject<Book>(bookJson);
                }
            }
            return deserializedBook;
        }
    }
}