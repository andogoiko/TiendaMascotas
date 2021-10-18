namespace UI{

    //esta clase se ha tenido que crear debido a que para conseguir ciertos resultados en pantalla se necesita una clase desitmta a las creadas en el modelo de negocio
public class MascotaMV{

        public string Nombre;
        public string Especie;
        public string Duenio;

        
        public override string ToString() => $"\"{Nombre}\" es un {Especie} de {Duenio}";
    }
}