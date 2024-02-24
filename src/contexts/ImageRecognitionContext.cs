using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class ImageRecognitionContext : DbContext
{
    public ImageRecognitionContext(DbContextOptions<ImageRecognitionContext> options)
        : base(options) { }

    public DbSet<BeamModel> Beams { get; set; }
    public DbSet<Esp32Model> Esp32s { get; set; }
    public DbSet<ImageModel> Images { get; set; }
    public DbSet<ItemModel> Items { get; set; }
    public DbSet<BeamItemModel> BeamItems { get; set; }

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

    public (ErrorModel? error, Esp32Model? data) GetEsp32ByIdOrMacAddress(string idOrMacAddress)
    {
        var esp32 = Esp32s.FirstOrDefault(e =>
            e.IdEsp32 == idOrMacAddress || e.MacAddress == idOrMacAddress
        );
        if (esp32 == null)
        {
            return (new ErrorModel { Message = "Esp32Model not found." }, null);
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
        var expiredImages = Images.Where(i => !BeamItems.Any(bi => bi.IdImage == i.IdImage));
        Images.RemoveRange(expiredImages);
        ImageHelper.DeleteImages(expiredImages.Select(i => i.Path).ToList());
        SaveChanges();
    }

    public ImageModel? GetLatestImage()
    {
        DeleteExpiredImages();
        return Images.OrderByDescending(i => i.CreatedAt).FirstOrDefault();
    }

    public (ErrorModel? error, BeamModel? data) AddBeam(BeamModel beam)
    {
        var existingBeam = Beams.FirstOrDefault(b =>
            b.IdBeam == beam.IdBeam || b.Name == beam.Name
        );
        if (existingBeam != null)
        {
            // Handle the case when an Esp32Model with the same MAC address already exists
            return (new ErrorModel { Message = "A Beam with the same Name already exists." }, null);
        }

        // Add the new Esp32Model to the context
        Beams.Add(beam);
        SaveChanges();

        return (null, beam);
    }

    public (ErrorModel? error, BeamModel? data) GetBeamByIdOrName(string idOrName)
    {
        var beam = Beams.FirstOrDefault(b => b.IdBeam == idOrName || b.Name == idOrName);
        if (beam == null)
        {
            return (new ErrorModel { Message = "Beam not found." }, null);
        }

        return (null, beam);
    }

    public List<BeamModel> GetBeams()
    {
        return Beams
            .Select(b => new BeamModel
            {
                IdBeam = b.IdBeam,
                Name = b.Name,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                CanBeSaved = b.CanBeSaved,
                MarkerValue = b.MarkerValue,
                MarkerValueBase64 = b.MarkerValueBase64,
                //Select the items that BeamItems has as idItem if it has the same idBeam as the current beam
                Items = BeamItems
                    .Where(bi => bi.IdBeam == b.IdBeam)
                    .Select(bi => new ItemModel
                    {
                        IdItem = bi.IdItem,
                        Name = Items.FirstOrDefault(i => i.IdItem == bi.IdItem).Name,
                        CreatedAt = Items.FirstOrDefault(i => i.IdItem == bi.IdItem).CreatedAt,
                        UpdatedAt = Items.FirstOrDefault(i => i.IdItem == bi.IdItem).UpdatedAt,
                        MarkerValue = Items.FirstOrDefault(i => i.IdItem == bi.IdItem).MarkerValue,
                        MarkerValueBase64 = Items
                            .FirstOrDefault(i => i.IdItem == bi.IdItem)
                            .MarkerValueBase64
                    })
                    .ToList(),
                BeamItems = BeamItems
                    .Select(bi => new BeamItemModel
                    {
                        IdBeamItem = bi.IdBeamItem,
                        IdItem = bi.IdItem,
                        IdImage = bi.IdImage,
                        IdBeam = bi.IdBeam
                    })
                    .ToList()
            })
            .ToList();
    }

    public (ErrorModel? error, ItemModel? data) CreateItem(ItemModel item)
    {
        var existingItem = Items.FirstOrDefault(i => i.IdItem == item.IdItem);
        if (existingItem != null)
        {
            // Handle the case when an ItemModel with the same IdItem already exists
            return (
                new ErrorModel { Message = "An Item with the same IdItem already exists." },
                null
            );
        }

        // Add the new ItemModel to the context
        Items.Add(item);
        SaveChanges();

        return (null, item);
    }

    public List<ItemModel> GetItems()
    {
        return Items
            .Select(a => new ItemModel
            {
                IdItem = a.IdItem,
                Name = a.Name,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
                MarkerValue = a.MarkerValue,
                MarkerValueBase64 = a.MarkerValueBase64,
                //Select the beams that BeamItems has as idBeam if it has the same idItem as the current item
                Beams = BeamItems
                    .Where(bi => bi.IdItem == a.IdItem)
                    .Select(bi => new BeamModel
                    {
                        IdBeam = bi.IdBeam,
                        Name = Beams.FirstOrDefault(b => b.IdBeam == bi.IdBeam).Name,
                        CreatedAt = Beams.FirstOrDefault(b => b.IdBeam == bi.IdBeam).CreatedAt,
                        UpdatedAt = Beams.FirstOrDefault(b => b.IdBeam == bi.IdBeam).UpdatedAt,
                        CanBeSaved = Beams.FirstOrDefault(b => b.IdBeam == bi.IdBeam).CanBeSaved,
                        MarkerValue = Beams.FirstOrDefault(b => b.IdBeam == bi.IdBeam).MarkerValue
                    })
                    .ToList()
            })
            .ToList();
    }

    public void RemoveItemsFromBeam(string idBeam)
    {
        BeamItems.RemoveRange(BeamItems.Where(bi => bi.IdBeam == idBeam));
    }

    public void AddItemToBeam(string idBeam, string markerValue, string idImage)
    {
        Console.WriteLine("Searching for item with marker value: " + markerValue);
        var item = Items.FirstOrDefault(i => i.MarkerValue == markerValue);
        if (item == null)
        {
            return;
        }
        Console.WriteLine("item id: " + item.IdItem + " exists");

        BeamItems.Add(
            new BeamItemModel
            {
                IdBeamItem = Guid.NewGuid().ToString(),
                IdBeam = idBeam,
                IdItem = item.IdItem,
                IdImage = idImage
            }
        );
        SaveChanges();
    }

    public (ErrorModel? error, ItemModel? data) GetItemById(string id)
    {
        var item = Items.FirstOrDefault(i => i.IdItem == id);
        if (item == null)
        {
            return (new ErrorModel { Message = "Item not found." }, null);
        }

        return (null, item);
    }
}
