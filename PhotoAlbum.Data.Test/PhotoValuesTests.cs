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
        private Random _randy;
        private PhotoValues _sut;
        private IApiClient _apiClient;
        private const string Uri = "https://jsonplaceholder.typicode.com/photos?albumId=";

        [SetUp]
        public void SetUp()
        {
            _randy = new Random();
            _apiClient = Substitute.For<IApiClient>();
            _sut = new PhotoValues(_apiClient);
        }

        [Test]
        public void WhenGetPhotoValuesIsCalledWithNullUserInputThenReturnCompleteListOfPhotos()
        {
            var userInput = RandomPositiveInt().ToString();
            var expected = RandomPhotos(3);
            var fullUri = new Uri(Uri + userInput);
            _apiClient.GetAsync<IEnumerable<PhotoDto>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri)).Returns(expected);

            var actual = _sut.GetPhotoValuesAsync(userInput);

            actual.Result.ToList().Should().BeEquivalentTo(expected);
            _apiClient.Received(1).GetAsync<IEnumerable<PhotoDto>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri));
        }
        
        [Test]
        public void WhenGetPhotoValuesIsCalledButNoPhotosAreFoundThenReturnEmptyList()
        {
            //this test doesn't actually test much. The 'fullUri' wasn't what was actually returned based on your input.
            var userInput = "photo-album -1";

            var expected = new List<PhotoDto>();

            var fullUri = new Uri(Uri);

            _apiClient.GetAsync<IEnumerable<PhotoDto>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri)).Returns(expected);

            var actual = _sut.GetPhotoValuesAsync(userInput);

            // Adding verifies on the reveived methods would show that this never actually called. Whay your reveive for
            // actual is just the default value. Uncomment the below line to see that above call is never received. 
            // _apiClient.Received(1).GetAsync<IEnumerable<PhotoDto>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri));
            actual.Result.ToList().Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void GivenGetPhotoValuesAsyncIsCalledWithValidInput_WhenGetPhotoValuesIsCalledButNoPhotosAreFound_ThenReturnEmptyList()
        {
            var userInput = RandomString();
            var expected = new List<PhotoDto>();
            var fullUri = new Uri(Uri + userInput);
            _apiClient.GetAsync<IEnumerable<PhotoDto>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri)).Returns(expected);

            var actual = _sut.GetPhotoValuesAsync(userInput);

            _apiClient.Received(1).GetAsync<IEnumerable<PhotoDto>>(Arg.Is<Uri>(u => u.AbsoluteUri == fullUri.AbsoluteUri));
            actual.Result.ToList().Should().BeEquivalentTo(expected);
        }

        
        [Test]
        public void WhenBuildingPhotoUriWithVaryingInputsReturnCorrectString()
        {
            var userInput = RandomString();
            var expected = Uri + userInput;
            
            var actual = _sut.PhotoUriBuilder(userInput);

            actual.ToString().Should().BeEquivalentTo(expected);
        }

        private List<PhotoDto> RandomPhotos(int count)
        {
            var photos = new List<PhotoDto>();

            for (var i = 0; i < count; i++)
            {
                photos.Add(RandomPhoto());
            }

            return photos;
        }

        private PhotoDto RandomPhoto()
        {
            return new PhotoDto
            {
                AlbumId = RandomPositiveInt(),
                Id = RandomPositiveInt(),
                ThunbnailUrl = RandomString(),
                Title = RandomString(),
                Url = RandomString()
            };
        }

        private string RandomString()
        {
            return System.Guid.NewGuid().ToString();
        }

        private int RandomPositiveInt()
        {
            return _randy.Next(1, int.MaxValue);
        }

        private int RandomNegativeInt()
        {
            return _randy.Next(int.MinValue, -1);
        }

        private int RandomInt()
        {
            return _randy.Next(int.MinValue, int.MaxValue);
        }
    }
}
