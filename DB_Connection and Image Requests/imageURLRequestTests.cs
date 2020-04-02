using Microsoft.VisualStudio.TestTools.UnitTesting;
using vr_db_interaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace vr_db_interaction.Tests
{
    [TestClass()]
    public class imageURLRequestTests
    {
        [TestMethod()]
        public async System.Threading.Tasks.Task imageURLRequestTestAsync()
        {
            imageURLRequest Test = new imageURLRequest("9jZ0HHINNLU7kcFYeQl6~XFYbAjS6FYvcXH-iPNVsPg~AgOFExaKkIpdR0kX6qUzR-8HqOONNa9lpAF0l0d6gMoC-U7MLEddz8iMqdesaCCn");
            await Test.initializeURL();
            string testURL = Test.GetQuadKeyURL(44.069, -103.228, 14);
            Assert.AreNotEqual(" ", testURL);
        }
    }
}