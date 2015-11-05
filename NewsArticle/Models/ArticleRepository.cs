using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsArticle.Models
{
    public class ArticleRepository : IArticleRepository
    {
        public static CloudBlobContainer container;
        public ArticleRepository()
        {
            var creds = new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials("saihadooptest", "6CUgPRjOYdAhbK6alofJzIqzqD8IbyBJl4iCVidaopcWJxsBUHoZSeFGsvsGwxTnsyWwPbctASp9UV+srt+cOw==");

            CloudStorageAccount storageAccount = new CloudStorageAccount(creds, true); 
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            
            container = blobClient.GetContainerReference("newsfeed");
            
            container.CreateIfNotExists();
        }
        private void save(object obj, string id)
        {
            CloudBlockBlob blob = container.GetBlockBlobReference(id);
            blob.UploadText(JsonConvert.SerializeObject(obj));
        }

        private ArticleModel Read(string id)
        {
            CloudBlockBlob blob = container.GetBlockBlobReference(id);
            if (!blob.Exists())
            {
                return null;
            }

            string blobStr = blob.DownloadText();
            if (string.IsNullOrEmpty(blobStr))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ArticleModel>(blobStr);
        }

        public List<ArticleModel> GetAllArticles()
        {
            var blobs = container.ListBlobs().ToList();
            List<ArticleModel> result = new List<ArticleModel>();
            foreach(var blob in blobs)
            {
                var uri = blob.Uri.Segments;
                result.Add(Read(uri[2]));
            }
            return result;
        }

        public List<ArticleModel> GetMyArticles(string userName)
        {
            var blobs = container.ListBlobs().ToList();
            List<ArticleModel> result = new List<ArticleModel>();
            foreach (var blob in blobs)
            {
                var uri = blob.Uri.Segments;
                result.Add(Read(uri[2]));
            }
            var finalResult = result.Where(r => r.CreatedBy.Equals(userName)).ToList();
            return finalResult;
        }

        public bool DeleteArticle(string id)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(id);
            blockBlob.Delete();
            return true;
        }

        public bool EditArticle(string id, string title, string description)
        {
            ArticleModel a = Read(id);
            a.Title = title;
            a.Description = description;
            save(a, a.Id.ToString());
            return true;
        }

        public bool AddNewArticle(ArticleModel a)
        {
            save(a, a.Id.ToString());
            return true;
        }
        
    }
}