using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoAlbum.Data;
using PhotoAlbum.Data.models;

namespace PhotoAlbum.Service
{
    public interface IPhotoService
    {
        Task<IEnumerable<PhotoDto>> GatherPossiblePhotosAsync(string userInput);
    }
    
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoValues _photoValues;
        
        public PhotoService(IPhotoValues photoValues)
        {
            _photoValues = photoValues;
        }


        public async Task<IEnumerable<PhotoDto>> GatherPossiblePhotosAsync(string userInput)
        {
            throw new System.NotImplementedException();
        }

        public static UserInput ProcessUserInput(string userInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
