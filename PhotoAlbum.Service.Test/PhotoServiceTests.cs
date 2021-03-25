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
        private PhotoService _sut;
        private IPhotoValues _photoValues;
        
        [SetUp]
        public void Setup()
        {
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
    }
}
