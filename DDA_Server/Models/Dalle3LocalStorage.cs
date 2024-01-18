using System.Buffers.Text;
using OpenAI_API.Images;

public class Dalle3LocalStorage
{

public async Task<string?> DownloadAndSaveImage(ImageResult imageResult, string diceSpreadId)
{
    try
    {

        // Assuming imageResult.ImageUrl contains the URL to the image
        HttpClient httpClient = new HttpClient();
        byte[] imageBytes = await httpClient.GetByteArrayAsync(imageResult.ToString());

        string imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "../DDA_Client/public/storage/images/diceSpreadRolledImages");
            Directory.CreateDirectory(imagesDirectory);

        string imagePath = Path.Combine(imagesDirectory, $"{diceSpreadId}.jpg");

        await File.WriteAllBytesAsync(imagePath, imageBytes);

        string relativeUrl = $"/storage/images/diceSpreadRolledImages/{diceSpreadId}.jpg";
        return relativeUrl;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in DownloadAndSaveImage: {ex.Message}");
        return null;
    }
}

}
