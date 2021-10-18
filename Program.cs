using Servicios;
using Negocio;
using UI.Consola;

System.Console.WriteLine("> Inicio el programa!!!");

var repositorio = new RepositorioCSV();
var sistema = new GestorDeMascotas(repositorio);
var vista = new Vista();
var controlador = new Controlador(sistema, vista);

controlador.Run();

System.Console.WriteLine("> Fin del programa :c");