using MongoDB.Bson;
using MongoDB.Driver;
using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;
using System.Collections.Generic;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure
{
    public class MongoDBFileRepository : IFileRepository
    {
        string _connectionString;
        MongoClient _dbClient;

        public MongoDBFileRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _dbClient = new MongoClient(_connectionString);
        }

        public async void InsertFileContent(SalesFileBase salesFile)
        {
            try
            {
                var mongoDBDataBase = _dbClient.GetDatabase("DealerTrack");
                var salesFilesCollection = mongoDBDataBase.GetCollection<CSVSalesFile>("SalesFiles");
                await salesFilesCollection.InsertOneAsync((CSVSalesFile)salesFile);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not save document", ex);
            }
        }
    }
}
