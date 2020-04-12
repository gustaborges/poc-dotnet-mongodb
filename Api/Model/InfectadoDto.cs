using System;

namespace Api.Controllers
{
    public class InfectadoDto
    {
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}