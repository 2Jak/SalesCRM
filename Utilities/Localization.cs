using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesCRM.Utilities
{
    public static class Localization
    {
        public static Dictionary<string, string> EnglishHebrew { get; } = new Dictionary<string, string>
        {
            { "ID", "ת.ז"},
            { "Name", "שם" },
            { "Address", "כתובת" },
            { "Phonenumber", "מספר טלפון" },
            { "SecondaryPhonenumber", "מספר טלפון נוסף" },
            { "Email", "אימייל" },
            { "Male", "מין" },
            { "Status", "סטאטוס" },
            { "SafetyCheck", @"צ'ק ביטחון" },
            { "PaidRegistrationFee", "דמי הרשמה שולמו" },
            { "SignedContract", "חוזה חתום" },
            { "AssignedToWork", "הוצב בעבודה" },
            { "PaymentTransfered", "תשלום הועבר" },
            { "DesiredCourse", "קורס מועדף" },
            { "LastUpdate", "עודכן לאחרונה" },
            { "FreeText", "טקסט חופשי" }
        };

        public static string GetGenderStringForBool(bool? male)
        {
            switch(male)
            {
                case true:
                    return "זכר";
                case false:
                    return "נקבה";
                default:
                    return "לא ידוע";
            }
        }

        public static Dictionary<string, bool?> GenderStringToBool { get; } = new Dictionary<string, bool?>
        {
            { "זכר", true },
            { "נקבה", false },
            { "לא ידוע", null }
        };
    }
}
