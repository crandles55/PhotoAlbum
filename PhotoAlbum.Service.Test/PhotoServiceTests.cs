using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using PhotoAlbum.Data;
using PhotoAlbum.Data.models;

namespace PhotoAlbum.Service.Test
{
    public class PhotoServiceTests
    {
        private Random _randy;
        private PhotoService _sut;
        private IPhotoValues _photoValues;
        private const string ErrorMessage = "Please Enter A Single Number";

        [SetUp]
        public void Setup()
        {
            _randy = new Random();
            _photoValues = Substitute.For<IPhotoValues>();
            _sut = new PhotoService(_photoValues);
        }

        [Test]
        public void GivenCorrectUserInputForAlbumIdReturnCorrectPhoto()
        {
            const string albumId = "1";

            var expected = new List<PhotoDto>
            {
                new PhotoDto
                {
                    AlbumId = 1,
                    Id = 1,
                    Title = "Test Title",
                    Url = "Test Url",
                    ThunbnailUrl = "Test ThumbnailUrl"
                }
            };

            _photoValues.GetPhotoValuesAsync(albumId)
                .Returns(expected);

            var actual = _sut.GatherPossiblePhotosAsync(albumId);

            actual.Result.ToList().Should().BeEquivalentTo(expected);
        }
        
        [TestCase("1", ExpectedResult = "1")]
        [TestCase("12", ExpectedResult = "12")]
        [TestCase("13", ExpectedResult = "13")]
        [TestCase("135", ExpectedResult = "135")]
        [TestCase("11 1", ExpectedResult = "Please Enter A Single Number")]
        [TestCase("51 11", ExpectedResult = "Please Enter A Single Number")]
        [TestCase("Test", ExpectedResult = "Please Enter A Single Number")]
        [TestCase("", ExpectedResult = "Please Enter A Single Number")]
        [Test]
        public string GivenMultipleUserInputsForAlbumIdReturnCorrectAlbumId(string userInput)
        {
            var actual = _sut.ProcessUserInput(userInput);

            return actual;
        }

        [Test]
        public void GivenValidUserInputsForAlbumId_ReturnCorrectAlbumId()
        {
            var userInput = RandomInt().ToString();
            var actual = _sut.ProcessUserInput(userInput);

            actual.Should().Be(userInput);
        }

        [Test]
        public void GivenMultipleIntegers_WhenProcessed_ReturnErrorMessage()
        {
            var userInput = RandomPositiveInt().ToString() + " " + RandomPositiveInt().ToString();
            var actual = _sut.ProcessUserInput(userInput);

            actual.Should().Be(ErrorMessage);
        }

        [Test]
        public void GivenNonNumericValue_WhenProcessed_ReturnErrorMessage()
        {
            var userInput = RandomString();
            var actual = _sut.ProcessUserInput(userInput);

            actual.Should().Be(ErrorMessage);
        }

        [Test]
        public void GivenDecimalValue_WhenProcessed_ReturnErrorMessage()
        {
            var userInput = RandomPositiveInt().ToString() + "." + RandomPositiveInt().ToString();
            var actual = _sut.ProcessUserInput(userInput);

            actual.Should().Be(ErrorMessage);
        }

        [Test]
        public void GivenEmptyValue_WhenProcessed_ReturnErrorMessage()
        {
            var userInput = string.Empty;
            var actual = _sut.ProcessUserInput(userInput);

            actual.Should().Be(ErrorMessage);
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
