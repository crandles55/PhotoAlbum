using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using PhotoAlbum.Data;
using PhotoAlbum.Data.models;

namespace PhotoAlbum.Test
{
    public class PhotoValuesTests
    {
        private readonly PhotoValues _sut;
        private readonly IApiClient _apiClient;
        private const string Uri = "https://jsonplaceholder.typicode.com/photos?";

        public PhotoValuesTests()
        {
            _apiClient = Substitute.For<IApiClient>();
            _sut = new PhotoValues(_apiClient);
        }

        [Test]
        public void WhenGetPhotoValuesIsCalledThenReturnListOfPhotos()
        {
            var expected = new List<Photo>
            {
                new Photo
                {
                    AlbumId = 1,
                    Id = 1,
                    ThunbnailUrl = "ThumbnailTest",
                    Title = "TestTitle",
                    Url = "TestUrl"
                },
                new Photo
                {
                    AlbumId = 2,
                    Id = 2,
                    ThunbnailUrl = "ThumbnailTest2",
                    Title = "TestTitle2",
                    Url = "TestUrl2"
                }
            };

            var fullUri = new Uri(Uri);

            _apiClient.GetAsync<IEnumerable<Photo>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri)).Returns(expected);

            var actual = _sut.GetPhotoValuesAsync(null, null, null, null, null);

            actual.Result.ToList().Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void WhenGetPhotoValuesIsCalledButNoPhotosAreFoundThenReturnEmptyList()
        {
            var expected = new List<Photo>();

            var fullUri = new Uri(Uri);

            _apiClient.GetAsync<IEnumerable<Photo>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri)).Returns(expected);

            var actual = _sut.GetPhotoValuesAsync(null, null, null, null, null);

            actual.Result.ToList().Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void WhenBuildingPhotoUriStringWithAllParametersReturnCorrectString()
        {
            const int albumId = 1;
            const int id = 1;
            const string title = "Test Title";
            const string url = "Test Url";
            const string thumbnailUrl = "Test Thumbnail Url";
            
            var expected =
                $"https://jsonplaceholder.typicode.com/photos?albumId={albumId}&id={id}&title={title}&url={url}&thumbnailUrl={thumbnailUrl}&";

            var actual = _sut.PhotoUriBuilder(albumId, id, title, url, thumbnailUrl);

            actual.ToString().Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void WhenBuildingPhotoUriStringWithNoParametersReturnCorrectString()
        {
            const string title = null;
            const string url = null;
            const string thumbnailUrl = null;
            
            const string expected = "https://jsonplaceholder.typicode.com/photos?";

            var actual = _sut.PhotoUriBuilder(null, null, title, url, thumbnailUrl);

            actual.ToString().Should().BeEquivalentTo(expected);
        }
        
        [TestCase(1,1,"test", "testUrl", "Test thumbnailUrl", ExpectedResult = "https://jsonplaceholder.typicode.com/photos?albumId=1&id=1&title=test&url=testUrl&thumbnailUrl=Test thumbnailUrl&")]
        [Theory]
        public string WhenBuildingPhotoUriWithOnlyATitleReturnCorrectString(int? albumId, int? id, string title, string url, string thumbnailUrl)
        {
            var expected =
                $"https://jsonplaceholder.typicode.com/photos?";

            var actual = _sut.PhotoUriBuilder(albumId, id, title, url, thumbnailUrl);

            return actual.ToString();
        }
        
    }
}
