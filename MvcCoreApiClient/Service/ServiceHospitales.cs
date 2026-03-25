using MvcCoreApiClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MvcCoreApiClient.Service
{
    public class ServiceHospitales
    {
        private string apiUrl;
        //NECESITAMOS INDICAR EL TIPO DE DATOS QUE VAMOS A LEER
        private MediaTypeWithQualityHeaderValue header;

        public ServiceHospitales(IConfiguration configuration)
        {
            this.apiUrl = configuration.GetValue<string>("ApiUrls:ApiHospitales");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            //SE UTILIZA LA CLASE HttpCLient para las peticiones
            using(HttpClient client = new HttpClient())
            {
                string request = "api/Hospitales";
                //INDICAMOS EL HOST
                client.BaseAddress = new Uri(this.apiUrl);
                //INDICAMOS LOS DATOS QUE VAMOS A CONSEGUIR
                //LIMPIAMOS LAS CABECERAS POR NORMA
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //REALIZAMOS LA PETICION Y CAPTURAMOS UNA RESPUESTA
                HttpResponseMessage response = await client.GetAsync(request);
                //EN LA RESPUESTA TENDRÍAMOS LA CLAVE SI DESEAMOS PERSONALIZAR ERRORES
                if(response.IsSuccessStatusCode==true)
                {
                    //RECUPERAMOS EL CONTENIDO EN JSON
                    string json = await response.Content.ReadAsStringAsync();
                    //MEDIANTE NEWTON LO PASAMOS A LISTA,LO DESERIALIZAMOS 
                    List<Hospital> hospitales = JsonConvert.DeserializeObject<List<Hospital>>(json);
                    return hospitales;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Hospital> FindHospitalById(int idHospital)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Hospitales/" + idHospital;
                client.BaseAddress = new Uri(this.apiUrl);
                //limpiamos 
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    //SI LAS PROPIEDADES DEL MODEL Y DEL JSON SE LLAMAN IGUAL
                    //NO ES NECESARUI DECORAR CON JSONPREPERTY Y TAMPOCO UTILIZAR JSONCONVERT
                    //SOLO SI SE LLAMAN IGUAL
                    Hospital hospital = await response.Content.ReadAsAsync<Hospital>();
                    return hospital;
                }
                else
                {
                    return null;
                }

            }
        }


    }
}
