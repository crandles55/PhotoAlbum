using System;
using System.Linq;
using PhotoAlbum.Data;
using PhotoAlbum.Service;

namespace PhotoAlbum
{
    class Program
    {
        private static void Main(string[] args)
        {
            var apiClient = new ApiClient();
            var photoValues = new PhotoValues(apiClient);
            var photoService = new PhotoService(photoValues);

            try
            {
                PhotoSearch(photoService);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        private static void PhotoSearch(IPhotoService photoService)
        {
            while (true)
            {
                Console.Write("Photo-Album: ");

                var userInput = Console.ReadLine();

                var processedUserInput = photoService.ProcessUserInput(userInput);

                if (processedUserInput.Equals("Please Enter A Single Number"))
                {
                    Console.WriteLine(processedUserInput);
                    continue;
                }

                var photos = photoService.GatherPossiblePhotosAsync(processedUserInput);

                if (!photos.Result.Any())
                {
                    Console.WriteLine($"No Photos Were Found With Photo-AlbumId: {processedUserInput}");
                    continue;
                }
                
                foreach (var photo in photos.Result)
                {
                    Console.WriteLine($"[{photo.Id}] {photo.Title}");
                }

                Restart(photoService);
                
                break;
            }
        }

        private static void Restart(IPhotoService photoService)
        {
            while (true)
            {
                Console.WriteLine("Would you like to Search for Another Album?");
                Console.Write("Please Enter Y or N?");

                var input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "y":
                        PhotoSearch(photoService);
                        break;
                    case "n":
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid Entry");
                        continue;
                }
                break;
            }
        }
    }
}
