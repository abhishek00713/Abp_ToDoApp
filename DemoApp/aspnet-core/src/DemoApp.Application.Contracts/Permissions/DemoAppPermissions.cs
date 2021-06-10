namespace DemoApp.Permissions
{
    public static class DemoAppPermissions
    {
        //Creating Permission
        public const string GroupName_Define = "ToDo Task Management";
        public const string GroupName_User = "ToDo User Update";

        public static class DemoApp
        {
            //for maanger or admin permission to define ToDo Tasks
            public const string Default_Define_ToDo = GroupName_Define + ".DemoApp";
            public const string Create_Define_ToDo = Default_Define_ToDo + ".Create";
            public const string Update_Define_ToDo = Default_Define_ToDo + ".Update";
            public const string Delete_Define_ToDo = Default_Define_ToDo + ".Delete";

            //for user permission to update ToDo Tasks
            public const string Default_User_ToDo = GroupName_User + ".DemoApp";
            public const string Create_User_ToDo = Default_User_ToDo + ".Create";
            public const string Update_User_ToDo = Default_User_ToDo + ".Update";
            public const string Delete_User_ToDo = Default_User_ToDo + ".Delete";
        }
    }
}