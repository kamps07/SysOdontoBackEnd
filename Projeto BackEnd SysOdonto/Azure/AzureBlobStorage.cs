using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Projeto_BackEnd_SysOdonto.Azure
{
    public class AzureBlobStorage
    {
        private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=sysodontonuvem;AccountKey=5UqfJe4txu+2cWWFMeNLQYDQIhpMU11ZgLq/fE6X5fP1eRoVucbETGVOjUcLTkL679Xp4oaw2hhJ+AStt7x52w==;EndpointSuffix=core.windows.net";
        private readonly string containerName = "sysodonto";

        public string UploadImage(string image)
        {
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

        public string UploadPdf(string pdfBase64)
        {
            // Gera um nome randomico para o arquivo PDF
            var fileName = Guid.NewGuid().ToString() + ".pdf";

            // Limpa o hash enviado se necessário 
            var data = pdfBase64;
            if (pdfBase64.StartsWith("data:application/pdf;base64,"))
            {
                data = pdfBase64.Replace("data:application/pdf;base64,", "");
            }

            // Gera um array de Bytes
            byte[] pdfBytes = Convert.FromBase64String(data);

            // Define o BLOB no qual o arquivo PDF será armazenado
            var blobClient = new BlobClient(connectionString, containerName, fileName);

            var blobHttpHeader = new BlobHttpHeaders { ContentType = "application/pdf" };

            // Envia o arquivo PDF
            using (var stream = new MemoryStream(pdfBytes))
            {
                blobClient.Upload(stream, new BlobUploadOptions()
                {
                    HttpHeaders = blobHttpHeader,
                });
            }

            // Retorna a URL do arquivo PDF
            return blobClient.Uri.AbsoluteUri;
        }
    }
}

