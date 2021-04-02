using System;
using System.Linq;
using PhotoAlbum.Service;

namespace PhotoAlbum
{
    public interface IPhotoController
    {
        void PhotoSearch();
        void Restart();
    }

    public class PhotoController : IPhotoController
    {
        private readonly IPhotoController _self;
        private readonly IPhotoService _service;
        private readonly IConsoleWrapper _console;

        public PhotoController(IPhotoController self = null,
                           IPhotoService service = null,
                           IConsoleWrapper console = null)
        {
            _self = self ?? this;
            _service = service ?? new PhotoService();
            _console = console ?? new ConsoleWrapper();
        }

        public void PhotoSearch()
        {
            _console.Write("Photo-Album: ");

            var userInput = Console.ReadLine();

            var processedUserInput = _service.ProcessUserInput(userInput);

            if (processedUserInput.Equals("Please Enter A Single Number"))
            {
                _console.WriteLine(processedUserInput);
                _self.PhotoSearch();
                return;
            }

            var photos = _service.GatherPossiblePhotosAsync(processedUserInput);

            if (!photos.Result.Any())
            {
                _console.WriteLine($"No Photos Were Found With Photo-AlbumId: {processedUserInput}");
                _self.PhotoSearch();
                return;
            }

            foreach (var photo in photos.Result)
            {
                _console.WriteLine($"[{photo.Id}] {photo.Title}");
            }

            _self.Restart();
        }

        public void Restart()
        {
            while (true)
            {
                _console.WriteLine("Would you like to Search for Another Album?");
                _console.Write("Please Enter Y or N?");

                var input = _console.ReadLine();

                switch (input.ToLower())
                {
                    case "y":
                        _self.PhotoSearch();
                        break;
                    case "n":
                        _console.WriteLine("Goodbye!");
                        break;
                    default:
                        _console.WriteLine("Invalid Entry");
                        continue;
                }
                break;
            }
        }
    }
}
