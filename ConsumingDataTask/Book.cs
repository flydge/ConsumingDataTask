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

        public override string ToString()
        {
            return String.Format("Title:{0}\nnumber of pages:{1}\nauthor:{2} {3}",
                _bookTitle,_pagesNumb,_authorLastName,_authorFirstName);
        }
    }
}