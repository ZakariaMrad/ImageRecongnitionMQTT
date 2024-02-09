using Microsoft.EntityFrameworkCore;

public class ImageRecognitionContext : DbContext
{
    public ImageRecognitionContext(DbContextOptions<ImageRecognitionContext> options)
        : base(options) { }

    public DbSet<BeamModel> Beams { get; set; }
    public DbSet<Esp32Model> Esp32s { get; set; }
    public DbSet<ImageModel> Images { get; set; }
    public DbSet<ItemModel> Items { get; set; }

    public (ErrorModel? error, Esp32Model? data) AddEsp32(Esp32Model esp32)
    {
        var existingEsp32 = Esp32s.FirstOrDefault(e => e.MacAddress == esp32.MacAddress);
        if (existingEsp32 != null)
        {
            // Handle the case when an Esp32Model with the same MAC address already exists
            return (
                new ErrorModel
                {
                    Message = "An Esp32Model with the same MAC address already exists."
                },
                null
            );
        }

        // Add the new Esp32Model to the context
        Esp32s.Add(esp32);
        SaveChanges();

        return (null, esp32);
    }

    public (ErrorModel? error, Esp32Model data) GetEsp32ByIdOrMacAddress(string idOrMacAddress)
    {
        var esp32 = Esp32s.FirstOrDefault(e =>
            e.IdEsp32 == idOrMacAddress || e.MacAddress == idOrMacAddress
        );
        if (esp32 == null)
        {
            return (new ErrorModel { Message = "Esp32Model not found." }, new Esp32Model());
        }

        return (null, esp32);
    }

    public void AddImage(ImageModel image)
    {
        DeleteExpiredImages();

        // Add the new image to the context
        Images.Add(image);
        SaveChanges();
    }

    public List<ImageModel> GetImages()
    {
        DeleteExpiredImages();
        return Images.ToList();
    }

    private void DeleteExpiredImages()
    {
        var expiredImages = Images.Where(i => i.DeleteAt < DateTime.Now);
        ImageHelper.DeleteImages(expiredImages.Select(i => i.Path).ToList());
        Images.RemoveRange(expiredImages);
        SaveChanges();
    }

    public ImageModel? GetLatestImage()
    {
        DeleteExpiredImages();
        return Images.OrderByDescending(i => i.CreatedAt).FirstOrDefault();
    }
}
