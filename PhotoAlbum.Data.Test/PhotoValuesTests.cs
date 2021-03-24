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
        private PhotoValues _sut;
        private IApiClient _apiClient;
        private const string Uri = "https://jsonplaceholder.typicode.com/photos?";

        [SetUp]
        public void SetUp()
        {
            _apiClient = Substitute.For<IApiClient>();
            _sut = new PhotoValues(_apiClient);
        }

        [Test]
        public void WhenGetPhotoValuesIsCalledThenReturnListOfPhotos()
        {
            var expected = new List<PhotoDto>
            {
                new PhotoDto
                {
                    AlbumId = 1,
                    Id = 1,
                    ThunbnailUrl = "ThumbnailTest",
                    Title = "TestTitle",
                    Url = "TestUrl"
                },
                new PhotoDto
                {
                    AlbumId = 2,
                    Id = 2,
                    ThunbnailUrl = "ThumbnailTest2",
                    Title = "TestTitle2",
                    Url = "TestUrl2"
                }
            };

            var fullUri = new Uri(Uri);

            _apiClient.GetAsync<IEnumerable<PhotoDto>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri)).Returns(expected);

            var actual = _sut.GetPhotoValuesAsync(null, null, null, null, null);

            actual.Result.ToList().Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void WhenGetPhotoValuesIsCalledButNoPhotosAreFoundThenReturnEmptyList()
        {
            var expected = new List<PhotoDto>();

            var fullUri = new Uri(Uri);

            _apiClient.GetAsync<IEnumerable<PhotoDto>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri)).Returns(expected);

            var actual = _sut.GetPhotoValuesAsync(null, null, null, null, null);

            actual.Result.ToList().Should().BeEquivalentTo(expected);
        }

        [TestCase(1,1,"test", "testUrl", "Test thumbnailUrl", ExpectedResult = "https://jsonplaceholder.typicode.com/photos?albumId=1&id=1&title=test&url=testUrl&thumbnailUrl=Test thumbnailUrl&")]
        [TestCase(null,1,"test", "testUrl", "Test thumbnailUrl", ExpectedResult = "https://jsonplaceholder.typicode.com/photos?id=1&title=test&url=testUrl&thumbnailUrl=Test thumbnailUrl&")]
        [TestCase(1,null,"test", "testUrl", "Test thumbnailUrl", ExpectedResult = "https://jsonplaceholder.typicode.com/photos?albumId=1&title=test&url=testUrl&thumbnailUrl=Test thumbnailUrl&")]
        [TestCase(1,1,null, "testUrl", "Test thumbnailUrl", ExpectedResult = "https://jsonplaceholder.typicode.com/photos?albumId=1&id=1&url=testUrl&thumbnailUrl=Test thumbnailUrl&")]
        [TestCase(1,1,"test", null, "Test thumbnailUrl", ExpectedResult = "https://jsonplaceholder.typicode.com/photos?albumId=1&id=1&title=test&thumbnailUrl=Test thumbnailUrl&")]
        [TestCase(1,1,"test", "testUrl", null, ExpectedResult = "https://jsonplaceholder.typicode.com/photos?albumId=1&id=1&title=test&url=testUrl&")]
        [TestCase(null,null,null, null, null, ExpectedResult = "https://jsonplaceholder.typicode.com/photos?")]
        [Test]
        public string WhenBuildingPhotoUriWithVaryingInputsReturnCorrectString(int? albumId, int? id, string title, string url, string thumbnailUrl)
        {
            var actual = _sut.PhotoUriBuilder(albumId, id, title, url, thumbnailUrl);

            return actual.ToString();
        }
        
    }
}
