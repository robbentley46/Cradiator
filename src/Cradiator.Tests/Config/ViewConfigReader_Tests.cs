using System.IO;
using System.Linq;
using Cradiator.Config;
using NUnit.Framework;
using Shouldly;

namespace Cradiator.Tests.Config
{
    [TestFixture]
    public class ViewConfigReader_Tests
    {
        ViewSettingsParser _parser;

        const string XML = "<configuration>" +
                                "<views>" +
                                    @"<view url=""http://url1"" 
                                        skin=""Grid"" 
                                        project-regex=""v5.*"" 
                                        category-regex="".*""
                                        server-regex="".*""  
                                        name=""whatever""
                                        showOnlyBroken=""false""
                                        showServerName=""false""
                                        showOutOfDate=""false""
                                        showBuildAge=""false""
                                        outOfDateDifferenceInMinutes=""0""/> " +
                                "</views>" +
                            "</configuration>";

        [SetUp]
        public void SetUp()
        {
            _parser = new ViewSettingsParser(new StringReader(XML));
        }

        [Test]
        public void can_read_view_from_xml()
        {
            var views = _parser.ParseXml();
            views.Count().ShouldBe(1);

            var view1 = views.First();

            view1.URL.ShouldBe("http://url1");
            view1.SkinName.ShouldBe("Grid");
            view1.ProjectNameRegEx.ShouldBe("v5.*");
            view1.CategoryRegEx.ShouldBe(".*");
            view1.ShowServerName.ShouldBe(false);
        }

        [Test]
        public void can_read_show_build_age_from_xml()
        {
            var views = _parser.ParseXml();
            views.Count().ShouldBe(1);

            var view1 = views.First();

            view1.ShowBuildAge.ShouldBe(false);
        }

        [Test]
        public void can_read_then_write_modified_view_to_xml()
        {
            var views = _parser.ParseXml();
            var xmlModified = _parser.CreateUpdatedXml(new ViewSettings
                              {
                                URL = "http://new",
                                ProjectNameRegEx = "[a-z]",  
                                CategoryRegEx = "[1-9]",  
                                ServerNameRegEx = "^(http://).*",
                                SkinName = "StackPhoto",  
                                ViewName = "NewView",
                                ShowOnlyBroken = true,
                                ShowServerName = true,
                                ShowOutOfDate = true,
                                OutOfDateDifferenceInMinutes = 45,
                              });

            _parser = new ViewSettingsParser(new StringReader(xmlModified));

            views = _parser.ParseXml();
            var view1 = views.First();

            view1.URL.ShouldBe("http://new");
            view1.ProjectNameRegEx.ShouldBe("[a-z]");
            view1.CategoryRegEx.ShouldBe("[1-9]");
            view1.ServerNameRegEx.ShouldBe("^(http://).*");
            view1.SkinName.ShouldBe("StackPhoto");
            view1.ViewName.ShouldBe("NewView");
            view1.ShowOnlyBroken.ShouldBe(true);
            view1.ShowServerName.ShouldBe(true);
            view1.ShowOutOfDate.ShouldBe(true);
            view1.OutOfDateDifferenceInMinutes.ShouldBe(45);
        }
        
        [Test]
        public void can_read_then_write_modified_show_build_age_value_to_xml()
        {
            var views = _parser.ParseXml();
            var xmlModified = _parser.CreateUpdatedXml(new ViewSettings
                              {
                                URL = "http://new",
                                ProjectNameRegEx = "[a-z]",  
                                CategoryRegEx = "[1-9]",  
                                ServerNameRegEx = "^(http://).*",
                                SkinName = "StackPhoto",  
                                ViewName = "NewView",
                                ShowOnlyBroken = true,
                                ShowServerName = true,
                                ShowOutOfDate = true,
                                ShowBuildAge = true,
                                OutOfDateDifferenceInMinutes = 45,
                              });

            _parser = new ViewSettingsParser(new StringReader(xmlModified));

            views = _parser.ParseXml();
            var view1 = views.First();

            view1.ShowBuildAge.ShouldBe(true);
        }
    }
}