using System;
using System.Threading.Tasks;
using PhotoAlbum.Data.models;

namespace PhotoAlbum.Data
{
    public interface IPhotoValues
    {
        Task<Photo> GetPhotoValues();
    }
    
    public class PhotoValues : IPhotoValues
    {
        public Task<Photo> GetPhotoValues()
        {
            throw new NotImplementedException();
        }
    }
}