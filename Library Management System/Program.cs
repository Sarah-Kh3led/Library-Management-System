using Library_Management_System;
using System.IO;

string choice = "";
List<Book> books = new List<Book>();
int nextId = 1;

if (File.Exists("books.txt"))
{
    foreach (string line in File.ReadAllLines("books.txt"))
    {
        string[] data = line.Split('|');
        Book book = new Book(int.Parse(data[0]), data[1], data[2]);
        book.isAvailable = bool.Parse(data[3]);
        books.Add(book);
        if (book.ID >= nextId)
        {
            nextId = book.ID + 1;
        }
    }
}

while (choice != "8")
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n===== Library Management System =====");
    Console.ResetColor();
    Console.WriteLine("1. Add Book");
    Console.WriteLine("2. View All Books");
    Console.WriteLine("3. Search Book");
    Console.WriteLine("4. Delete Book");
    Console.WriteLine("5. Borrow Book");
    Console.WriteLine("6. Return Book");
    Console.WriteLine("7. Edit Book");
    Console.WriteLine("8. Exit");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Enter your choice: ");
    Console.ResetColor();
    choice = Console.ReadLine() ?? "";

    if (choice == "1")
    {
        Console.Write("Enter book title: ");
        string title = Console.ReadLine() ?? "";
        Console.Write("Enter author name: ");
        string author = Console.ReadLine() ?? "";
        Book newBook = new Book(nextId, title, author);
        books.Add(newBook);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Book added successfully!");
        Console.ResetColor();
        Console.WriteLine("Book ID: " + nextId);
        nextId++;
    }
    else if (choice == "2")
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books available.");
        }
        else
        {
            Console.WriteLine("Avaliable Books Are:");
            foreach (Book book in books)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("ID: " + book.ID);
                Console.WriteLine("Title: " + book.Title);
                Console.WriteLine("Author: " + book.Author);
                Console.WriteLine("Available: " + book.isAvailable);
            }
        }
    }

    else if (choice == "3")
    {
        Console.Write("Enter Book Title: ");
        string title = Console.ReadLine() ?? "";
        List<Book> foundBooks = books.FindAll(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

        if (foundBooks.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (Book book in foundBooks)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("ID: " + book.ID);
                Console.WriteLine("Title: " + book.Title);
                Console.WriteLine("Author: " + book.Author);
                Console.WriteLine("Available: " + (book.isAvailable ? "Yes" : "No"));
            }

            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Book not found!");
            Console.ResetColor();
        }
    }
    else if (choice == "4")
    {
        Console.Write("Enter Book Title to Delete: ");
        string title = Console.ReadLine() ?? "";
        Book? book = books.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (book == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Book not found!");
            Console.ResetColor();
        }
        else
        {
            books.Remove(book);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book deleted successfully!");
            Console.ResetColor();
        }
    }
    else if (choice == "5")
    {
        Console.Write("Enter Book Title to Borrow: ");
        string title = Console.ReadLine() ?? "";
        Book? book = books.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        if (book == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Book not found!");
            Console.ResetColor();
        }
        else if (!book.isAvailable)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("This book is already borrowed!");
            Console.ResetColor();
        }
        else
        {
            book.isAvailable = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book borrowed successfully!");
            Console.ResetColor();
        }
    }
    else if (choice == "6")
    {
        Console.Write("Enter Book Title to Return: ");
        string title = Console.ReadLine() ?? "";
        Book? book = books.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        if (book == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Book not found!");
            Console.ResetColor();
        }
        else if (book.isAvailable)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("This book is not borrowed!");
            Console.ResetColor();
        }
        else
        {
            book.isAvailable = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book returned successfully!");
            Console.ResetColor();
        }
    }
    else if(choice == "7")
    {
        Console.Write("Enter the current book title: ");
        string title = Console.ReadLine() ?? "";
        Book? book = books.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        if (book == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Book not found!");
            Console.ResetColor();
        }
        else
        {
            Console.Write("Enter new title: ");
            string newTitle = Console.ReadLine() ?? "";
            Console.Write("Enter new author: ");
            string newAuthor = Console.ReadLine() ?? "";
            book.Title = newTitle;
            book.Author = newAuthor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book updated successfully!");
            Console.ResetColor();
        }

    }
    else if (choice == "8")
    {
        List<string> lines = new List<string>();
        foreach (Book book in books)
        {
            lines.Add($"{book.ID}|{book.Title}|{book.Author}|{book.isAvailable}");
        }
        File.WriteAllLines("books.txt", lines);
        Console.WriteLine("Books saved successfully!");
        Console.WriteLine("Thanks For Using Our Library System ");
        Environment.Exit(0);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid choice!. Please try again.");
        Console.ResetColor();
    }
}
Environment.Exit(0);