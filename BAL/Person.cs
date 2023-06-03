using System;
using System.Text;
// using System.Collections.Generic;
using System.Data;
using Rawson.DAL_Person;
using Rawson.DAL_ContactInformation;

namespace Rawson.BAL_Person
{
    public class BalPerson
    {
        public static DalPerson objDalPerson = new DalPerson();
        public static DalContactInformation objDalContactInformation = new DalContactInformation();

        public void GetPeople()
        {
            objDalPerson.GetPeople();
        }

        public void AddPerson(Person person)
        {
            DataTable dt = objDalPerson.GetPeopleByIdentityNumber(person.IdentityNumber);

            if (dt.Rows.Count > 0)
            {
                Console.WriteLine("Person existent");
            }
            else
            {
                int PersonId = 0;
                objDalPerson.AddPerson(person, out PersonId);

                foreach (var item in person.Contact_Information)
                {
                    Console.WriteLine(item.Email);
                    objDalContactInformation.AddContactInformation(item, PersonId);
                }
            }
        }
    }
}
