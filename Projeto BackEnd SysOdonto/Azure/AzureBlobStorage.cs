using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Projeto_BackEnd_SysOdonto.Azure
{
    public class AzureBlobStorage
    {
        public string UploadImage(string image)
        {
            string connectionString = "";
            string containerName = "";

            // Generate a random name for the image
            var fileName = Guid.NewGuid().ToString() + ".jpg";

            // Clear the sent hash
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(image, "");

            // Generate a byte array
            byte[] imageBytes = Convert.FromBase64String(data);

            // Define the BLOB where the image will be stored
            var blobClient = new BlobClient(connectionString, containerName, fileName);

            var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpg" };

            // Upload the image
            using (var stream = new MemoryStream(imageBytes))
            {
                blobClient.Upload(stream, new BlobUploadOptions()
                {
                    HttpHeaders = blobHttpHeader,
                });
            }

            // Return the URL of the image
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
