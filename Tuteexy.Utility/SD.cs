using System;
using System.Collections.Generic;
using System.Text;
using Tuteexy.Models;

namespace Tuteexy.Utility
{
    public static class SD
    {
        public const string Proc_CoverType_Create = "usp_CreateCoverType";
        public const string Proc_CoverType_Get = "usp_GetCoverType";
        public const string Proc_CoverType_GetAll = "usp_GetCoverTypes";
        public const string Proc_CoverType_Update = "usp_UpdateCoverType";
        public const string Proc_CoverType_Delete = "usp_DeleteCoverType";

        //public const string Role_User_Indi = "Individual Customer";
        //public const string Role_User_Comp = "Company Customer";
        public const string Role_Admin = "Admin";
        public const string Role_Ironman = "Ironman";
        public const string Role_User = "User";

        public const string ssShoppingCart = "Shoping Cart Session";

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
        public const string PaymentStatusRejected = "Rejected";



        public static double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
        {
            if (quantity < 50)
            {
                return price;
            }
            else
            {
                if (quantity < 100)
                {
                    return price50;
                }
                else
                {
                    return price100;
                }
            }
        }

        public static string ConvertToRawHtml(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static IEnumerable<Country> GetCountry()
        {
            // hard coded list for demo. 
            // You may replace with real data from database to create Employee objects
            return new List<Country>
            {
                new Country { CountryID="Afghanistan", CountryName="Afghanistan"},
                new Country { CountryID="Albania", CountryName="Albania"},
                new Country { CountryID="Algeria", CountryName="Algeria"},
                new Country { CountryID="Andorra", CountryName="Andorra"},
                new Country { CountryID="Angola", CountryName="Angola"},
                new Country { CountryID="Antigua and Barbuda", CountryName="Antigua and Barbuda"},
                new Country { CountryID="Argentina", CountryName="Argentina"},
                new Country { CountryID="Armenia", CountryName="Armenia"},
                new Country { CountryID="Australia", CountryName="Australia"},
                new Country { CountryID="Austria", CountryName="Austria"},
                new Country { CountryID="Azerbaijan", CountryName="Azerbaijan"},
                new Country { CountryID="Bahamas", CountryName="Bahamas"},
                new Country { CountryID="Bahrain", CountryName="Bahrain"},
                new Country { CountryID="Bangladesh", CountryName="Bangladesh"},
                new Country { CountryID="Barbados", CountryName="Barbados"},
                new Country { CountryID="Belarus", CountryName="Belarus"},
                new Country { CountryID="Belgium", CountryName="Belgium"},
                new Country { CountryID="Belize", CountryName="Belize"},
                new Country { CountryID="Benin", CountryName="Benin"},
                new Country { CountryID="Bhutan", CountryName="Bhutan"},
                new Country { CountryID="Bolivia", CountryName="Bolivia"},
                new Country { CountryID="Bosnia and Herzegovina", CountryName="Bosnia and Herzegovina"},
                new Country { CountryID="Botswana", CountryName="Botswana"},
                new Country { CountryID="Brazil", CountryName="Brazil"},
                new Country { CountryID="Brunei", CountryName="Brunei"},
                new Country { CountryID="Bulgaria", CountryName="Bulgaria"},
                new Country { CountryID="Burkina Faso", CountryName="Burkina Faso"},
                new Country { CountryID="Burundi", CountryName="Burundi"},
                new Country { CountryID="Côte d'Ivoire", CountryName="Côte d'Ivoire"},
                new Country { CountryID="Cabo Verde", CountryName="Cabo Verde"},
                new Country { CountryID="Cambodia", CountryName="Cambodia"},
                new Country { CountryID="Cameroon", CountryName="Cameroon"},
                new Country { CountryID="Canada", CountryName="Canada"},
                new Country { CountryID="Central African Republic", CountryName="Central African Republic"},
                new Country { CountryID="Chad", CountryName="Chad"},
                new Country { CountryID="Chile", CountryName="Chile"},
                new Country { CountryID="China", CountryName="China"},
                new Country { CountryID="Colombia", CountryName="Colombia"},
                new Country { CountryID="Comoros", CountryName="Comoros"},
                new Country { CountryID="Congo (Congo-Brazzaville)", CountryName="Congo (Congo-Brazzaville)"},
                new Country { CountryID="Costa Rica", CountryName="Costa Rica"},
                new Country { CountryID="Croatia", CountryName="Croatia"},
                new Country { CountryID="Cuba", CountryName="Cuba"},
                new Country { CountryID="Cyprus", CountryName="Cyprus"},
                new Country { CountryID="Czechia (Czech Republic)", CountryName="Czechia (Czech Republic)"},
                new Country { CountryID="Democratic Republic of the Congo", CountryName="Democratic Republic of the Congo"},
                new Country { CountryID="Denmark", CountryName="Denmark"},
                new Country { CountryID="Djibouti", CountryName="Djibouti"},
                new Country { CountryID="Dominica", CountryName="Dominica"},
                new Country { CountryID="Dominican Republic", CountryName="Dominican Republic"},
                new Country { CountryID="Ecuador", CountryName="Ecuador"},
                new Country { CountryID="Egypt", CountryName="Egypt"},
                new Country { CountryID="El Salvador", CountryName="El Salvador"},
                new Country { CountryID="Equatorial Guinea", CountryName="Equatorial Guinea"},
                new Country { CountryID="Eritrea", CountryName="Eritrea"},
                new Country { CountryID="Estonia", CountryName="Estonia"},
                new Country { CountryID="Ethiopia", CountryName="Ethiopia"},
                new Country { CountryID="Fiji", CountryName="Fiji"},
                new Country { CountryID="Finland", CountryName="Finland"},
                new Country { CountryID="France", CountryName="France"},
                new Country { CountryID="Gabon", CountryName="Gabon"},
                new Country { CountryID="Gambia", CountryName="Gambia"},
                new Country { CountryID="Georgia", CountryName="Georgia"},
                new Country { CountryID="Germany", CountryName="Germany"},
                new Country { CountryID="Ghana", CountryName="Ghana"},
                new Country { CountryID="Greece", CountryName="Greece"},
                new Country { CountryID="Grenada", CountryName="Grenada"},
                new Country { CountryID="Guatemala", CountryName="Guatemala"},
                new Country { CountryID="Guinea", CountryName="Guinea"},
                new Country { CountryID="Guinea-Bissau", CountryName="Guinea-Bissau"},
                new Country { CountryID="Guyana", CountryName="Guyana"},
                new Country { CountryID="Haiti", CountryName="Haiti"},
                new Country { CountryID="Holy See", CountryName="Holy See"},
                new Country { CountryID="Honduras", CountryName="Honduras"},
                new Country { CountryID="Hungary", CountryName="Hungary"},
                new Country { CountryID="Iceland", CountryName="Iceland"},
                new Country { CountryID="India", CountryName="India"},
                new Country { CountryID="Indonesia", CountryName="Indonesia"},
                new Country { CountryID="Iran", CountryName="Iran"},
                new Country { CountryID="Iraq", CountryName="Iraq"},
                new Country { CountryID="Ireland", CountryName="Ireland"},
                new Country { CountryID="Israel", CountryName="Israel"},
                new Country { CountryID="Italy", CountryName="Italy"},
                new Country { CountryID="Jamaica", CountryName="Jamaica"},
                new Country { CountryID="Japan", CountryName="Japan"},
                new Country { CountryID="Jordan", CountryName="Jordan"},
                new Country { CountryID="Kazakhstan", CountryName="Kazakhstan"},
                new Country { CountryID="Kenya", CountryName="Kenya"},
                new Country { CountryID="Kiribati", CountryName="Kiribati"},
                new Country { CountryID="Kuwait", CountryName="Kuwait"},
                new Country { CountryID="Kyrgyzstan", CountryName="Kyrgyzstan"},
                new Country { CountryID="Laos", CountryName="Laos"},
                new Country { CountryID="Latvia", CountryName="Latvia"},
                new Country { CountryID="Lebanon", CountryName="Lebanon"},
                new Country { CountryID="Lesotho", CountryName="Lesotho"},
                new Country { CountryID="Liberia", CountryName="Liberia"},
                new Country { CountryID="Libya", CountryName="Libya"},
                new Country { CountryID="Liechtenstein", CountryName="Liechtenstein"},
                new Country { CountryID="Lithuania", CountryName="Lithuania"},
                new Country { CountryID="Luxembourg", CountryName="Luxembourg"},
                new Country { CountryID="Madagascar", CountryName="Madagascar"},
                new Country { CountryID="Malawi", CountryName="Malawi"},
                new Country { CountryID="Malaysia", CountryName="Malaysia"},
                new Country { CountryID="Maldives", CountryName="Maldives"},
                new Country { CountryID="Mali", CountryName="Mali"},
                new Country { CountryID="Malta", CountryName="Malta"},
                new Country { CountryID="Marshall Islands", CountryName="Marshall Islands"},
                new Country { CountryID="Mauritania", CountryName="Mauritania"},
                new Country { CountryID="Mauritius", CountryName="Mauritius"},
                new Country { CountryID="Mexico", CountryName="Mexico"},
                new Country { CountryID="Micronesia", CountryName="Micronesia"},
                new Country { CountryID="Moldova", CountryName="Moldova"},
                new Country { CountryID="Monaco", CountryName="Monaco"},
                new Country { CountryID="Mongolia", CountryName="Mongolia"},
                new Country { CountryID="Montenegro", CountryName="Montenegro"},
                new Country { CountryID="Morocco", CountryName="Morocco"},
                new Country { CountryID="Mozambique", CountryName="Mozambique"},
                new Country { CountryID="Myanmar (formerly Burma)", CountryName="Myanmar (formerly Burma)"},
                new Country { CountryID="Namibia", CountryName="Namibia"},
                new Country { CountryID="Nauru", CountryName="Nauru"},
                new Country { CountryID="Nepal", CountryName="Nepal"},
                new Country { CountryID="Netherlands", CountryName="Netherlands"},
                new Country { CountryID="New Zealand", CountryName="New Zealand"},
                new Country { CountryID="Nicaragua", CountryName="Nicaragua"},
                new Country { CountryID="Niger", CountryName="Niger"},
                new Country { CountryID="Nigeria", CountryName="Nigeria"},
                new Country { CountryID="North Korea", CountryName="North Korea"},
                new Country { CountryID="North Macedonia", CountryName="North Macedonia"},
                new Country { CountryID="Norway", CountryName="Norway"},
                new Country { CountryID="Oman", CountryName="Oman"},
                new Country { CountryID="Pakistan", CountryName="Pakistan"},
                new Country { CountryID="Palau", CountryName="Palau"},
                new Country { CountryID="Palestine State", CountryName="Palestine State"},
                new Country { CountryID="Panama", CountryName="Panama"},
                new Country { CountryID="Papua New Guinea", CountryName="Papua New Guinea"},
                new Country { CountryID="Paraguay", CountryName="Paraguay"},
                new Country { CountryID="Peru", CountryName="Peru"},
                new Country { CountryID="Philippines", CountryName="Philippines"},
                new Country { CountryID="Poland", CountryName="Poland"},
                new Country { CountryID="Portugal", CountryName="Portugal"},
                new Country { CountryID="Qatar", CountryName="Qatar"},
                new Country { CountryID="Romania", CountryName="Romania"},
                new Country { CountryID="Russia", CountryName="Russia"},
                new Country { CountryID="Rwanda", CountryName="Rwanda"},
                new Country { CountryID="Saint Kitts and Nevis", CountryName="Saint Kitts and Nevis"},
                new Country { CountryID="Saint Lucia", CountryName="Saint Lucia"},
                new Country { CountryID="Saint Vincent and the Grenadines", CountryName="Saint Vincent and the Grenadines"},
                new Country { CountryID="Samoa", CountryName="Samoa"},
                new Country { CountryID="San Marino", CountryName="San Marino"},
                new Country { CountryID="Sao Tome and Principe", CountryName="Sao Tome and Principe"},
                new Country { CountryID="Saudi Arabia", CountryName="Saudi Arabia"},
                new Country { CountryID="Senegal", CountryName="Senegal"},
                new Country { CountryID="Serbia", CountryName="Serbia"},
                new Country { CountryID="Seychelles", CountryName="Seychelles"},
                new Country { CountryID="Sierra Leone", CountryName="Sierra Leone"},
                new Country { CountryID="Singapore", CountryName="Singapore"},
                new Country { CountryID="Slovakia", CountryName="Slovakia"},
                new Country { CountryID="Slovenia", CountryName="Slovenia"},
                new Country { CountryID="Solomon Islands", CountryName="Solomon Islands"},
                new Country { CountryID="Somalia", CountryName="Somalia"},
                new Country { CountryID="South Africa", CountryName="South Africa"},
                new Country { CountryID="South Korea", CountryName="South Korea"},
                new Country { CountryID="South Sudan", CountryName="South Sudan"},
                new Country { CountryID="Spain", CountryName="Spain"},
                new Country { CountryID="Sri Lanka", CountryName="Sri Lanka"},
                new Country { CountryID="Sudan", CountryName="Sudan"},
                new Country { CountryID="Suriname", CountryName="Suriname"},
                new Country { CountryID="Swaziland", CountryName="Swaziland"},
                new Country { CountryID="Sweden", CountryName="Sweden"},
                new Country { CountryID="Switzerland", CountryName="Switzerland"},
                new Country { CountryID="Syria", CountryName="Syria"},
                new Country { CountryID="Tajikistan", CountryName="Tajikistan"},
                new Country { CountryID="Tanzania", CountryName="Tanzania"},
                new Country { CountryID="Thailand", CountryName="Thailand"},
                new Country { CountryID="Timor-Leste", CountryName="Timor-Leste"},
                new Country { CountryID="Togo", CountryName="Togo"},
                new Country { CountryID="Tonga", CountryName="Tonga"},
                new Country { CountryID="Trinidad and Tobago", CountryName="Trinidad and Tobago"},
                new Country { CountryID="Tunisia", CountryName="Tunisia"},
                new Country { CountryID="Turkey", CountryName="Turkey"},
                new Country { CountryID="Turkmenistan", CountryName="Turkmenistan"},
                new Country { CountryID="Tuvalu", CountryName="Tuvalu"},
                new Country { CountryID="Uganda", CountryName="Uganda"},
                new Country { CountryID="Ukraine", CountryName="Ukraine"},
                new Country { CountryID="United Arab Emirates", CountryName="United Arab Emirates"},
                new Country { CountryID="United Kingdom", CountryName="United Kingdom"},
                new Country { CountryID="United States of America", CountryName="United States of America"},
                new Country { CountryID="Uruguay", CountryName="Uruguay"},
                new Country { CountryID="Uzbekistan", CountryName="Uzbekistan"},
                new Country { CountryID="Vanuatu", CountryName="Vanuatu"},
                new Country { CountryID="Venezuela", CountryName="Venezuela"},
                new Country { CountryID="Vietnam", CountryName="Vietnam"},
                new Country { CountryID="Yemen", CountryName="Yemen"},
                new Country { CountryID="Zambia", CountryName="Zambia"},
                new Country { CountryID="Zimbabwe", CountryName="Zimbabwe"}

            };
        }

        public static IEnumerable<string> GetGenderPreference()
        {
            // hard coded list for demo. 
            // You may replace with real data from database to create Employee objects
            return new List<string>
            {
                "Female","Male", "No special preference"
            };
        }
     }
}
