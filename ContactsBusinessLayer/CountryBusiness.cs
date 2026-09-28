using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContactsBusinessLayer
{
    public class clsCountry
    {
        public enum enMode { AddNew=1, Update=2}
        public enMode Mode=enMode.AddNew;
        public int ID { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public string PhoneCode { get; set; }


        public clsCountry()
        {
            this.ID = -1;
            this.Name = "";
            this.Code = null;
            this.PhoneCode = null;
            Mode = enMode.AddNew;
        }
        private clsCountry(int ID, string Name, string Code, string PhoneCode)
        {
            this.ID=ID;
            this.Name = Name;
            this.Code = Code;
            this.PhoneCode = PhoneCode;
            Mode = enMode.Update;
        }

        private bool _AddNewCountry()
        {
            this.ID = clsDataCountry.AddCountry(this.Name, this.Code, this.PhoneCode);
            return (this.ID != -1);

        }

        private bool _UpdateCountry()
        {
            return clsDataCountry.UpdateCountry(this.ID, this.Name, this.Code, this.PhoneCode);
        }


        public static clsCountry Find(string Name)
        {
            int ID = -1;
            string Code = "", PhoneCode = "";
            if(clsDataCountry.GetCointryByName(Name, ref ID, ref Code, ref PhoneCode))
            {
                return new clsCountry(ID, Name, Code, PhoneCode);
            }
            else
            {
                return null;
            }

        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                    
                case enMode.Update:
                    return _UpdateCountry();
            }
            return false;
        }

        public static bool Delete(string Name)
        {
            return clsDataCountry.DeleteCountry(Name);
        }
        public static bool Delete(int ID)
        {
            return clsDataCountry.DeleteCountry(ID);
        }


        public static DataTable ListCountries()
        {
            return clsDataCountry.GetAllCountries();
        }

        public static bool IsContryExist(string CountryName)
        {
            return clsDataCountry.IsContryExist(CountryName);
        }


    }
}
