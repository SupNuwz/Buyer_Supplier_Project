using System;
using System.Collections.Generic;
using System.IO;
using Minio.Exceptions;
using System.Threading.Tasks;
using Minio;


namespace Mainframe.BuyerSupplier.Common.Utility
{
    public class FileServerUtility
    {

        private static MinioClient minioClient = new MinioClient("192.168.1.192:9000",
                                                    "6URXOJDRPKUMMWJ4IXLX",
                                                      "pUBLgD5fgRSXUSfiiaCtiW9pxk+FIWO0j0SgjOS+");
            
        
        //Make a bucket
        public async static Task CreateBucket(string bucketName = "inventory-items")
        {
            try
            {
                await minioClient.MakeBucketAsync(bucketName);
            }
            catch (Exception e)
            {

            }
        }


        public async static Task UploadFile(string bucketName = "inventory-items",
                                   string objectName = "ld-reading-book-kid-clip-art-vector_csp2633566.jpg",
                                                               string fileName = "Pictures")
        {
            // Upload a file
            try
            {
                byte[] bs = File.ReadAllBytes(fileName);
                System.IO.MemoryStream filestream = new System.IO.MemoryStream(bs);

                await minioClient.PutObjectAsync(bucketName, objectName, filestream, filestream.Length, "application/octet-stream");
                Console.Out.WriteLine("uploaded successfully");
                Console.ReadLine();
            }
            catch (MinioException e)
            {
                Console.Out.WriteLine("Error occurred: " + e);
            }
        }

        //Presigned Get Object
        public async static Task<string> GetPresignedUrl(
                                    string bucketName = "inventory-items",
                                    string objectName = "Vegetable/subcategory/name/asd.jpeg")
        {
            try
            {
                Dictionary<string, string> reqParams = new Dictionary<string, string>() { { "response-content-type", "application/json" } };
                return await minioClient.PresignedGetObjectAsync(bucketName, objectName, 1000, reqParams);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Exception ", e.Message);
            }
            return null;
        }

        //pre-signed put
        public async static Task<string> GetPresignedPutUrl(        
                            string bucketName = "inventory-items",
                            string objectName = "Vegetable/subcategory/name/asd.jpeg")
        {
            try
            {
                return await minioClient.PresignedPutObjectAsync(bucketName, objectName,1000);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Exception ", e.Message);
            }
            return null;
        }
    }
}


