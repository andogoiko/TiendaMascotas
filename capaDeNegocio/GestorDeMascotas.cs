using System;
using System.Collections.Generic;
using System.Linq;


using Servicios;

namespace Negocio{

    using Negocio.Modelos;

    //esta clase se utiliza como datos en cache tal y como trabaja windows

    public class GestorDeMascotas{

        IRepositorio Repositorio;
        List<Duenios> cacheDuenios = new List<Duenios>{};
        List<Mascotas> cacheMascotas = new List<Mascotas>{};

        public GestorDeMascotas(IRepositorio repositorio){

            Repositorio = repositorio;
            Repositorio.Inicializar();

            cacheDuenios = Repositorio.CargarDuenios();
            cacheMascotas = Repositorio.CargarMascotas();
        }

        public List<Duenios> ObtenerDuenios() => cacheDuenios;
        public List<Mascotas> ObtenerMascotas() => cacheMascotas;

        public bool ComprarMascota(Duenios duenio, Mascotas mascot){

            mascot.IdDuenio = duenio.IdDuenio;
            cacheMascotas.Add(mascot);
            return true;
        }
    }
}