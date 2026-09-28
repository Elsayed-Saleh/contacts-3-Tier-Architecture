using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ContactsConsoleAppPresentationLayer
{
    internal class Program
    {
        static void testFindContact(int ContactID)
        {
            clsContact Contact1 = clsContact.Find(ContactID);

            if (Contact1 != null)
            {
                Console.WriteLine("ContactID: " + Contact1.ID);
                Console.WriteLine("First Name: " + Contact1.FirstName);
                Console.WriteLine("Last Name: "+Contact1.LastName);
                Console.WriteLine("Email: " + Contact1.Email);
                Console.WriteLine("Phone: " + Contact1.Phone);
                Console.WriteLine("Address: " + Contact1.Address);
                Console.WriteLine("Date Of Birth: " + Contact1.DateOfBirth);
                Console.WriteLine("CountryID: " + Contact1.CountryID);
                Console.WriteLine("Image Path: " + Contact1.ImagePath);

            }
            else
            {
                Console.WriteLine($"Contact ID [{ContactID}] Not Found!"); 
            }
        }

        static void testAddNewContact()
        {
            clsContact contact=new clsContact();
            contact.FirstName = "Elsayed";
            contact.LastName = "Saleh";
            contact.Email = "Elsayed@gmail.com";
            contact.Phone = "0100928392";
            contact.Address = "Address122";
            contact.DateOfBirth = new DateTime(2007, 1, 4, 12, 1, 1);
            contact.CountryID = 1;
            contact.ImagePath = "";
            if(contact.Save())
            {
                Console.WriteLine("Contact Added Successfully with ID = " + contact.ID);
            }

        }
        
        static void testUpdateContact(int ContactID)
        {
            clsContact contact = clsContact.Find(ContactID);
            if(contact != null)
            {
                contact.FirstName = "Mohammed";
                contact.LastName = "Mahmoud";
                contact.Email = "Ahmed@gmail.com";
                contact.Phone = "0120020020";
                contact.Address = "Address111";
                contact.DateOfBirth = new DateTime(2000, 1, 4, 12, 1, 1);
                contact.CountryID = 3;
                contact.ImagePath = "C:\\OneDriveTemp";
                if(contact.Save())
                {
                    Console.WriteLine("Contact Added Successfully");
                }
                
            }
            else
            {
                Console.WriteLine("Error");

            }

        }

        static void testDeleteContact(int ContactID)
        {
            if(clsContact.DeleteContact(ContactID))
            {
                Console.WriteLine("Contact deleted Successfully");
            }
            else
            {
                Console.WriteLine("Contact not deleted");
            }
        }

        static void testIsContactExist(int ContactID)
        {
            if(clsContact.IsContactExist(ContactID))
            {
                Console.WriteLine("Yes, Contacts is there.");
            }
            else
            {
                Console.WriteLine("No, Contacts Is not there");
            }
        }

        static void testFindByFirstName(string FirstName)
        {
            clsContact Contact1= clsContact.Find(FirstName);

            if (Contact1 != null)
            {
                Console.WriteLine("ContactID: " + Contact1.ID);
                Console.WriteLine("First Name: " + Contact1.FirstName);
                Console.WriteLine("Last Name: " + Contact1.LastName);
                Console.WriteLine("Email: " + Contact1.Email);
                Console.WriteLine("Phone: " + Contact1.Phone);
                Console.WriteLine("Address: " + Contact1.Address);
                Console.WriteLine("Date Of Birth: " + Contact1.DateOfBirth);
                Console.WriteLine("CountryID: " + Contact1.CountryID);
                Console.WriteLine("Image Path: " + Contact1.ImagePath);

            }
            else
            {
                Console.WriteLine($"Contact ID [{FirstName}] Not Found!");
            }
        }

        static void testUpdateCountry(string CountryName)
        {

            clsCountry country=clsCountry.Find(CountryName);

            if(country != null )
            {
                country.Name= CountryName;
                country.Code = "112";
                country.PhoneCode = "111";
                Console.WriteLine($"{country.ID}\n{country.Name}\n{country.Code}\n{country.PhoneCode}");
            }
            country.Save();

        }
        static void testFindCountry(string CountryName)
        {
            clsCountry country= clsCountry.Find(CountryName);

            Console.WriteLine($"{country.ID}\n{country.Name}\n{country.Code}\n{country.PhoneCode}");

        }

        static void ListCountry()
        {
            DataTable dataTable = new DataTable();

            dataTable = clsCountry.ListCountries();

            foreach (DataRow row in dataTable.Rows)
            {

                Console.WriteLine($"{row["CountryID"]} {row["CountryName"]} {row["Code"]} {row["PhoneCode"]}");

            }
        }

        static void AddNewCountry()
        {
            clsCountry country=new clsCountry();
            country.Name = "Egypt";
            country.Code = "101";
            country.PhoneCode = "020";
            if (country.Save())
            {
                Console.WriteLine("Country Added Successfully with ID = " + country.ID);
            }
        }

        static void ListContacts()
        {
            DataTable  dataTable = clsContact.GetAllContacts();

            Console.WriteLine("Contacts Data: \n");

            Console.WriteLine($"{"ContactID",-15}"+$"{"FirstName", -15}"+$"{"LastName", -15}");
            Console.WriteLine("\n_____________________________________________________\n");

            foreach (DataRow row in dataTable.Rows)
            {

                Console.WriteLine($"{" ", -4}"+$"{row["ContactID"],-10} {row["FirstName"], -12} {row["LastName"],-12}");

            }

        }
        static void Main(string[] args)
        {


            
            ListContacts();


        }
    }
}
