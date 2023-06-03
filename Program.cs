using System;
using System.Text;
using Rawson.BAL_Person;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Presentation
{
    public class Program
    {
        public static BalPerson objBalPerson = new BalPerson();

        public static List<ContactInformation> lst_ContactInformation =
            new List<ContactInformation>();

        static void Main(string[] args)
        {
            string selected = String.Empty;
            int valselected = 0;
            bool option = false;

            Console.WriteLine("Hello, Rawson!");
            Console.WriteLine("Which option do you want to choose!");
            Console.WriteLine("1. Get person");
            Console.WriteLine("2. Add person");
            Console.WriteLine("Write only the selected number");

            while (!option)
            {
                selected = Console.ReadLine();
                while (!int.TryParse(selected, out valselected))
                {
                    Console.WriteLine("Type valid option, Please.");
                    selected = Console.ReadLine();
                }

                switch (valselected)
                {
                    case 1:
                        GetPeople();
                        option = true;
                        break;
                    case 2:
                        AddPerson();
                        option = true;
                        break;
                    default:
                        Console.WriteLine("Validate option selected, Please.");
                        option = false;
                        break;
                }
            }
        }

        public static void GetPeople()
        {
            objBalPerson.GetPeople();
        }

        public static bool AddPerson()
        {
            string iden_Num;
            string names;
            string surnames;
            string dateBirth;
            string selected = String.Empty;
            bool option = false;
            string email,
                address,
                phoneNumber;

            DateTime valdateBirth = new DateTime();
            Console.WriteLine("Write identity number:");
            iden_Num = Console.ReadLine();
            while (ValidateAlphanumericRequired(iden_Num, "validate identity number"))
            {
                iden_Num = Console.ReadLine();
            }

            Console.WriteLine("Write names:");
            names = Console.ReadLine();
            while (ValidateLatinAlphabetRequired(names, "validate names"))
            {
                names = Console.ReadLine();
            }

            Console.WriteLine("Write surnames:");
            surnames = Console.ReadLine();

            while (ValidateLatinAlphabetRequired(surnames, "validate surnames"))
            {
                surnames = Console.ReadLine();
            }

            Console.WriteLine("Write dateBirth (yyyy/MM/dd):");
            dateBirth = Console.ReadLine();
            while (ValidateDate(dateBirth, out valdateBirth, "validate dateBirth"))
            {
                dateBirth = Console.ReadLine();
            }

            Console.WriteLine("How many registers of contact information do you want add?>: 1/2 ");
            while (!option)
            {
                selected = Console.ReadLine();
                if (!(selected.Trim() == "1" || selected.Trim() == "2"))
                {
                    Console.WriteLine("Type valid option, Please.");
                }
                else
                {
                    option = true;
                }
            }

            for (int i = 1; i <= Convert.ToInt32(selected); )
            {
                Console.WriteLine("Write email " + i + ":");
                email = Console.ReadLine();
                Console.WriteLine("Write address " + i + ":");
                address = Console.ReadLine();
                Console.WriteLine("Write phone number " + i + ":");
                phoneNumber = Console.ReadLine();

                if (
                    email.Trim().Length > 0
                    || address.Trim().Length > 0
                    || phoneNumber.Trim().Length > 0
                )
                {
                    lst_ContactInformation.Add(
                        new ContactInformation
                        {
                            Email = email,
                            Address = address,
                            PhoneNumber = phoneNumber
                        }
                    );
                    i++;
                }
                else
                {
                    Console.WriteLine("I need some information, please:");
                }
            }
            Person objPerson = new Person {
                IdentityNumber = iden_Num,
                Names = names,
                Surnames = surnames,
                DateBirth = valdateBirth,
                Contact_Information = lst_ContactInformation
            };

            objBalPerson.AddPerson(objPerson);
            return true;
        }

        public static bool ValidateAlphanumericRequired(string option, string message)
        {
            if (!(option.Trim().Length > 0))
            {
                Console.WriteLine("Invalid information, please " + message + ". Required field ");
                return true;
            }
            if (!Regex.IsMatch(option, "^[a-zA-Z0-9]*$"))
            {
                Console.WriteLine("Invalid information, please " + message + ". Non-alphanumeric ");
                return true;
            }
            return false;
        }

        public static bool ValidateLatinAlphabetRequired(string option, string message)
        {
            if (!(option.Trim().Length > 0))
            {
                Console.WriteLine("Invalid information, please " + message + ". Required field ");
                return true;
            }
            if (!Regex.IsMatch(option, "^[a-zA-Z Á-Úá-ú]*$"))
            {
                Console.WriteLine(
                    "Invalid information, please "
                        + message
                        + ". Non-latin Alphabet or numbers on text"
                );
                return true;
            }
            return false;
        }

        public static bool ValidateDate(string option, out DateTime valdateBirth, string message)
        {
            valdateBirth = new DateTime();
            if (!(option.Trim().Length > 0))
            {
                Console.WriteLine("Invalid information, please " + message + ". Required field ");
                return true;
            }
            if (
                !DateTime.TryParseExact(
                    option,
                    "yyyy/MM/dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out valdateBirth
                )
            )
            {
                Console.WriteLine(
                    "Invalid information, please " + message + ". Non-date or non-format"
                );
                return true;
            }

            return false;
        }
    }
}
