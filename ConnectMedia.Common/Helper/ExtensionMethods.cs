﻿using ConnectMedia.Common.Model;
using System.Collections.Generic;
using System.Linq;

namespace ConnectMedia.Common.Helper
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            if (user == null) return null;

            user.password = null;
            return user;
        }
    }
}
