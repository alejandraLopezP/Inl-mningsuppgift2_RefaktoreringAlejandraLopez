using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;
using System.Text;
using System.Threading.Tasks;

namespace Inlamning_2_ra_kod
{

    /* CLASS: Person
    * PURPOSE: Create a new Contact in the Address Book
    */
    class Person
    {
        public string _name, _address, _phone, _email;

        /* METHOD: class.ReadListFile (static) 
         * PURPOSE: READ ALL FILES (.txt)
         * PARAMETERS: THE FILE TO READ: fileName
         * RETURN VALUE: SHOW THE FILE CONTENT
         */
        public Person(string name, string address, string phone, string email)
        {
            _name = name; _address = address; _phone = phone; _email = email;
        }

        /* METHOD: class.ReadListFile (static) 
         * PURPOSE: READ ALL FILES (.txt)
         * PARAMETERS: THE FILE TO READ: fileName
         * RETURN VALUE: SHOW THE FILE CONTENT
         */
        public Person()
        {
            Write("Introduce a contact NAME: ");
            _name = ReadLine().ToUpper();
            Write("Introduce a contact ADDRESS: ");
            _address = ReadLine().ToUpper();
            Write("Introduce a contact PHONE: ");
            _phone = ReadLine().ToUpper();
            Write("Introduce a contact EMAIL: ");
            _email = ReadLine().ToUpper();
        }

        /* METHOD: class.ReadListFile (static) 
         * PURPOSE: READ ALL FILES (.txt)
         * PARAMETERS: THE FILE TO READ: fileName
         * RETURN VALUE: SHOW THE FILE CONTENT
         */
        public Person(string fields, string values)
        {
            _name = "";
            _address = "";
            _phone = "";
            _email = "";

            string[] fieldsList = fields.Split(", ");
            string[] valueList = values.Split(", ");

            if(fieldsList.Length == valueList.Length)
            {
                for (int i = 0; i < fieldsList.Length; i++)
                {
                    if (fieldsList[i].ToUpper() == "NAME")
                        _name = valueList[i];
                    else if (fieldsList[i].ToUpper() == "ADDRESS")
                        _address = valueList[i];
                    else if (fieldsList[i].ToUpper() == "PHONE")
                        _phone = valueList[i];
                    else if (fieldsList[i].ToUpper() == "EMAIL")
                        _email = valueList[i];
                    else
                        WriteLine("Undefinded field " + fieldsList[i].ToUpper());
                }
            }
        }

        /* METHOD: class.ReadListFile (static) 
         * PURPOSE: READ ALL FILES (.txt)
         * PARAMETERS: THE FILE TO READ: fileName
         * RETURN VALUE: SHOW THE FILE CONTENT
         */
        public void Print()
        {
            WriteLine("{0}, {1}, {2}, {3}", _name, _address, _phone, _email);
        }
    }

     /* CLASS: Program
      * PURPOSE: Create a new Contact in the Address Book
      */
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = @".\addressList.txt";
            List<Person> personList = ReadListToFile(fileName);

