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
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=sysodontonuvem;AccountKey=5UqfJe4txu+2cWWFMeNLQYDQIhpMU11ZgLq/fE6X5fP1eRoVucbETGVOjUcLTkL679Xp4oaw2hhJ+AStt7x52w==;EndpointSuffix=core.windows.net";
            string containerName = "sysodonto";

            // Gera um nome randomico para imagem
            var fileName = Guid.NewGuid().ToString() + ".jpg";

            // Limpa o hash enviado
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(image, "");

            // Gera um array de Bytes
            byte[] imageBytes = Convert.FromBase64String(data);

            // Define o BLOB no qual a imagem será armazenada
            var blobClient = new BlobClient(connectionString, containerName, fileName);

            var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpg" };

            // Envia a imagem
            using (var stream = new MemoryStream(imageBytes))
            {
                blobClient.Upload(stream, new BlobUploadOptions()
                {
                    HttpHeaders = blobHttpHeader,
                });
            }

            // Retorna a URL da imagem
            return blobClient.Uri.AbsoluteUri;
        }
    }
}

