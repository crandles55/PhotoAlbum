using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private PhotoValues _photoValues;
        
        [SetUp]
        public void Setup()
        {
            _photoValues = Substitute.For<PhotoValues>();
            _sut = new PhotoService(_photoValues);
        }

        [Test]
        public void GivenCorrectUserInputForAlbumIdReturnCorrectPhoto()
        {
            const string input = "Photo-Album 1";

            var userInput = new UserInput
            {
                AlbumId = 1,
                Id = null,
                Title = null,
                Url = null,
                ThunbnailUrl = null
            };

            var expected = new Task<IEnumerable<PhotoDto>>();

            PhotoService.ProcessUserInput(input).Returns(userInput);

            _photoValues.GetPhotoValuesAsync(expected.AlbumId, expected.Id, expected.Title, expected.Url,
                expected.ThunbnailUrl).Returns(expected);

            var actual = _sut.GatherPossiblePhotosAsync(input);

            actual.Should().Be();
        }
    }
}
