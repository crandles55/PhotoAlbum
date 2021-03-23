using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoAlbum.Data.models;

namespace PhotoAlbum.Data
{
    public interface IPhotoValues
    {
        Task<List<Photo>> GetPhotoValues();
    }
    
    public class PhotoValues : IPhotoValues
    {
        private readonly IApiClient _apiClient;

        public PhotoValues(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
        public Task<List<Photo>> GetPhotoValues()
        {
            throw new NotImplementedException();
        }
    }
}
