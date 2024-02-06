using System.Buffers.Text;
using OpenAI_API.Images;

public class Dalle3LocalStorage
{

public async Task<string?> DownloadAndSaveImage(ImageResult imageResult, string diceSpreadId)
{
    try
    {
        HttpClient httpClient = new HttpClient();
        byte[] imageBytes = await httpClient.GetByteArrayAsync(imageResult.ToString());

        string imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "../DDA_Client_FileServer/storage/images/diceSpreadRolledImages");
        Directory.CreateDirectory(imagesDirectory);

        string uniqueHash = Guid.NewGuid().ToString("N");
        string imagePath = Path.Combine(imagesDirectory, $"{uniqueHash}_{diceSpreadId}.jpg");

        await File.WriteAllBytesAsync(imagePath, imageBytes);

        string fileName = $"{uniqueHash}_{diceSpreadId}.jpg";
        return fileName;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in DownloadAndSaveImage: {ex.Message}");
        return null;
    }
}

}
