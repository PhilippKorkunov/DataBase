using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    internal class Threat
    {
        private string privacyBreak = "Нет";
        private string integrityBreak = "Нет";
        private string availabilityBreak = "Нет";

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Aim { get; set; }
        public string PrivacyBreak
        {
            set { privacyBreak = BoolToString(value); }
            get { return privacyBreak; }
        }
        public string IntegrityBreak
        {
            set { integrityBreak = BoolToString(value); }
            get { return integrityBreak; }
        }
        public string AvailabilityBreak
        {
            set { availabilityBreak = BoolToString(value); }
            get { return availabilityBreak; }
        }
        public DateTime AppearDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        private static string BoolToString(string b)
        {
            if (b == "1") return "Да";
            else return "Нет";
        }
    }
}
