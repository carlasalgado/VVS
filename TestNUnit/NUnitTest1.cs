﻿using System;
using NUnit.Framework;
using Es.Udc.DotNet.PracticaMaD.Model.ComentarioDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model;
using Microsoft.Practices.Unity;
using System.Transactions;
using System.Configuration;
using Es.Udc.DotNet.PracticaMaD.TestNUnit;

namespace TestNUnit
{ 


    [TestFixture]
    public class NUnitTest1
    {

        private static IUnityContainer container;
        private static IUserProfileDao userProfileDao;
        private UserProfile userProfile;

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

        //Use ClassInitialize to run code before running the first test in the class
        [OneTimeSetUp]
        public static void MyClassInitialize()
        {
            container = TestManager.ConfigureUnityContainer("unity");
            userProfileDao = container.Resolve<IUserProfileDao>();
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
        [TearDown]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }
        #region Additional test attributes



        #endregion

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [Test]
        public void DAOUserProfile_FindByLoginNameTest()
        {
            UserProfile actual = userProfileDao.FindByLoginName(userProfile.loginName);
            Assert.AreEqual(userProfile, actual);
        }
    }
}