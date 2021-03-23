﻿namespace PhotoAlbum.Data.models
{
    public class Photo
    {
        public int AlbumId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThunbnailUrl { get; set; }
    }
}
