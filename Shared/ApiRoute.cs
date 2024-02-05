namespace Management.Common
{
    public static class ApiRoute
    {
        #region Account

        public static string Login = "/api/Account/Login";
        public static string SignUp = "/api/Account/Register";
        public static string ForgetPassword = "/api/Account/ForgetPassword";
        public static string ResetPassword = "/api/Account/ResetPassword";
        public static string ResetPasswordUrl = "https://localhost:7222/Account/ResetPassword";
        public static string CreateProject = "/api/Project/CreateProject";
        public static string GetAllProject = "api/Project/GetAllProjects";
        public static string GetTechStackNames="api/Project/GetTechStackNames";
        public static string DeleteProject = "/api/Project/DeleteProject";
        public static string UpdateProject= "/api/Project/UpdateProject";
        public static string UpdateProjectPost = "/api/Project/UpdateProjectPost";
        public static string SearchByName = "/api/Project/SearchByName";
        public static string GetAllUsers = "/api/Account/GetAllUsers";

        #endregion Account
    }
}