            WriteLine("Welcome to the Address Book!");
            WriteLine("Write 'quit' for to finish the program!");
            string command;
            do
            {
                Write("> ");
                command = ReadLine();
                if (command == "quit")
                {
                    WriteLine("Bye bye!");
                }
                else if (command == "new")
                {
                    Person person = new Person();
                    personList.Add(person);
                }
                else if (command == "delete")
                {
                    Write("Wich contact do you want delete? (introduce the Name): ");
                    string deleteContact = ReadLine().ToUpper();
                    DeleteContact(personList, deleteContact);
                }
                else if (command == "show")
                {
                    for (int i = 0; i < personList.Count(); i++)
                    {
                        Person person = personList[i];
                        person.Print();
                    }
                }
                else if (command == "change")
                {
                    Write("Wich contact do you want change? (introduce the Name): ");
                    string changeContact = ReadLine().ToUpper();
                    ChangeContact(personList, changeContact);
                }
                else
                {
                    WriteLine($"Undefined command: {command}");
                }
            } while (command != "quit");
        }

        /* METHOD: class.ReadListFile (static) 
         * PURPOSE: READ ALL FILES (.txt)
         * PARAMETERS: THE FILE TO READ: fileName
         * RETURN VALUE: SHOW THE FILE CONTENT
         */
        public static List<Person> ReadListToFile(string fileName)
        {

            List<Person> people = new List<Person>();
            Write("Load people list ... ");
            using (StreamReader fileAddress = new StreamReader(fileName))
            {
                while (fileAddress.Peek() >= 0)
                {
                    string line = fileAddress.ReadLine();
                    string[] textIntroduced = line.Split('#');
                    Person person = new Person(textIntroduced[0], textIntroduced[1], textIntroduced[2], textIntroduced[3]);
                    people.Add(person);
                }
            }
            WriteLine("Done, list loaded!");
            return people;
        }

        /* METHOD: class.GetIndexToListPerson (static) 
          * PURPOSE: FIND THE CONTACT INDEX
          * PARAMETERS: PEOPLE LIST AND VARIABEL changeContact THAT STORE THE NAME OF PERSON TO FIND
          * RETURN VALUE: THE PERSON INDEX AND IF CONTACT DOESN´T EXIST, RETURN -1
          */
        public static int GetIndexToListPerson(List<Person> people, string changeContact)
        {
            int index = -1;
            for (int i = 0; i < people.Count(); i++)
            {
                if (people[i]._name == changeContact) index = i;
            }

            return index;
        }

        /* METHOD: class.ReadListFile (static) 
         * PURPOSE: READ ALL FILES (.txt)
         * PARAMETERS: THE FILE TO READ: fileName
         * RETURN VALUE: SHOW THE FILE CONTENT
         */
        public static void ChangeContactInfoByIndex(List<Person> people, int index)
        {
            WriteLine("Introduce the fields of the Contact that you want modified, like this: (name, address, phone, email) ");
            string fields =  ReadLine();

            Write("Introduce the new values: (name, address, phone, email): ");
            string values = ReadLine().ToUpper();
            Person newPerson = new Person(fields, values);
            people[index]._name = newPerson._name == "" ? people[index]._name : newPerson._name;
            people[index]._address = newPerson._address == "" ? people[index]._address : newPerson._address;
            people[index]._phone = newPerson._phone == "" ? people[index]._phone : newPerson._phone;
            people[index]._email = newPerson._email == "" ? people[index]._email : newPerson._email;
        }

        /* METHOD: class.ReadListFile (static) 
         * PURPOSE: READ ALL FILES (.txt)
         * PARAMETERS: THE FILE TO READ: fileName
         * RETURN VALUE: SHOW THE FILE CONTENT
         */
        public static void ChangeContact(List<Person> people, string changeContact)
        {
            int indexPerson = GetIndexToListPerson(people, changeContact);
            if (indexPerson == -1)
                WriteLine($"Unluckely: {changeContact} doesn´t exist in the Address Book :(");
            else
            {
                WriteLine($"This contact {changeContact} ");
                people[indexPerson].Print();
                if(ConfirmationAction("Update"))
                    ChangeContactInfoByIndex(people, indexPerson);
            }
        }

        /* METHOD: class.ReadListFile (static) 
         * PURPOSE: READ ALL FILES (.txt)
         * PARAMETERS: THE FILE TO READ: fileName
         * RETURN VALUE: SHOW THE FILE CONTENT
         */
        public static void DeleteContact(List<Person> people, string deleteContact)
        {
            int indexPerson = GetIndexToListPerson(people, deleteContact);
            if (indexPerson == -1)
                WriteLine($"Unluckely: {deleteContact} doesn´t exist in the Address Book :(");
            else
                people.RemoveAt(indexPerson);
        }

        /* METHOD: class.ReadListFile (static) 
         * PURPOSE: READ ALL FILES (.txt)
         * PARAMETERS: THE FILE TO READ: fileName
         * RETURN VALUE: SHOW THE FILE CONTENT
         */
        public static bool ConfirmationAction(string action)
        {
            WriteLine("Are you sure that you want " + action + " Y/N");
            string answerAction = ReadLine().ToUpper();
            if (answerAction == "Y" )
            {
                return true;
            }
            WriteLine(action + " Cancelled!");
            return false;
        }
    }
}
