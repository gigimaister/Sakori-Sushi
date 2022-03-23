using System.Collections.Generic;

namespace RealWorldApp
{
    public static class Constants
    {
        // For Every 'Preferences' Loacal Storage Device Hard Coded Strings
        public static class Preference
        {
            public const string AccessToken = "accessToken";
            public const string UserId = "acceuserIdssToken";
            public const string UserName = "userName";
            public const string TokenExpirationTime = "tokenExpirationTime";
            public const string CurrentTime = "currentTime";
            public const string Email = "email";
            public const string Password = "password";
            public const string DeviceLanguage = "deviceLanguage";
        }

        /// <summary>
        /// For Every Product That We Need/Want To Add User 
        /// Selection(type of fish/veg selection and so on...)
        /// Get String Translated By Device Language Settings
        /// </summary>
        public static class MenuSelection
        {
            // Fishes Selection
            public static  List<string> Fishes()
            {
                var fishList = new List<string>
                {
                    TraslatedMessages.Menu.Salmon(),
                    TraslatedMessages.Menu.Denis(),
                    TraslatedMessages.Menu.YesllowTail()
                };
                return fishList;

            }

            // Vegetables Selction
            public static List<string> Vegetables()
            {
                var vegetablesList = new List<string>
                {
                    TraslatedMessages.Menu.Cucumber(),
                    TraslatedMessages.Menu.Batata(),
                    TraslatedMessages.Menu.CreamCheese(),
                    TraslatedMessages.Menu.Carrot(),
                    TraslatedMessages.Menu.Avocado(),
                    TraslatedMessages.Menu.Irit(),
                    TraslatedMessages.Menu.GreenOnion(),
                    TraslatedMessages.Menu.Tamago(),
                };
                return vegetablesList;

            }
        }

        // For Every String Get Translate To Eng/Heb/Rus
        #region Languages
        public static class EnglishMessages
        {
            public static string Alert_Hello_Kampai = "Kampai!!";
            public static string Alert_Account_Created = "Your Account Has Been Created (:";
            public static string Alert_Dismiss = "Ok";
            public static string Alert_Oops = "Oops...";
            public static string Alert_Something_Went_Wrong = "Something Went Wrong...";
            public static string Alert_Pwd_Missmatch = "The Psswords Doesn't Match!";
            public static string Alert_pls_Check_pwds = "Please Make Sure The Passwords Match!";
            public static string Alert_Default_User_Name = "Guest";
            public static string Alert_Added_Items_To_Cart = "Your Items Has Been Added To The Cart!";
            public static string Alert_Cleared_Cart = "Your Cart Has Been Cleared";
            public static string Alert_No_Items_In_Cart = "No Items In The Cart";

            public static class Fishes
            {
                public static string Salmon = "Salmon";
                public static string Denis = "Denis";
                public static string YesllowTail = "Yellow-Tail";
            }
            public static class Vegetables
            {
                public static string Cucumber = "Cucumber";
                public static string Batata = "Sweet potato";
                public static string CreamCheese = "Cream Cheese";
                public static string Carrot = "Carrot";
                public static string Avocado = "Avocado";
                public static string Irit = "Chives";
                public static string GreenOnion = "Green onion";
                public static string Tamago = "Tamago";
            }
        }
        public static class HebrewMessages
        {
            public static string Alert_Hello_Kampai = "קמפאי!!";
            public static string Alert_Account_Created = "החשבון שלך נוצר בהצלחה (:";
            public static string Alert_Dismiss = "בסדר";
            public static string Alert_Oops = "אופס...";
            public static string Alert_Something_Went_Wrong = "משהו השתבש...";
            public static string Alert_Pwd_Missmatch = "סיסמאות לא תואמות!";
            public static string Alert_pls_Check_pwds = "נא לבדוק שהסיסמאות תואמות!";
            public static string Alert_Default_User_Name = "אורח/ת";
            public static string Alert_Added_Items_To_Cart = "הפריט נוסף לעגלה";
            public static string Alert_Cleared_Cart = "כל הפריטים הוסרו מהעגלה";
            public static string Alert_No_Items_In_Cart = "אין פריטים בעגלה";
            public static class Fishes
            {
                public static string Salmon = "סלמון";
                public static string Denis = "דניס";
                public static string YesllowTail = "יילו-טייל";
            }
            public static class Vegetables
            {
                public static string Cucumber = "מלפפון";
                public static string Batata = "בטטה";
                public static string CreamCheese = "גבינת שמנת";
                public static string Carrot = "גזר";
                public static string Avocado = "אבוקדו";
                public static string Irit = "עירית";
                public static string GreenOnion = "בצל ירוק";
                public static string Tamago = "טאמגו";
            }
        }
        public static class RussionMessages
        {
            public static string Alert_Hello_Kampai = "קמפאי!!";
            public static string Alert_Account_Created = "החשבון שלך נוצר בהצלחה (:";
            public static string Alert_Dismiss = "בסדר";
            public static string Alert_Oops = "אופס...";
            public static string Alert_Something_Went_Wrong = "משהו השתבש...";
            public static string Alert_Pwd_Missmatch = "סיסמאות לא תואמות!";
            public static string Alert_pls_Check_pwds = "נא לבדוק שהסיסמאות תואמות!";
            public static string Alert_Default_User_Name = "אורח/ת";
            public static class Fishes
            {
                public static string Sakmon = "Salmon";
                public static string Denis = "Denis";
                public static string YesllowTail = "Yellow-Tail";
            }
        }
        #endregion

    }
}
