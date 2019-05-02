using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;


namespace WebApp_OpenIDConnect_DotNet.Blob
{
    public class StorageHandler
    {
        const string StorageAccountName = "apdevproject";
        const string StorageAccountKey = "hP/K604+p7af6gpjw01WGc0pAFN7ln34jR+9EKkIcy9EBYpmz8O4Jgz9GswC3cqjTM0FcOyQ0bBVDwTZSlt3xw==";
        //const string FolderPath = @"C:\test";

        public static void Upload(string filedir, string unmappedLocation)
        {
            var StorageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), true);
            var blobClient = StorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("primary-container");
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions()
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            //string filepath = Server.MapPath(Url.Content("~/Content/Images/Image.jpg"));
            var blob = container.GetBlockBlobReference(unmappedLocation.Remove(0, 2));
            blob.UploadFromFile(filedir);
            //foreach (var FilePath in Directory.GetFiles(FolderPath, "*.*", SearchOption.AllDirectories))
            //{
            //    var blob = container.GetBlockBlobReference(FilePath);
            //    blob.UploadFromFile(FilePath);
            //}
        }
    }
}