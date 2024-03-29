﻿namespace Management.Common
{
    public static class ApiRoute
    {
        #region Account

        public static string Login = "/api/Account/Login";
        public static string SignUp = "/api/Account/Register";
        public static string ForgetPassword = "/api/Account/ForgetPassword";
        public static string ResetPassword = "/api/Account/ResetPassword";
        public static string ResetPasswordUrl = "https://localhost:7222/Account/ResetPassword";

        #endregion Account
    }
}