using ApiSteam.Models;
using Newtonsoft.Json;

namespace ApiSteam.Utils {
    public static class JsonReader {
        public static List<Persona> LeerPersonas(string Ruta) {
            List<Persona> Lista = new List<Persona>();
            string text = File.ReadAllText(Ruta);
            try {
                Lista = JsonConvert.DeserializeObject<List<Persona>>(text)!;
            } catch {
               Lista.Add(new Persona() { Nombre = "Error", Apellido = "Error", Edad = 0 });
            }
            return Lista;
        }
    }
}
