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
            public static string Menu_SideDishes = "Side Dishes:";
            public static string Alert_No_SideDish_Selected = "Sorry But You Didn't Choose Any Side Dish..";
            public static string Alert_Product_Detail_Validator(int maxsideDish) 
            {
                return $"You Must Select At least {maxsideDish} Side Dishes!";
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
            public static string Menu_SideDishes = "תוספות שנבחרו:";
            public static string Alert_No_SideDish_Selected = "לא בקטע רע...כן? אבל לא נבחרו תוספות ):";
            public static string Alert_Product_Detail_Validator(int maxsideDish)
            {
                return $"חובה לבחור לפחות {maxsideDish} תוספות!";
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
          
        }
        #endregion

    }
}
