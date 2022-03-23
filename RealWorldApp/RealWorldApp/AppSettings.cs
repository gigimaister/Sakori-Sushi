
using Xamarin.Essentials;
using static RealWorldApp.Constants;

namespace RealWorldApp
{
    public static class AppSettings
    {
        public static string ApiUrl2 = "http://192.168.254.83:45457";
        public static string ApiUrl = "http://192.168.43.179:45455";
    }

    public static class TraslatedMessages
    {
        // Check What Language The Device Is On And Set Alert Strings Accordinally
        static string userLanguage = Preferences.Get(Preference.DeviceLanguage, string.Empty);
        public static string Alert_Hello_Kampai()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Hello_Kampai;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Hello_Kampai;
        }
        public static string Alert_Account_Created()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Account_Created;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Account_Created;
        }
        public static string Alert_Dismiss()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Dismiss;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Dismiss;
        }
        public static string Alert_Oops()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Oops;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Oops;
        }
        public static string Alert_Something_Went_Wrong()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Something_Went_Wrong;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Something_Went_Wrong;
        }
        public static string Alert_Pwd_Missmatch()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Pwd_Missmatch;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Pwd_Missmatch;
        }
        public static string Alert_pls_Check_pwds()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_pls_Check_pwds;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_pls_Check_pwds;
        }
        public static string Alert_Default_User_Name()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Default_User_Name;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Default_User_Name;
        }
        public static string Alert_Added_Items_To_Cart()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Added_Items_To_Cart;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Added_Items_To_Cart;
        }
        public static string Alert_Cleared_Cart()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Cleared_Cart;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Cleared_Cart;
        }
        public static string Alert_No_Items_In_Cart()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_No_Items_In_Cart;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_No_Items_In_Cart;
        }
        public static class Menu
        {
            public static string Salmon()
            {
                if (userLanguage == "en") return EnglishMessages.Fishes.Salmon;
                else if (userLanguage == "rus") return "";
                else return HebrewMessages.Fishes.Salmon;
            }
            public static string Denis()
            {
                if (userLanguage == "en") return EnglishMessages.Fishes.Denis;
                else if (userLanguage == "rus") return "";
                else return HebrewMessages.Fishes.Denis;
            }
            public static string YesllowTail()
            {
                if (userLanguage == "en") return EnglishMessages.Fishes.YesllowTail;
                else if (userLanguage == "rus") return "";
                else return HebrewMessages.Fishes.YesllowTail;
            }
            public static string Cucumber()
            {
                if (userLanguage == "en") return EnglishMessages.Vegetables.Cucumber;
                else if (userLanguage == "rus") return "";
                else return HebrewMessages.Vegetables.Cucumber;
            }
            public static string Batata()
            {
                if (userLanguage == "en") return EnglishMessages.Vegetables.Batata;
                else if (userLanguage == "rus") return "";
                else return HebrewMessages.Vegetables.Batata;
            }
            public static string CreamCheese()
            {
                if (userLanguage == "en") return EnglishMessages.Vegetables.CreamCheese;
                else if (userLanguage == "rus") return "";
                else return HebrewMessages.Vegetables.CreamCheese;
            }
            public static string Carrot()
            {
                if (userLanguage == "en") return EnglishMessages.Vegetables.Carrot;
                else if (userLanguage == "rus") return "";
                else return HebrewMessages.Vegetables.Carrot;
            }
            public static string Avocado()
            {
                if (userLanguage == "en") return EnglishMessages.Vegetables.Avocado;
                else if (userLanguage == "rus") return "";
                else return HebrewMessages.Vegetables.Avocado;
            }
            public static string Irit()
            {
                if (userLanguage == "en") return EnglishMessages.Vegetables.Irit;
                else if (userLanguage == "rus") return "";
                else return HebrewMessages.Vegetables.Irit;
            }
            public static string GreenOnion()
            {
                if (userLanguage == "en") return EnglishMessages.Vegetables.GreenOnion;
                else if (userLanguage == "rus") return "";
                else return HebrewMessages.Vegetables.GreenOnion;
            }
            public static string Tamago()
            {
                if (userLanguage == "en") return EnglishMessages.Vegetables.Tamago;
                else if (userLanguage == "rus") return "";
                else return HebrewMessages.Vegetables.Tamago;
            }

        }

    }

  
}
