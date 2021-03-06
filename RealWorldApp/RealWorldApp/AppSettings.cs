
using Xamarin.Essentials;
using static RealWorldApp.Constants;

namespace RealWorldApp
{
    public static class AppSettings
    {
        public static string ApiUrl2 = "http://192.168.254.83:45457";
        public static string ApiUrl = "http://192.168.43.159:45455";
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

        public static string Alert_Product_Detail_Validator(int maxSideDishCount)
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Product_Detail_Validator(maxSideDishCount);
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Product_Detail_Validator(maxSideDishCount);
        }
        public static string Alert_No_SideDish_Selected()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_No_SideDish_Selected;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_No_SideDish_Selected;
        }
        public static string Alert_Delete_All_Products()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Delete_All_Products;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Delete_All_Products;
        }
        public static string Alert_Yes()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Yes;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Yes;
        }
        public static string Alert_No()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_No;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_No;
        }
        public static string Alert_Delete_Product()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Delete_Product;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Delete_Product;
        }
        public static string Alert_Product_Was_Clear()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_Product_Was_Clear;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_Product_Was_Clear;
        }
        public static string Alert_No_Main_Course()
        {
            if (userLanguage == "en") return EnglishMessages.Alert_No_Main_Course;
            else if (userLanguage == "rus") return "";
            else return HebrewMessages.Alert_No_Main_Course;
        }
    }

  
}
