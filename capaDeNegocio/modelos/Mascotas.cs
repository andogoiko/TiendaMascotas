using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Negocio.Modelos{

    public class Mascotas{

        public string Nombre;
        public string Especie;
        public int IdDuenio;

         //devolvemos en un string el objeto dueño

        public override string ToString() => $"{Nombre}/{Especie}";

        // internal significa que es accesible desde cualquier parte del namespace, pero externamente no

        // la función sirve para que al meterle un dato lo trate y extraiga la informacion guardándolo en una lista de objetos Duenios

        internal static Mascotas ParseRow(string row){

            //para decirle que los numeros no estan en ingles

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            var columns = row.Split(',');

            return new Mascotas{

                Nombre = columns[1].Trim(),
                Especie = columns[2].Trim(),
                IdDuenio = Int32.Parse(columns[0].Trim(), nfi)
            };
        }
    }
}