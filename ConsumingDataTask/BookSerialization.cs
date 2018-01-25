using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
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
        
        //serialization task
        public void SerializeWithDataContract(String path)
        {
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(Book));
                dataContractSerializer.WriteObject(stream,_book);
            }
        }

        public Book DeserializeWithDataContract(String path)
        {
            Book book;
            using (Stream stream = new FileStream(path,FileMode.Open))
            {
                DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(Book));
                book = (Book) dataContractSerializer.ReadObject(stream);
            }
            return book;
        }

        public void SerializeWithBinaryFormatter(String path)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(path,FileMode.Create))
            {
                formatter.Serialize(stream,_book);
            }
        }

        public Book DeserializeWtihBinaryFormatter(String path)
        {
            Book book;
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(path,FileMode.Open))
            {
                book = (Book) formatter.Deserialize(stream);
            }

            return book;
        }

        public void SerializationWithXMLBinary(String path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Book));
            using (StringWriter stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter,_book);
                string xmlFile = stringWriter.ToString();
                File.WriteAllText(path,xmlFile);
            }
        }

        public Book DeserializationWithXMLBinary(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Book));
            Book book;
            String xmlFile = File.ReadAllText(path);
            using (StringReader stringReader = new StringReader(xmlFile))
            {
                book = (Book) xmlSerializer.Deserialize(stringReader);
            }

            return book;
        }

        public void SerializationWithJSON(string path)
        {
            using (Stream stream = new FileStream(path,FileMode.Create))
            {
               DataContractJsonSerializer dataContractJsonSerializer =
                   new DataContractJsonSerializer(typeof(Book));
                dataContractJsonSerializer.WriteObject(stream,_book);
            }
        }

        public Book DeserializationWithJSON(String path)
        {
            Book book;
            DataContractJsonSerializer dataContractSerializer =
                new DataContractJsonSerializer(typeof(Book));
            using (Stream stream = new FileStream(path,FileMode.Open))
            {
                book = (Book) dataContractSerializer.ReadObject(stream);
            }

            return book;
        }
    }
}