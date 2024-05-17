using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;

public class FileUploader
{
    public string UploadPDF(byte[] fileBytes, string fileName, string connectionString, string containerName)
    {
        var blobClient = new BlobClient(connectionString, containerName, fileName);

        BlobHttpHeaders blobHttpHeader = new BlobHttpHeaders { ContentType = "application/pdf" };

        using (var stream = new MemoryStream(fileBytes))
        {
            blobClient.Upload(stream, new BlobUploadOptions()
            {
                HttpHeaders = blobHttpHeader
            });
        }

        return blobClient.Uri.AbsoluteUri;
    }
}

internal class PDF
{
    static void Main(string[] args)
    {
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=sysodontonuvem;AccountKey=5UqfJe4txu+2cWWFMeNLQYDQIhpMU11ZgLq/fE6X5fP1eRoVucbETGVOjUcLTkL679Xp4oaw2hhJ+AStt7x52w==;EndpointSuffix=core.windows.net";
        string containerName = "sysodonto";

        byte[] fileBytes = File.ReadAllBytes("caminho-do-arquivo/arquivo.pdf");
        string fileName = "nome-do-arquivo.pdf";

        FileUploader uploader = new FileUploader();
        string fileUrl = uploader.UploadPDF(fileBytes, fileName, connectionString, containerName);

        Console.WriteLine("Arquivo enviado com sucesso. URL: " + fileUrl);
    }
}
