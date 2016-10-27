using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.TestNUnit.TestsDAOs
{
    [TestFixture]
    public class GrupoDAOTest
    {


        #region Variables
        private static IUnityContainer container;
        private static IUserProfileDao userProfileDao;
        private static IGrupoDao grupoDao;
        private static Grupo grupo1;
        private static Grupo grupo2;
        private UserProfile userProfile;
        private static String GRUPO_NO_EXISTENTE = "grupoNoExiste";

        TransactionScope transaction;

        private TestContext testContextInstance;
        #endregion

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
            grupoDao = container.Resolve<IGrupoDao>();
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

            grupo1 = new Grupo();
            grupo2 = new Grupo();

            grupo1.nombre = "Deportes";
            grupo1.descripcion = "Grupo de deportes";

            grupo2.nombre = "Musica";
            grupo2.descripcion = "Grupo de musica";

            grupoDao.Create(grupo1);
            grupoDao.Create(grupo2);

            userProfile = new UserProfile();
            userProfile.loginName = "jsmith";
            userProfile.enPassword = "password";
            userProfile.firstName = "John";
            userProfile.lastName = "Smith";
            userProfile.email = "jsmith@acme.com";
            userProfile.language = "en";
            userProfile.country = "US";
            userProfile.Grupo.Add(grupo2);

            userProfileDao.Create(userProfile);

        }

        //Use TestCleanup to run code after each test has run
        [TearDown]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        [Test]
        public void PR_UN_18()
        {
            Collection<Grupo> gruposObtenidos = grupoDao.MostrarGrupos(0, 10);

            Assert.AreEqual(2, gruposObtenidos.Count);

            Assert.IsTrue(gruposObtenidos.Contains(grupo1));

            Assert.IsTrue(gruposObtenidos.Contains(grupo2));
        }

        [Test]
        public void PR_UN_19()
        {
            Assert.AreEqual(grupo1, grupoDao.BuscarPorNombre("Deportes"));
        }

        [Test]
        public void PR_UN_20()
        {
            try
            {
                grupoDao.BuscarPorNombre(GRUPO_NO_EXISTENTE);
                Assert.IsTrue(false);

            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void PR_UN_21()
        {
            Collection<Grupo> obtenido = grupoDao.BuscarPorUsuario(userProfile.usrId);

            Assert.AreEqual(1, obtenido.Count);
            Assert.AreEqual(grupo2, obtenido.First());
        }
    }
}
