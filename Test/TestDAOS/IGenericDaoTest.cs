using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using System;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{

    /// <summary>
    ///This is a test class for IGenericDaoTest and is intended
    ///to contain all IGenericDaoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IGenericDaoTest
    {

        private TestContext testContextInstance;
        private UserProfile userProfile;
        private static IUnityContainer container;
        private static IUserProfileDao userProfileDao;


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {

            container = TestManager.ConfigureUnityContainer("unity");

            userProfileDao = container.Resolve<IUserProfileDao>();

        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearUnityContainer(container);
        }
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            userProfile = new UserProfile();
            userProfile.loginName = "jsmith";
            userProfile.enPassword = "password";
            userProfile.firstName = "John";
            userProfile.lastName = "Smith";
            userProfile.email = "jsmith@acme.com";
            userProfile.language = "en";
            userProfile.country = "US";

            userProfileDao.Create(userProfile);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            try
            {
                userProfileDao.Remove(userProfile.usrId);
            }
            catch (Exception) { }
        }

        #endregion


        [TestMethod()]
        public void DAOGeneric_FindTest()
        {

            try
            {
                UserProfile actual = userProfileDao.Find(userProfile.usrId);

                Assert.AreEqual(userProfile, actual, "User found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }



        [TestMethod()]
        public void DAOGeneric_ExistsTest()
        {
            try
            {
                bool userExists = userProfileDao.Exists(userProfile.usrId);

                Assert.IsTrue(userExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAOGeneric_NotExistsTest()
        {
            try
            {
                bool userNotExists = userProfileDao.Exists(-1);

                Assert.IsFalse(userNotExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void DAOGeneric_UpdateTest()
        {
            try
            {
                userProfile.firstName = "Juan";
                userProfile.lastName = "González";
                userProfile.email = "jgonzalez@acme.es";
                userProfile.language = "es";
                userProfile.country = "ES";
                userProfile.enPassword = "contraseña";

                userProfileDao.Update(userProfile);

                UserProfile actual = userProfileDao.Find(userProfile.usrId);

                Assert.AreEqual(userProfile, actual);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Remove
        [TestMethod()]
        public void DAOGeneric_RemoveTest()
        {

            try
            {
                userProfileDao.Remove(userProfile.usrId);

                bool userExists = userProfileDao.Exists(userProfile.usrId);

                Assert.IsFalse(userExists);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void DAOGeneric_CreateTest()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                UserProfile newUserProfile = new UserProfile();
                newUserProfile.loginName = "login";
                newUserProfile.enPassword = "password";
                newUserProfile.firstName = "John";
                newUserProfile.lastName = "Smith";
                newUserProfile.email = "john.smith@acme.com";
                newUserProfile.language = "en";
                newUserProfile.country = "US";

                userProfileDao.Create(newUserProfile);

                bool userExists = userProfileDao.Exists(newUserProfile.usrId);

                Assert.IsTrue(userExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }

        }

    }

}