﻿using Amazon.S3;
using Amazon.S3.Model;
using MvcCoreApiPeliculasAWS7.Models;

namespace MvcCoreApiPeliculasAWS7.Services
{
    public class ServicesStorageAWS
    {
        private IAmazonS3 client;
        private string BucketName;

        public ServicesStorageAWS(IAmazonS3 client, KeysModel keys)
        {
            this.client = client;
            this.BucketName =keys.BucketName;
        }

        //METODO PARA SUBIR LAS IMAGENES DONDE NECESITAMOS EL NOMBRE DE LA IMAGEN, EL STREAM , EL BUCKET NAME
        public async Task<bool>UploadFileAsync(string filename,Stream stream)
        {
            PutObjectRequest request = new PutObjectRequest
            {
                InputStream = stream,
                Key = filename,
                BucketName = this.BucketName
            };
            PutObjectResponse response = await this.client.PutObjectAsync(request);
            if(response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
