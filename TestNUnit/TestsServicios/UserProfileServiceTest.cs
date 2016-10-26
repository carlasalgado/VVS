using System;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;

namespace Es.Udc.DotNet.PracticaMaD.TestNUnit.TestsServicios
{
    [TestFixture]
    public class UserProfileServiceTest
    {

        private static IUnityContainer container;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;

        // Variables used in several tests are initialized here
        private const String loginName = "loginNameTest";
        private const String clearPassword = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;

        TransactionScope transaction;

        private TestContext testContextInstance;

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

        //Use ClassInitialize to run code before running the first test in the class
        [OneTimeSetUp]
        public static void MyClassInitialize()
        {
            container = TestManager.ConfigureUnityContainer("unity");
            userProfileDao = container.Resolve<IUserProfileDao>();
            userService = container.Resolve<IUserService>();

        }

        //Use ClassCleanup to run code after all tests in a class have run
        [OneTimeTearDown]
        public static void MyClassCleanup()
        {
            TestManager.ClearUnityContainer(container);
        }

        //Use TestInitialize to run code before running each test
        [SetUp]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TearDown]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        /// <summary>
        ///A test for RegisterUser
        ///</summary>
        [Test]
        public void PR_IN_58()
        {
            // Register user and find profile
            long userId =
                userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            UserProfile userProfile = userProfileDao.Find(userId);

            // Check data
            Assert.AreEqual(userId, userProfile.usrId);
            Assert.AreEqual(loginName, userProfile.loginName);
            Assert.AreEqual(PasswordEncrypter.Crypt(clearPassword), userProfile.enPassword);
            Assert.AreEqual(firstName, userProfile.firstName);
            Assert.AreEqual(lastName, userProfile.lastName);
            Assert.AreEqual(email, userProfile.email);
            Assert.AreEqual(language, userProfile.language);
            Assert.AreEqual(country, userProfile.country);

        }

        /// <summary>
        ///A test for registering a user that already exists in the database
        ///</summary>
        [Test]
        public void PR_IN_59()
        {
            // Register user
            userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));
            // Register the same user
            try
            {
                userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                Assert.IsTrue(false);
            }
            catch (DuplicateInstanceException)
            {
                Assert.IsTrue(true);
            }
        }

        ///// <summary>
        /////A test for Login with clear password
        /////</summary>
        [Test]
        public void PR_IN_60()
        {
            // Register user
            long userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            LoginResult expected = new LoginResult(userId, firstName,
                PasswordEncrypter.Crypt(clearPassword), language, country);

            // Login with clear password
            LoginResult actual =
                   userService.Login(loginName,
                   clearPassword, false);

            // Check data
            Assert.AreEqual(expected, actual);

        }

        ///// <summary>
        /////A test for Login with encrypted password
        /////</summary>
        [Test]
        public void PR_IN_61()
        {
            // Register user
            long userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            LoginResult expected = new LoginResult(userId, firstName,
                PasswordEncrypter.Crypt(clearPassword), language, country);

            // Login with encrypted password
            LoginResult obtained =
                   userService.Login(loginName,
                   PasswordEncrypter.Crypt(clearPassword), true);

            // Check data
            Assert.AreEqual(expected, obtained);
        }

        ///// <summary>
        /////A test for Login with incorrect password
        /////</summary>
        [Test]
        public void PR_IN_62()
        {
            // Register user
            long userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            // Login with incorrect (clear) password
            try
            {
                LoginResult actual =
                       userService.Login(loginName, clearPassword + "X", false);
                Assert.IsTrue(false);
            }
            catch (IncorrectPasswordException)
            {
                Assert.IsTrue(true);
            }
        }

        ///// <summary>
        /////A test for Login with a non-existing user
        /////</summary>
        [Test]
        public void PR_IN_63()
        {
            // Login for a user that has not been registered
            try
            {
                LoginResult actual =
                       userService.Login(loginName, clearPassword, false);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for FindUserProfileDetails
        ///</summary>
        [Test]
        public void PR_IN_64()
        {
            UserProfileDetails expected =
                new UserProfileDetails(firstName, lastName, email, language, country);

            long userId =
                userService.RegisterUser(loginName, clearPassword, expected);

            UserProfileDetails obtained =
                   userService.FindUserProfileDetails(userId);

            // Check data
            Assert.AreEqual(expected, obtained);
        }

        /// <summary>
        ///A test for FindUserProfileDetails when the user does not exist
        ///</summary>
        [Test]
        public void PR_IN_65()
        {
            try
            {
                userService.FindUserProfileDetails(NON_EXISTENT_USER_ID);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for UpdateUserProfileDetails
        ///</summary>
        [Test]
        public void PR_IN_66()
        {
            // Register user and update profile details
            long userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            UserProfileDetails expected =
                    new UserProfileDetails(firstName + "X", lastName + "X",
                        email + "X", "XX", "XX");

            userService.UpdateUserProfileDetails(userId, expected);

            UserProfileDetails obtained =
                userService.FindUserProfileDetails(userId);

            // Check changes
            Assert.AreEqual(expected, obtained);
        }

        /// <summary>
        ///A test for UpdateUserProfileDetails when the user does not exist
        ///</summary>
        [Test]
        public void PR_IN_67()
        {
            try
            {
                userService.UpdateUserProfileDetails(NON_EXISTENT_USER_ID,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for ChangePassword
        ///</summary>
        [Test]
        public void PR_IN_68()
        {
            // Register user
            long userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            // Change password
            String newClearPassword = clearPassword + "X";
            userService.ChangePassword(userId, clearPassword, newClearPassword);

            // Try to login with the new password. If the login is correct, then
            // the password was successfully changed.
            userService.Login(loginName, newClearPassword, false);
        }

        /// <summary>
        ///A test for ChangePassword entering a wrong old password
        ///</summary>
        [Test]
        public void PR_IN_69()
        {
            // Register user
            long userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language, country));

            // Change password
            String newClearPassword = clearPassword + "X";
            try
            {
                userService.ChangePassword(userId, clearPassword + "Y", newClearPassword);
                Assert.IsTrue(false);
            }
            catch (IncorrectPasswordException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for ChangePassword when the user does not exist
        ///</summary>
        [Test]
        public void PR_IN_70()
        {
            try
            {
                userService.ChangePassword(NON_EXISTENT_USER_ID,
                    clearPassword, clearPassword + "X");
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }


    }
}
