using Microsoft.VisualStudio.TestTools.UnitTesting;
using VFCAuditChecker;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VFCAuditChecker.Tests
{
    [TestClass()]
    public class CloudAPITests
    {
        /*
         * Arrange
         * Act
         * Assert
        */

        public string getKey()
        {
            string PATH = "C:/Users/asantana/Desktop/VFCAuditChecker/VFCAuditChecker/resources/Credentials.txt";
            string key = File.ReadAllText(PATH);
            return key;
        }

        [TestMethod()]
        public void GetStatus_Test()
        {
            // arrange
            CloudAPI api = new CloudAPI(getKey());

            // act
            string status = api.GetStatus();

            // assert
            Assert.IsNotNull(status);
        }

        [TestMethod()]
        public void Login_Valid()
        {
            // arrange
            CloudAPI api = new CloudAPI(getKey());

            // act
            api.Login("andres.santana@lascarelectronics.com", "Lascarseddi12");
            
            // assert handled through exception
        }

        [TestMethod()]
        public void Login_Invalid()
        {
            // Arrange
            CloudAPI api = new CloudAPI(getKey());

            // act
            try
            {
                api.Login("fake@fake.com", "fake");
            } catch (Exception e)
            {
                Assert.IsTrue(e.GetType() == typeof(CloudAPI.CloudException));
            }

            // assert 
            //Assert.Fail("Expected CloudException");
        }
    }
}