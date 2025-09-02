Dictionary<string, string> contacts = new Dictionary<string, string>();

contacts.Add("Alice", "123-456-7890");
contacts.Add("Bob", "987-654-3210");
contacts.Add("Charlie", "555-555-5555");

while (true)
{
    Console.WriteLine("Choose an option:");
    Console.WriteLine("1. Add Contact");
    Console.WriteLine("2. View Contact");
    Console.WriteLine("3. Update Contact");
    Console.WriteLine("4. Delete Contact");
    Console.WriteLine("5. List All Contacts");
    Console.WriteLine("6. Exit");

    string choice = Console.ReadLine();
    switch (choice)
    {
        case "6":
            return;

        case "1":
            Console.Write("Enter name: ");
            string nameToAdd = Console.ReadLine();
            Console.Write("Enter phone number: ");
            string phoneToAdd = Console.ReadLine();
            contacts.Add(nameToAdd, phoneToAdd);
            continue;

        case "2":
            Console.Write("Enter name: ");
            string nameToSearch = Console.ReadLine();
            if (contacts.ContainsKey(nameToSearch))
            {
                Console.WriteLine($"Name: {nameToSearch}, Phone: {contacts[nameToSearch]}");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
            continue;

        case "3":
            Console.WriteLine("Enter name: ");
            string nameToUpdate = Console.ReadLine();
            if (contacts.ContainsKey(nameToUpdate))
            {
                Console.WriteLine("Enter new phone number: ");
                string newPhone = Console.ReadLine();
                contacts[nameToUpdate] = newPhone;
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
            continue;

        case "4":
            Console.WriteLine("Enter name: ");
            string nameToDelete = Console.ReadLine();
            if (contacts.ContainsKey(nameToDelete))
            {
                contacts.Remove(nameToDelete);
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
            continue;

        case "5":
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Name: {contact.Key}, Phone: {contact.Value}");
            }
            continue;
    }
}