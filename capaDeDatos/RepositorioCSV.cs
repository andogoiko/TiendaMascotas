 using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Servicios{

    using Negocio.Modelos;

    public class RepositorioCSV : IRepositorio{

        //tiene que ser una constante o no deja utilizarlo de esta manera
        const string rutaDatos = "Data/";
        string dueniosCSV = rutaDatos + "duenios.csv";
        string mascotasCSV = rutaDatos+ "mascotas.csv";

        void IRepositorio.Inicializar(){

        }

        //devolvemos los dueños

        List<Duenios> IRepositorio.CargarDuenios(){

            // cogemos todas las lineas que cumplan las condiciones

            return File.ReadAllLines(dueniosCSV)
                .Where(row => row.Length > 0)
                .Select(d=>Duenios.ParseRow(d)).ToList();
        }

        //devolvemos los dueños y cogemos todas las lineas que cumplan las condiciones

        List<Mascotas> IRepositorio.CargarMascotas() => File.ReadAllLines(mascotasCSV).Where(row => row.Length > 0).Select(Mascotas.ParseRow).ToList();
        
    }
}