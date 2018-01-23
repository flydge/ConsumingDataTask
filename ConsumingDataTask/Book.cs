using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace ConsumingDataTask
{
    public class Book
    {
        private string _bookTitle;
        private int _pagesNumb;
        private string _authorFirstName;
        private string _authorLastName;

        public Book()
        {
        }

        public Book(string bookTitle, int pagesNumb, string authorFirstName, string authorLastName)
        {
            _bookTitle = bookTitle;
            _pagesNumb = pagesNumb;
            _authorFirstName = authorFirstName;
            _authorLastName = authorLastName;
        }


        public string BookTitle
        {
            get => _bookTitle;
            set => _bookTitle = value;
        }

        public int PagesNumb
        {
            get => _pagesNumb;
            set => _pagesNumb = value;
        }

        public string AuthorFirstName
        {
            get => _authorFirstName;
            set => _authorFirstName = value;
        }

        public string AuthorLastName
        {
            get => _authorLastName;
            set => _authorLastName = value;
        }

        public  XElement getBookXElement()
        {
            XElement bookXElement = new XElement("book",
                new XElement("bookTitle", _bookTitle),
                new XElement("pagesNumb", _pagesNumb),
                new XElement("author",
                    new XElement("authorFirstName",_authorFirstName),
                    new XElement("authorLastName",_authorLastName)));

            return bookXElement;
        }

        public Book getBookFromXMLFile(String path)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            using (XmlReader xmlReader = XmlReader.Create(path,settings))
            {
                xmlReader.MoveToContent();
                xmlReader.ReadStartElement("book");
                _bookTitle = xmlReader.ReadElementContentAsString("bookTitle", "");
                _pagesNumb = xmlReader.ReadElementContentAsInt("pagesNumb", "");
                xmlReader.ReadStartElement("author");
                _authorFirstName = xmlReader.ReadElementContentAsString("authorFirstName", "");
                _authorLastName = xmlReader.ReadElementContentAsString("authorLastName", "");
                xmlReader.ReadEndElement();
                xmlReader.ReadEndElement();
            }
            Book book = new Book(BookTitle, PagesNumb, AuthorFirstName, AuthorLastName);
            return book;
        }
        public void setXMLBookToFile(string path)
        {
            Book book = new Book(BookTitle, PagesNumb, AuthorFirstName, AuthorLastName);
            book.getBookXElement().Save(path);
        }

        public string getJSONBook()
        {
            Book book = new Book(BookTitle, PagesNumb, AuthorFirstName, AuthorLastName);
            return JsonConvert.SerializeObject(book);
        }

        public void setJSONBookToFile(string path)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                Book book = new Book(BookTitle, PagesNumb, AuthorFirstName, AuthorLastName);
                streamWriter.WriteLine(book.getJSONBook());
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


        public override string ToString()
        {
            return String.Format("Title:{0}\nnumber of pages:{1}\nauthor:{2} {3}",
                _bookTitle,_pagesNumb,_authorLastName,_authorFirstName);
        }
    }
}