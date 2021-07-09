using DemoApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DemoApp.Permissions
{
    public class DemoAppPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup_define = context.AddGroup(DemoAppPermissions.GroupName_Define);
            var myGroup_user = context.AddGroup(DemoAppPermissions.GroupName_User);

            var demoAppPermission_define = myGroup_define.AddPermission(DemoAppPermissions.DemoApp.Default_Define_ToDo, L("ToDo Task Management"));
            demoAppPermission_define.AddChild(DemoAppPermissions.DemoApp.Create_Define_ToDo, L("Create"));
            demoAppPermission_define.AddChild(DemoAppPermissions.DemoApp.Update_Define_ToDo, L("Update"));
            demoAppPermission_define.AddChild(DemoAppPermissions.DemoApp.Delete_Define_ToDo, L("Delete"));

            var demoAppPermission_user = myGroup_user.AddPermission(DemoAppPermissions.DemoApp.Default_User_ToDo, L("ToDo User Update"));
            demoAppPermission_user.AddChild(DemoAppPermissions.DemoApp.Create_User_ToDo, L("Create"));
            demoAppPermission_user.AddChild(DemoAppPermissions.DemoApp.Update_User_ToDo, L("Update"));
            demoAppPermission_user.AddChild(DemoAppPermissions.DemoApp.Delete_User_ToDo, L("Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DemoAppResource>(name);
        }
    }
}
