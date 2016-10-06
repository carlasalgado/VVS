﻿using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Linq;
using System.Data.Entity;


namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public class UserProfileDaoEntityFramework : GenericDaoEntityFramework <UserProfile, Int64>, IUserProfileDao
    {
        public UserProfileDaoEntityFramework() { }

        #region IUserProfileDao Members

        public UserProfile FindByLoginName(string loginName)
        {
            UserProfile userProfile = null;


            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 where u.loginName == loginName
                 select u);

            userProfile = result.FirstOrDefault();

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(UserProfile).FullName);


            return userProfile;
        }

        #endregion
    
    }
}
