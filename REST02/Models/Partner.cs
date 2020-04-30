using REST02.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST02.Models
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public PartnerType Type { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }


        public bool Valid()
        {
            if (String.IsNullOrWhiteSpace(Name) || String.IsNullOrWhiteSpace(INN))
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(Enums.PartnerType), Type))
            {
                return false;
            }

            if ((Type == Enums.PartnerType.LegalEntity) && String.IsNullOrWhiteSpace(KPP))
            {
                return false;
            }

            return true;
        }

    }   

}
