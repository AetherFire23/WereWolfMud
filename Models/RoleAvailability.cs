using Microsoft.Identity.Client;
using WereWolfUltraCool.Enums;

namespace WereWolfMud.Models
{
    public class RoleAvailability
    {
        public RoleType RoleType { get; set; }
        public bool IsAvailable { get; set; }

        public RoleAvailability(RoleType roleType)
        {
            RoleType = roleType;
            IsAvailable = true;
        }
    }
}
