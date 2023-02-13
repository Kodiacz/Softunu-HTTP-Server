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

            IViewEngine viewEngine = new ShsViewEngine();
            var view = File.ReadAllText($"ViewTests/{fileName}.html");
            var actualResult = viewEngine.GetHtml(view, viewModel);

            var expectedResult = File.ReadAllText($"ViewTests/{fileName}.Result.html");

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestTemplateViewModel()
        {
            IViewEngine viewEngine = new ShsViewEngine();
            var actualResult = viewEngine.GetHtml(@"@foreach(var num in Model)
{
<span>@num</span>
}", new List<int> { 1, 2, 3 });
            var expectedResult = @"<span>1</span>
<span>2</span>
<span>3</span>
";
            Assert.Equal(expectedResult, actualResult);
        }
    }
 }