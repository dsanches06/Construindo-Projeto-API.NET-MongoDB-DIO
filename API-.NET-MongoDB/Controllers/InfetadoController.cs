using API_.NET_MongoDB.Data.Collections;
using API_.NET_MongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_.NET_MongoDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfetadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infetado> _infetadosCollection;

        public InfetadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            //obter a lista de colleções do tipo infetados registados no mongo database
            _infetadosCollection = _mongoDB.DB.GetCollection<Infetado>(typeof(Infetado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult Save([FromBody] InfetadoDTO dto)
        {
            // cria o objeto
            var infetado = new Infetado(dto.DatadeNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
            //insere no mongo database
            _infetadosCollection.InsertOne(infetado);
            //retorna o codigo 201 e uma mensagem
            return StatusCode(201, "Infetado foi adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            //obter a lista de todos os infetados que estão registado mongo database
            var infetados = _infetadosCollection.Find(Builders<Infetado>.Filter.Empty).ToList();
            //retorna codigo 200 - OK() com lista de infetados
            return Ok(infetados);
        }
    }
}
