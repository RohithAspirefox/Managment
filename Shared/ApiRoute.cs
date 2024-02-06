namespace Management.Common
{
    public static class ApiRoute
    {
        #region Account

        public static string Login = "/api/Account/Login";
        public static string SignUp = "/api/Account/Register";
        public static string ForgetPassword = "/api/Account/ForgetPassword";
        public static string ResetPassword = "/api/Account/ResetPassword";
        public static string CreatePassword = "/api/Account/CreatePassword";
        public static string SetPassword = "/api/Account/SetPassword";
        public static string UpdateUsers = "/api/User/UpdateUsers";

        public static string ResetPasswordUrl = "https://localhost:7222/Account/ResetPassword";

        #endregion Account
    }
}