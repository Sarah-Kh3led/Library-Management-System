using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System
{
    internal class Book
    {
            public int ID;
            public string Title;
            public string Author;
            public bool isAvailable;
            public Book(int ID, string Title, string Author)
            {
                this.ID = ID;
                this.Title = Title;
                this.Author = Author;
                this.isAvailable = true;
            }
    }
}
