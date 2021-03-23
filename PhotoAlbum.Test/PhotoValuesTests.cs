using System;
using System.Collections.Generic;
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
        private const string Uri = "https://jsonplaceholder.typicode.com/photos";

        public PhotoValuesTests()
        {
            _apiClient = Substitute.For<IApiClient>();
            _sut = new PhotoValues(_apiClient);
        }
        
        [SetUp]
        public void Setup()
        {
            
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

            _apiClient.GetAsync<List<Photo>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri)).Returns(expected);
            
            var actual = _sut.GetPhotoValues();

            actual.Should().Be(expected);
        }
    }
}
