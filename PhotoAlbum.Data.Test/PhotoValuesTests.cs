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
        private const string Uri = "https://jsonplaceholder.typicode.com/photos?albumId=";

        [SetUp]
        public void SetUp()
        {
            _apiClient = Substitute.For<IApiClient>();
            _sut = new PhotoValues(_apiClient);
        }

        [Test]
        public void WhenGetPhotoValuesIsCalledWithNullUserInputThenReturnCompleteListOfPhotos()
        {

            var userInput = "1";
            
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
                    AlbumId = 1,
                    Id = 2,
                    ThunbnailUrl = "ThumbnailTest2",
                    Title = "TestTitle2",
                    Url = "TestUrl2"
                }
            };

            var fullUri = new Uri(Uri + userInput);

            _apiClient.GetAsync<IEnumerable<PhotoDto>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri)).Returns(expected);

            var actual = _sut.GetPhotoValuesAsync(userInput);

            actual.Result.ToList().Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void WhenGetPhotoValuesIsCalledButNoPhotosAreFoundThenReturnEmptyList()
        {
            var userInput = "photo-album -1";

            var expected = new List<PhotoDto>();

            var fullUri = new Uri(Uri);

            _apiClient.GetAsync<IEnumerable<PhotoDto>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri)).Returns(expected);

            var actual = _sut.GetPhotoValuesAsync(userInput);

            actual.Result.ToList().Should().BeEquivalentTo(expected);
        }

        
        [Test]
        public void WhenBuildingPhotoUriWithVaryingInputsReturnCorrectString()
        {
            var userInput = "1";

            var expected = Uri + userInput;
            
            var actual = _sut.PhotoUriBuilder(userInput);

            actual.ToString().Should().BeEquivalentTo(expected);
        }
        
    }
}
