using System;
using JSONDemo.Data;
using SerializingUtils.Classes;
using SerializingUtils.enums;

// using JSONUtils.Data;

namespace JSONDemo
{
    class Program
    {
        private static Serializer _XMlConverter = new Serializer();

        private static object _customer = new object();
        
        static void Main(string[] args)
        {
            _printMenu(true);
        }

        /// <summary>
        /// Print het keuze menu voor de gebruiker
        /// </summary>
        /// <param name="clearScreen"></param>
        private static void _printMenu(bool clearScreen = false)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (clearScreen) Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Read 'customer.xml' file to 'Customer' object"); //deserialize
            Console.WriteLine("2) Build 'newCustomer.xml' file from 'Customer' object - Generic"); //serialize
            Console.WriteLine("3) Build 'newCustomer.json' file from 'Customer' object - Generic");
            Console.Write("\r\nSelect an option: ");

            // wahct op input van de gebuiker: ReadLine()
            switch (Console.ReadLine())
            {
              
                case "1":
                    {
                        _readObjectSettings<Customer>("customer.xml", @"c:\VivesTestFiles");
                        _printMenu();
                        break;
                    }
             
                case "2":
                    {
                        _writeObjectSettings((Customer)_customer,"newCustomer.xml", FileType.XML);
                        _printMenu();
                        break;
                    }
                case "3":
                    {
                        _writeObjectSettings((Customer)_customer, "newCustomer.JSON", FileType.JSON);
                        _printMenu();
                        break;
                    }
                default:
                    {
                        _printMenu();
                        break;
                    }
            }
        }

       

        /// <summary>
        /// Generiek methode om een object te maken uit een json bestand.
        /// Maakt gebruik van de DemoNetCoreJSONConverter class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="folderPath"></param>
        private static void _readObjectSettings<T>(string fileName, string folderPath)
        {
            var result = _XMlConverter.DesirializeObjectFromXML<T>(folderPath, fileName);
            if (result.Status == ConverterStatus.HasError)
            {
                _printMessage(result.Error.Message.ToString() , ConsoleColor.Red);
            }
            else
            {
                _customer = result.ReturnValue;
                Console.WriteLine("New person has been added");
                _printCustomer((Customer)_customer);
            }

            
        }

        /// <summary>
        /// Generieke methode om een object te serialiseren en weg te schrijven naar een json bestand
        /// Maakt gebruik van de DemoNetCoreJSONConverter class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToSerialize"></param>
        /// <param name="fileName"></param>
        /// <param name="absolutefolderPath"></param>
        private static void _writeObjectSettings<T>(T objectToSerialize, string fileName, FileType f, string absolutefolderPath = @"c:\VivesTestFiles")
        {
            Type objectType = typeof(T);
            fileName = (fileName == null) ? $"{ objectType.Name }.xml" : fileName;
            var result = _XMlConverter.Serialize<T>(absolutefolderPath, fileName, objectToSerialize, f);
            if (result.Status == ConverterStatus.HasError)
            {
                _printErrorMessage(result.Error.Message);
                return;
            }
            _printSuccessMessage($"Object of type \" { objectType.Name  } \" was serialized to file {fileName}");
        }


        private static void _printInfoMessage(string message)
        {
            _printMessage(message, ConsoleColor.Cyan);
        }
        private static void _printSuccessMessage(string message)
        {
            _printMessage(message, ConsoleColor.Green);
        }
        private static void _printErrorMessage(string message)
        {
            _printMessage(message, ConsoleColor.Red);
        }

        private static void _printMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void _printCustomer(Customer c)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Name: " + c.Name + ", " + c.FirstName);
            Console.WriteLine("E-mail address: " + c.EmailAddress);
            Console.WriteLine("Phone number: " + c.PhoneNumber);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
