using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileLoggerApp.src.mobilelogger.model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace MobileLoggerTest.tests
{
    [TestClass]
    public class LogDatabaseTest
    {
        [TestMethod]
        public void TestInsert()
        {
            using(LogEventDataContext context = new LogEventDataContext(MobileLoggerApp.MainPage.ConnectionString))
            {

            }
        }
    }
}
