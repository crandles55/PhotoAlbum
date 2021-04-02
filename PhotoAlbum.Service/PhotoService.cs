using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoAlbum.Data;
using PhotoAlbum.Data.models;

namespace PhotoAlbum.Service
{
    public interface IPhotoService
    {
        Task<IEnumerable<PhotoDto>> GatherPossiblePhotosAsync(string userInput);
        string ProcessUserInput(string userInput);
    }
    
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoValues _photoValues;
        
        public PhotoService(IPhotoValues photoValues = null)
        {
            _photoValues = photoValues ?? new PhotoValues();
        }


        public async Task<IEnumerable<PhotoDto>> GatherPossiblePhotosAsync(string userInput)
        {
            return await _photoValues.GetPhotoValuesAsync(userInput);
        }

        public string ProcessUserInput(string userInput)
        {
            if (!int.TryParse(userInput, out var albumId))
            {
                return "Please Enter A Single Number";
            }

            return albumId.ToString();
        }
    }
}
