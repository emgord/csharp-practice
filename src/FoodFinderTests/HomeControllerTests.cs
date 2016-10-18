using FoodFinder;
using FoodFinder.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FoodFinderTests
{
    class FakeMapService : IMapService
    {
        private SearchResult _result;

        public FakeMapService(SearchResult result)
        {
            _result = result;
        }

        public SearchResult FindRoute(string start, string end)
        {
            return _result;
        }
    }

    public class HomeControllerTests
    {
        [Fact]
        public void TestSearch()
        {
            var searchResult = new SearchResult(123.4, 89.6, 124.5, 90.1);
            var mapService = new FakeMapService(searchResult);
            var subject = new HomeController(mapService);
            var result = (ViewResult) subject.Search("foo", "bar");
            var viewModel = (SearchResult) result.Model;
            Assert.Same(viewModel, searchResult);
        }
    }
}