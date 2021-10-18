using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;


namespace UI.Consola{

    using System;
    using Negocio;
    using Negocio.Modelos;
    using UI;

    public class Controlador{

        private GestorDeMascotas sistema;
        private Vista vista;
        private Dictionary<string, Action> casosDeUso;

        public Controlador(GestorDeMascotas sistema, Vista vista){

            this.sistema = sistema;
            this.vista = vista;
            this.casosDeUso = new Dictionary<string, Action>(){
                { "Obtener los propietarios", ObtenerDuenios },
                { "Obtener todas las mascotas", ObtenerMascotas },
                { "Comprar una mascota", ComprarMascota },
                { "Mascotas perro con propietario Chico", MascotasPerroConDueño },
            };
        }

        public void Run(){

            vista.LimpiarPantalla();
            var menu = casosDeUso.Keys.ToList<String>();

            while (true)
                try{

                    // mostramos el menú
                    var key = vista.TrySeleccionarOpcionDeListaEnumerada("Menú de Usuario", menu, "Seleciona una opción");

                    vista.LimpiarPantalla();
                    vista.MuestraTitulo(key);

                    casosDeUso[key].Invoke();

                    vista.MuestraLineYEsperaReturn("Pulsa <Return> para continuar");
                    vista.LimpiarPantalla();
                }
                catch { return; }
        }

        // CASOS DE USO
        private void ObtenerDuenios() =>
            vista.MostrarListaEnumerada("Todos los Dueños", sistema.ObtenerDuenios());
        void ObtenerMascotas() =>
            vista.MostrarListaEnumerada("Todos las Mascotas", sistema.ObtenerMascotas());
        void ObtenerMascotasDTO(){

            var duenios = sistema.ObtenerDuenios();
            var mascotas = sistema.ObtenerMascotas();
            var query = from dueño in duenios
                        join mascota in mascotas on dueño.IdDuenio equals mascota.IdDuenio
                        select new MascotaMV { Duenio = dueño.Nombre, Nombre = mascota.Nombre, Especie = mascota.Especie };

            vista.MostrarListaEnumerada("Todos las Mascotas", query.ToList());
        }

        void ComprarMascota(){

            try{

                var duenio = vista.TrySeleccionarOpcionDeListaEnumerada("Comprador de mascota", sistema.ObtenerDuenios(), "Escoje un comprador");
                var nombre = vista.TryObtenerEntradaDeTipo<string>("¿Cómo se llama la mascota?");
                var especie = vista.TryObtenerEntradaDeTipo<string>("Indicame la especie");
                var hecho = sistema.ComprarMascota(duenio, new Mascotas { Nombre = nombre, Especie = especie });
                vista.MuestraMensaje(hecho ? "Mascota felizmente adquirida" : "Operación no permitida");
            }
            catch (Exception e){

                Console.WriteLine(e.Message);
                return;
            }
        }

        void MascotasPerroConDueño(){

            // Linq con selección WHERE expecífica
            var duenios = sistema.ObtenerDuenios();
            var mascotas = sistema.ObtenerMascotas();
            var query = from dueño in duenios
                        join mascota in mascotas on dueño.IdDuenio equals mascota.IdDuenio
                        where dueño.Sexo == 'H' & mascota.Especie == "perro"
                        select new MascotaMV { Duenio = dueño.Nombre, Nombre = mascota.Nombre, Especie = mascota.Especie };
            vista.MostrarListaEnumerada("Mascotas con dueño chico", query.ToList());
        }
    }
}