using System;

namespace PhotoAlbum
{
    public interface IApplication
    {
        void Run();
    }

    public class Application : IApplication
    {
        private readonly IPhotoController _controller;
        private readonly IConsoleWrapper _console;

        public Application(IPhotoController controller = null, IConsoleWrapper console = null)
        {
            _controller = controller ?? new PhotoController();
            _console = console ?? new ConsoleWrapper();
        }

        public void Run()
        {
            try
            {
                _controller.PhotoSearch();
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
