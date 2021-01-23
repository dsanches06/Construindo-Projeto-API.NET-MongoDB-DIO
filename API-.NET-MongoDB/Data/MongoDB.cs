using API_.NET_MongoDB.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET_MongoDB.Data
{
    public class MongoDB
    {
        public IMongoDatabase DB { get; }

        public MongoDB(IConfiguration configuration)
        {
            try
            {
                //criar uma configuração de connection string
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                //criar um cliente 
                var client = new MongoClient(settings);
                //conecta a objeto a base de dados do mongodb
                DB = client.GetDatabase(configuration["NomeBanco"]);
                //mapear
                MapClasses();
            }
            catch (Exception)
            {
                throw new MongoException("It was not possible to connect to MongoDB");
            }
        }

        private void MapClasses()
        {
            //criar uma convenção para permitir o uso de camel case
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            //regista a convenção camel-case
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            //verificar se não existe classe mapead do tipo infetado
            if (!BsonClassMap.IsClassMapRegistered(typeof(Infetado)))
            {
                //se não for, faz o mapeamento 
                BsonClassMap.RegisterClassMap<Infetado>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
