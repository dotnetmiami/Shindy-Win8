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
        public void GetJsonData_Url_WebException()
        {

            Assert.Throws<System.Net.WebException>(
                delegate
                {
                    var jsontest = Program.GetURLJsonData("http://fail");
                }
                );
        }
 
        [Fact]
        public void GetJsonData_Url_NotNull()
        {
            var jsontest = Program.GetURLJsonData("http://dotnetmiami.com/event.js");
            Assert.NotNull(jsontest);
        }

        [Fact]
        public void DetermineTransport_Url_Pass()
        {
            Program.TransportType tType = Program.DetermineTransport("http://dotnetmiami.com");
            Assert.Equal(Program.TransportType.http, tType);
        }

        [Fact]
        public void DetermineTransport_FileValidPath_Pass()
        {
            Program.TransportType tType = Program.DetermineTransport("c:\temp");
            Assert.Equal(Program.TransportType.file, tType);
        }

        // This passes since the default is file. We're not checking the validity of the path.
        [Fact]
        public void DetermineTransport_FileInvalidPath_Pass()
        {
            Program.TransportType tType = Program.DetermineTransport("invalid path");
            Assert.Equal(Program.TransportType.file, tType);
        }

        [Fact]
        public void LoadObjectFromJson_ValidJson_NotNull()
        {
            string jsonData = "{ \"Events\" : [ { \"EventID\" : 110 , \"EventDateTime\" : \"7/19/2012 6:30 PM\" , \"Title\" : \"Introduction to MEF: the Managed Extensiblility Framework and jQuery Basics\" ,  \"HostedGroups\" : [ {     \"Name\" : \"dotNet Miami\"  } ] , \"Description\" : \"<p>Creating flexible and extensible software should be our goal as software developers. But time constraints and technical complexity usually derail us from this. A few years ago Microsoft released the Managed Extensibility Framework (MEF). It helps us achieve a true pluggable architecture and assists us in creating flexible software. Sam Abraham will show us what MEF is and how to implement it in our own applications.  Also, Dave Nicolas will be back again to show us the basics of jQuery and how we should be using it.  If you’ve ever wanted to create applications that can uses plug-ins or wanted get to know jQuery a bit better then you don’t want to miss this interactive gathering of the dotNet Miami community.</p><p>We will also be giving away more books, swag, and another one-month subscription to PluralSight.</p>\" , \"RegistrationURI\" : \"http://dotnetmiamijuly2012.eventbrite.com/\" , \"EventLocation\" : {     \"LocationID\" : 100 ,     \"Name\" : \"Planet Linux Caffe\" ,     \"LocationURI\" : \"http://www.planetlinuxcaffe.com\" ,     \"Address\" : {  \"Street\" : \"1430 Ponce De Leon Blvd\" ,  \"City\" : \"Coral Gables\" ,  \"State:\" : \"FL\" ,  \"ZipCode\" : \"33134\" ,  \"AddressURL\" : \"http://maps.google.es/maps?f=q&source=s_q&hl=en&geocode=&q=1430+Ponce+De+Leon+Boulevard,+Coral+Gables,+FL+33134&aq=&sll=40.396764,-3.713379&sspn=13.011054,19.753418&vpsrc=0&ie=UTF8&hq=&hnear=1430+Ponce+De+Leon+Blvd,+Coral+Gables,+Florida+33134,+Estados+Unidos&t=m&view=map\"      }  } ,  \"Sessions\" : [ {     \"Title\" : \"Introducing MEF: the Managed Extensiblility Framework\" ,     \"SessionType\" : \"main\" ,     \"Abstract\" : \"<p>The Managed Extensibility Framework (MEF) is a composition layer for .NET that improves the flexibility, maintainability and testability of large applications. Join us in this code-centric talk as we progressively introduce MEF and highlight how leveraging concepts including IoC Containers, Dependency Injection, Composition, Attribute Annotation and Inheritance can support development efforts in an agile environment.</p>\" ,     \"PresentationURI\" : \"\" ,     \"DemoURI\" : \"\" ,     \"Speakers\" : [ {  \"PersonID\" : 111 ,  \"FirstName\" : \"Sam\" ,   \"LastName\" : \"Abraham\" ,  \"Email\" : \"wildturtle21@hotmail.com\" ,   \"PersonURI\" : \"http://www.geekswithblogs.net/wildturtle\" ,  \"Bio\" : \"<p>SomeBio</p>\"     } ] } , {     \"Title\" : \"jQuery Basics\" ,     \"SessionType\" : \"intro\" ,     \"abstract\" : \"<p></p>\" ,     \"PresentationURI\" : \"\" ,     \"DemoURI\" : \"\" ,     \"Speakers\" : [ {  \"PersonID\" : 106 ,   \"FirstName\" : \"Dave\" ,   \"LastName\" : \"Nicolas\" ,  \"Email\" : \"\" ,   \"PersonURI\" : \"\" ,  \"Bio\" : \"<p>Another Bio</p>\"     } ] } ] , \"Sponsors\" : [ {     \"SponsorID\" : 101 ,     \"Name\" : \"Sherlock\" ,     \"ImageURI\" : \"images/SherlochTeckColorLogoVertical.png\" ,  \"SponsorURI\" : \"http://www.sherlocktech.com/\" } ]    } ]}";
            Program.dnm dnm = Program.LoadObjectFromJson<Program.dnm>(jsonData);
            Assert.NotNull(dnm.Events);
        }

        [Fact]
        public void LoadObjectFromJson_InvalidJson_Null()
        {
            string jsonData = "{ \"BadEvents\" : [ { \"BadEventID\" : 110 , \"EBadventDateTime\" : \"7/19/2012 6:30 PM\" , \"BadTitle\" : \"Introduction to MEF: the Managed Extensiblility Framework and jQuery Basics\"   } ]} ";
            Program.dnm dnm = Program.LoadObjectFromJson<Program.dnm>(jsonData);
            Assert.Null(dnm.Events);
        }

        [Fact(Skip = "Need to figure out how to determine where the file is on disk to test.")]
        public void GetJsonData_Path_Pass()
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            System.Diagnostics.Debug.WriteLine(path);
            var jsontest = Program.GetFileJsonData(path);
            Assert.NotNull(jsontest);
        }
        
    }

}
