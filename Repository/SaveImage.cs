namespace E_cart.Repository
{
    public class SaveImage
    {
        private readonly IWebHostEnvironment environment;

        public SaveImage(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<string> SaveImages(IFormFile image, string directory)
        {
            if (image == null)
            {
                throw new ArgumentException("Image file is required");
            }

            var ext = Path.GetExtension(image.FileName);
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtension.Contains(ext))
            {
                throw new ArgumentException($"Extension not allowed, Only {allowedExtension} is allowed");
            }

            var contentPath = this.environment.ContentRootPath;
            var imagePath = Path.Combine(contentPath, directory);
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            string uniqueFileName = Guid.NewGuid().ToString();
            var fileName = uniqueFileName + ext;
            var fileWithPath = Path.Combine(imagePath, fileName);

            using (var stream = new FileStream(fileWithPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
