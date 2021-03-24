using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoAlbum.Data;
using PhotoAlbum.Data.models;

namespace PhotoAlbum.Service
{
    public interface IPhotoService
    {
        Task<IEnumerable<Photo>> GatherPossiblePhotosAsync();
    }
    
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoValues _photoValues;
        
        public PhotoService(IPhotoValues photoValues)
        {
            _photoValues = photoValues;
        }


        public async Task<IEnumerable<Photo>> GatherPossiblePhotosAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}