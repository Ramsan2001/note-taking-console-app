Console.Title = "Note-Taking App";

static void GetAllNotes(string fileName, out string line, out int index)
{
    Console.Clear();
    line = "";
    index = 0;
    using (StreamReader reader = new StreamReader(fileName))
    {
        Console.WriteLine("All notes:");
        while ((line = reader.ReadLine()) != null) { Console.WriteLine(++index + ". " + line); }
    }
}

string fileName = "notes.txt";

while (true)
{
    Console.WriteLine();
    Console.WriteLine("What would you like to do?");
    Console.WriteLine("[1] Add a note");
    Console.WriteLine("[2] View all notes");
    Console.WriteLine("[3] Delete notes");
    Console.WriteLine("[4] Exit");

    switch (Console.ReadLine())
    {
        case "1":
            //Add a note
            Console.Clear();
            Console.WriteLine("Enter your note:");
            string note = Console.ReadLine();

            if (note.Length == 0 || note == null)
            {
                Console.Clear();
                Console.WriteLine("Empty note!");
                break;
            }

            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Append);
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(note);
                    Console.Clear();
                    Console.WriteLine("Note added!");
                }
            }
            finally { if (fs != null) fs.Dispose(); }
            break;
        case "2":
            //View all notes
            string line;
            int index;

            if (File.ReadAllLines(fileName).Count() == 0)
            {
                Console.Clear();
                Console.WriteLine("No notes yet!");
                break;
            }
            
            GetAllNotes(fileName, out line, out index);
            break;
        case "3":
            //Delete notes
            int noteCount = File.ReadAllLines(fileName).Count();
            if (noteCount == 0)
            {
                Console.Clear();
                Console.WriteLine("No notes yet!");
                break;
            }

            Console.Clear();
            index = 0;
            GetAllNotes(fileName, out line, out index);

            Console.WriteLine("Choose [#] of note you want to delete ('0' to delete all - '100' to go back):");
            int deletionChoice = int.Parse(Console.ReadLine());

            if (deletionChoice <= noteCount && deletionChoice != 0 && deletionChoice != 00)
            {
                List<string> linesList = File.ReadAllLines(fileName).ToList();
                linesList.RemoveAt(deletionChoice - 1);

                File.WriteAllLines(fileName, linesList.ToArray());

                Console.Clear();
                Console.WriteLine("Note deleted!");
            }
            else if (deletionChoice == 0)
            {
                File.WriteAllText(fileName, string.Empty);

                Console.Clear();
                Console.WriteLine("File cleared!");
            }
            else if (deletionChoice == 100)
            {
                Console.Clear();
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid number! You only have " + noteCount + " note/s.");
            }
            break;
        case "4":
            //Exit
            Environment.Exit(0);
            break;
        default:
            Console.Clear();
            break;
    }
}