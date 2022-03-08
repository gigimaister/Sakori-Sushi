
using Xamarin.Essentials;

namespace RealWorldApp
{
    public static class AppSettings
    {
        public static string ApiUrl = "http://localhost:49887/";
    }

    public static class TraslatedMessages
    {
        static string userLanguage = Preferences.Get("deviceLanguage", string.Empty);

        public static string Alert_Hello_Kampai()
        {
            if (userLanguage == "en") return "Kampai!!";
            else if (userLanguage == "rus") return "";
            else return "קמפאי!!";
        }
        public static string Alert_Account_Created()
        {
            if (userLanguage == "en") return "Your Account Has Been Created (:";
            else if (userLanguage == "rus") return "";
            else return "החשבון שלך נוצר בהצלחה (:";
        }
        public static string Alert_Dismiss()
        {
            if (userLanguage == "en") return "Ok";
            else if (userLanguage == "rus") return "";
            else return "בסדר";
        }
        public static string Alert_Oops()
        {
            if (userLanguage == "en") return "Oops...";
            else if (userLanguage == "rus") return "";
            else return "אופס...";
        }
        public static string Alert_Something_Went_Wrong()
        {
            if (userLanguage == "en") return "Something Went Wrong...";
            else if (userLanguage == "rus") return "";
            else return "משהו השתבש";
        }
        public static string Alert_Pwd_Missmatch()
        {
            if (userLanguage == "en") return "The Psswords Doesn't Match!";
            else if (userLanguage == "rus") return "";
            else return "סיסמאות לא תואמות";
        }
        public static string Alert_pls_Check_pwds()
        {
            if (userLanguage == "en") return "Please Make Sure The Passwords Match!";
            else if (userLanguage == "rus") return "";
            else return "נא לבדוק שהסיסמאות תואמות!";
        }

        
    }
    public static class EnglishMessages
    {
        public static string Alert_Hello_Kampai = "Kampai!!";
        public static string Alert_Account_Created = "Your Account Has Been Created (:";
        public static string Alert_Dismiss = "Ok";
        public static string Alert_Oops = "Oops...";
        public static string Alert_Something_Went_Wrong = "Something Went Wrong...";
        public static string Alert_Pwd_Missmatch = "The Psswords Doesn't Match!";
        public static string Alert_pls_Check_pwds = "Please Make Sure The Passwords Match!";
    }

}
