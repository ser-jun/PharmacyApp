using PharmacyApp.Models;

namespace PharmacyApp.Services
{
    public static class CurrentUser
    {
        public static User User { get; private set; }
        public static bool IsAuthenticated => User != null;
        public static bool IsAdmin => User?.Role == "admin";
        public static bool IsPharmacist => User?.Role == "pharmacist";
        public static bool IsCustomer => User?.Role == "customer";

        public static void Login(User user) => User = user;
        public static void Logout() => User = null;

    }
}

