namespace SoftuniHTTPServer.MvcFramework.Tests
{
    using SoftuniHTTPServer.MvcFramework.ViewEngine;

    public class SHSViewEngineTests
    {
        // This attribute is comming from XUnit and it allows parameters
        // to the test method
        [Theory]
        // happy path
        // interesting cases
        // complex cases or combinations of tests
        // code coverage 100%l
        [InlineData("CleanHtml")]
        [InlineData("Foreach")]
        [InlineData("IfElseFor")]
        [InlineData("ViewModel")]
        public void TestGetHtml(string fileName)
        {
            var viewModel = new TestViewModel()
            {
                DateOfBirth = new DateTime(2019, 6, 1),
                Name = "Doggo Arghentino",
                Price = 12345.67M,
            };

            IViewEngine viewEngine = new SHSViewEngine();
            var view = File.ReadAllText($"ViewTests/{fileName}.html");
            var result = viewEngine.GetHtml(view, viewModel);

            var expectedResult = File.ReadAllText($"ViewTests/{fileName}.Result.html");

            Assert.Equal(expectedResult, result);
        }

        public class TestViewModel
        {
            public string Name { get; set; }

            public decimal Price { get; set; }

            public DateTime DateOfBirth { get; set; }
        }
    }
}