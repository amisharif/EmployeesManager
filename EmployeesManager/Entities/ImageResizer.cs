using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.IO;

public class ImageResizer
{
    public async Task ResizeImageAsync(Stream inputStream, Stream outputStream, int width, int height)
    {
        using (var image = await Image.LoadAsync(inputStream))
        {
            // Resize the image to the specified width and height
            image.Mutate(x => x.Resize(width, height));

            // Save the resized image as a JPEG
            var encoder = new JpegEncoder() { Quality = 75 }; // Optionally, adjust the quality
            await image.SaveAsync(outputStream, encoder);
        }
    }
}
