using CGen.Base;
using CGen.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NGen.Tests.NGenTests
{
    [TestClass]
    public class ReadingTemplate
    {
        [TestMethod]
        public void Generate()
        {
            var settings = new CGenSettings
            {
                ConnectionString = @"Data Source=DESKTOP-8CFSBBB\SQLEXPRESS;Initial Catalog=IndicoPacking;Integrated Security=True",
                PathToProjectFolder = @"C:\Users\srlso\Documents\Visual Studio 2015\Projects\TestingDataAccess\TestingDataAccess",
                ProjectName = "TestingDataAccess",
                ProjectNamespace = "TestingDataAccess"
            };
            Generator.Generate(settings);
        }
    }
}
