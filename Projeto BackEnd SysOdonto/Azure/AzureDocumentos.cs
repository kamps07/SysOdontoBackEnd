using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;

namespace Projeto_BackEnd_SysOdonto.Azure
{
    public class AzureBlobStoragepdf
    {
        public string UploadPdf(string pdfBase64)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=sysodontonuvem;AccountKey=5UqfJe4txu+2cWWFMeNLQYDQIhpMU11ZgLq/fE6X5fP1eRoVucbETGVOjUcLTkL679Xp4oaw2hhJ+AStt7x52w==;EndpointSuffix=core.windows.net";
            string containerName = "sysodonto";

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
