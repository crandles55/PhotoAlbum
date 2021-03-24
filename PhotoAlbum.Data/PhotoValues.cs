using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoAlbum.Data.models;

namespace PhotoAlbum.Data
{
    public interface IPhotoValues
    {
        Task<IEnumerable<Photo>> GetPhotoValuesAsync(int? albumId, int? id, string title, string url, string thumbnailUrl);
        Uri PhotoUriBuilder(int? albumId, int? id, string title, string url, string thumbnailUrl);
    }
    
    public class PhotoValues : IPhotoValues
    {
        private readonly IApiClient _apiClient;

        public PhotoValues(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
        public async Task<IEnumerable<Photo>> GetPhotoValuesAsync(int? albumId, int? id, string title, string url, string thumbnailUrl)
        {
            var response = await _apiClient.GetAsync<IEnumerable<Photo>>(PhotoUriBuilder(albumId, id, title, url, thumbnailUrl));

            return response;
        }

        public Uri PhotoUriBuilder(int? albumId, int? id, string title, string url, string thumbnailUrl)
        {
            var uri = "https://jsonplaceholder.typicode.com/photos?";

            uri = albumId == null ? uri : uri + $"albumId={albumId}&";
            
            uri = id == null ? uri : uri + $"id={id}&";
            
            uri = title == null ? uri : uri + $"title={title}&";
            
            uri = url == null ? uri : uri + $"url={url}&";
            
            uri = thumbnailUrl == null ? uri : uri + $"thumbnailUrl={thumbnailUrl}&";

            return new Uri(uri);
        }
    }
}
