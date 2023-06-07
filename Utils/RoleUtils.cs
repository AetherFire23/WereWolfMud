using System.Runtime.CompilerServices;
using WereWolfUltraCool.Enums;

namespace WereWolfMud.Utils
{
    public static class RoleUtils
    {
        private static readonly List<RoleType> EvilRoles = new List<RoleType>()
        {
            RoleType.WereWolf,
        };

        private static readonly List<RoleType> NeutralRoles = new List<RoleType>()
        {
            RoleType.Lycanthrope,
        };

        private static List<RoleType> TownRoles => new List<RoleType>(
            TownProtective.Union(TownInvestigative).Union(TownSupportive).Union(TownProtective)
        );

        private static readonly List<RoleType> TownProtective = new List<RoleType>()
        {
            RoleType.Detective,
            RoleType.Doctor,
        };

        private static readonly List<RoleType> TownInvestigative = new List<RoleType>()
        {
            RoleType.Doctor,
        };


        private static readonly List<RoleType> TownSupportive = new List<RoleType>()
        {
            RoleType.Medium,
        };


        public static bool IsEvilRole(RoleType role)
        {
            return EvilRoles.Contains(role);
        }

        public static bool IsNeutralRole(RoleType role)
        {
            return NeutralRoles.Contains(role);
        }

        public static bool IsTownRole(RoleType role)
        {
            return TownRoles.Contains(role);
        }

        public static bool IsRoleValid(RoleType role)
        {
            return IsEvilRole(role) || IsNeutralRole(role) || IsTownRole(role);
        }

        public static RoleType GetRandomTown()
        {
            var role = RandomHelper.GetRandomFrom(TownRoles);
            return role;
        }

        public static RoleType GetRandomTownInvestigative()
        {
           return RandomHelper.GetRandomFrom(TownInvestigative);
        }
        public static RoleType GetRandomTownSupportive()
        {
            return RandomHelper.GetRandomFrom(TownSupportive);
        }
        public static RoleType GetRandomTownProtective()
        {
            return RandomHelper.GetRandomFrom(TownProtective);
        }

        public static RoleType GetRandomEvil()
        {
            return RandomHelper.GetRandomFrom(EvilRoles);
        }

        public static RoleType GetRandomNeutral()
        {
            return RandomHelper.GetRandomFrom(NeutralRoles);
        }
    }
}
