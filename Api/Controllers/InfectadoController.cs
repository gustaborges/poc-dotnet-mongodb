using Api.Data;
using Api.Data.Collections;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        private Data.CoronavirusDB _coronavirusDB;
        private IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.CoronavirusDB coronavirusDB)
        {
            this._coronavirusDB = coronavirusDB;
            this._infectadosCollection = _coronavirusDB.Database.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public IActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
            _infectadosCollection.InsertOne(infectado);
            return StatusCode(201, "Sucesso na inserção de infectado");
        }

        [HttpGet]
        public IActionResult ObterInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            return Ok(infectados);
        }

    }
}