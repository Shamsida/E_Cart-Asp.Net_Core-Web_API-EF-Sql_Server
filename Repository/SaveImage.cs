namespace E_cart.Repository
{
    public class SaveImage
    {
        private readonly IWebHostEnvironment _environment;

        public SaveImage(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImages(IFormFile image, string directory)
        {
            if (image == null)
            {
                throw new ArgumentException("Image file is required");
            }

            var ext = Path.GetExtension(image.FileName);
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(ext))
            {
                throw new ArgumentException($"Extension not allowed, Only {allowedExtensions} are allowed");
            }

            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, directory);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string uniqueString = Guid.NewGuid().ToString();
            var newfileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newfileName);

            using (var stream = new FileStream(fileWithPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return newfileName;
        }
    }
}
