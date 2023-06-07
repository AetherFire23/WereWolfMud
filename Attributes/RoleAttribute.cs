using WereWolfUltraCool.Enums;

namespace WereWolfMud.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RoleAttribute : Attribute
    {
        public RoleType RoleType { get; set; }

        public RoleAttribute(RoleType roleType)
        {
            RoleType = roleType;
        }
    }
}
