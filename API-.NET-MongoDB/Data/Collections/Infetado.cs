
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET_MongoDB.Data.Collections
{
    public class Infetado
    {
        public DateTime DatadeNascimento { get; set; }
        public string Sexo { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }

        public Infetado(DateTime dataNascimento, string sexo, double latitude, double longitude)
        {
            this.DatadeNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }
    }
}
