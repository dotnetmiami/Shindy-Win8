using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Document;
using Newtonsoft.Json;
using System.Net;
using Xunit;
using ShindyDataLoader;

namespace Shindy.Test
{

    public class TestShindyDataLoader
    {
        string eventJson = string.Empty;

        public TestShindyDataLoader()
        {

        }

        [Fact]
        public void GetJsonData_Url_Fail()
        {

            Assert.Throws<System.Net.WebException>(
                delegate
                {
                    var jsontest = Program.GetJSONData<Program.dnm>("http://fail");
                }
                );
        }
 
        [Fact]
        public void GetJsonData_Url_Pass()
        {
            var jsontest = Program.GetJSONData<Program.dnm>("http://dotnetmiami.com/event.js");
            Assert.NotNull(jsontest);
        }

        [Fact(Skip = "Need to figure out how to determine where the file is on disk to test.")]
        public void GetJsonData_Path_Pass()
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            System.Diagnostics.Debug.WriteLine(path);
            var jsontest = Program.GetJSONData<Program.dnm>(path);
            Assert.NotNull(jsontest);
        }
        
    }

}
