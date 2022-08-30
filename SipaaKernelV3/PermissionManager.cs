using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3
{
    public enum Permission : uint
    {
        PermZero = 0x00,
        PermRoot = 0x01,
        PermUser = 0x02,
        PermGuest = 0x03,
        PermUnknown = 0xFF
    }
    public class PermissionManager
    {
        uint token;
        List<Permission> permissions;

        public List<Permission> Permissions { get { return permissions; } }

        public uint Initialize()
        {
            permissions = new List<Permission>();
            token = (uint)new Random().Next(1000, 32767);
            return token;
        }

        public bool IsUserHavePermission(Permission perm)
        {
            bool isUserHavePerm = false;
            foreach (Permission p in permissions)
            {
                if (p == perm)
                {
                    isUserHavePerm = true;
                }
            }
            return isUserHavePerm;
        }
        public int RemovePermission(Permission perm, uint token)
        {
            if (this.token == token)
            {
                permissions.Remove(perm);
                return 0;
            }
            else
            {
                return 1;
            }
        }
        public int AddPermission(Permission perm, uint token)
        {
            if (this.token == token)
            {
                permissions.Add(perm);
                return 0;
            }
            else
            {
                return 1;
            }
        }

        // STATIC METHODS
        public static Permission GetPermissionFromString(string perm)
        {
            switch (perm)
            {
                case "zero":
                    return Permission.PermZero;
                case "root":
                    return Permission.PermRoot;
                case "guest":
                    return Permission.PermGuest;
                case "user":
                    return Permission.PermUser;
            }
            return Permission.PermUnknown;
        }
    }
}
