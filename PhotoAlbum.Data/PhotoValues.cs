using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoAlbum.Data.models;

namespace PhotoAlbum.Data
{
    public interface IPhotoValues
    {
        Task<IEnumerable<PhotoDto>> GetPhotoValuesAsync(string albumId);
        Uri PhotoUriBuilder(string albumId);
    }
    
    public class PhotoValues : IPhotoValues
    {
        private readonly IApiClient _apiClient;

        public PhotoValues(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
        public async Task<IEnumerable<PhotoDto>> GetPhotoValuesAsync(string albumId) => await _apiClient.GetAsync<IEnumerable<PhotoDto>>(PhotoUriBuilder(albumId));

        public Uri PhotoUriBuilder(string albumId)
            => new($"https://jsonplaceholder.typicode.com/photos?albumId={albumId}");
        
    }
}
