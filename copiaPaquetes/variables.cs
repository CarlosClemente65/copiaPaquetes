using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace copiaPaquetes
{

    public class variables
    {
        //Rutas
        public string rutaPi { get; set; }
        public string rutanoPi { get; set; }
        public string rutaGestion { get; set; }
        public string rutaGasoleos { get; set; }

        //Destinos
        public string destinoPi { get; set; }
        public string destinoLocal { get; set; }
        public string destinonoPi { get; set; }
        public string destinoPasesPi { get; set; }
        public string destinoPasesnoPi { get; set; }
        public string destino {  get; set; }




        public variables()
        {
            //Rutas origen por defecto
            rutaPi = @"\\185.57.175.101\basprog_cyc\master9\EstandarPI\";
            rutanoPi = @"\\185.57.175.101\basprog_cyc\master9\EstandarAsesoria\";
            rutaGestion = @"\\185.57.175.101\basprog_cyc\master9\EstandarEmpresa\";
            rutaGasoleos = @"\\185.57.175.101\basprog_cyc\master9\Medida\";

            //Rutas destino por defecto
            destinoPi = @"/u/dspi/master/";
            destinoLocal = @"c:\descargas_geco72\";
            destinonoPi = @"/u/dsnopi/master/";
            destinoPasesPi = @"/u/pases_pi/master/";
            destinoPasesnoPi = @"/u/pases_nopi/master/";
            destino = destinoPi;
        }


        public void GuardarConfiguracion()
        {
            //Permite guardar en un fichero json el contenido de las variables
            try
            {
                string rutaArchivo = "configuracion.json";
                string jsonConfiguracion = JsonConvert.SerializeObject(this);
                File.WriteAllText(rutaArchivo, jsonConfiguracion);
                MessageBox.Show("Fichero de configuracion actualizado correctamente", "Actualizar configuracion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Fichero de configuracion no se ha podido actualizar", "Actualizar configuracion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarConfiguracion()
        {
            //Carga el contenido de las variables segun las que esten grabadas en el fichero de configuracion
            string rutaArchivo = "configuracion.json";

            if (File.Exists(rutaArchivo))
            {
                string jsonConfiguracion = File.ReadAllText(rutaArchivo);
                JsonConvert.PopulateObject(jsonConfiguracion, this);

                // Actualiza las variables después de cargar la configuración
                ActualizaVariables(rutaPi, rutanoPi, rutaGestion, rutaGasoleos, destinoPi, destinonoPi, destinoLocal, destinoPasesPi, destinoPasesnoPi);
            }

        }

        public void ActualizaVariables(string nuevaRutaPi, string nuevaRutanoPi, string nuevaRutaGestion, string nuevaRutaGasoleos, string nuevoDestinoPi, string nuevoDestinonoPi, string nuevoDestinoLocal, string nuevoDestinoPasesPi, string nuevoDestinoPasesnoPi)
        {
            //Una vez leidas las variables del fichero de configuracion, se graban en las variables de la clase
            rutaPi = nuevaRutaPi;
            rutanoPi = nuevaRutanoPi;
            rutaGestion = nuevaRutaGestion;
            rutaGasoleos = nuevaRutaGasoleos;
            destinoPi = nuevoDestinoPi;
            destinonoPi = nuevoDestinonoPi;
            destinoLocal = nuevoDestinoLocal;
            destinoPasesPi = nuevoDestinoPasesPi;
            destinoPasesnoPi = nuevoDestinonoPi;
        }
    }

    public class Programas
    {
        //Programas. La variable booleana establece si se copia o no, y la de ruta es el nombre de la aplicacion a copiar (luego se le añade la ruta de destino).
        public bool ipcont08 = false;
        public string ipcont08Ruta = @"ipcont08\pcont08z.tgz";
        public bool ipbasica = false;
        public string ipbasicaRuta = @"ipbasica\pbasicaz.tgz";
        public bool ipmodelo = false;
        public string ipmodeloRuta = @"ipmodelo\pmodeloz.tgz";
        public bool ipintegr = false;
        public string ipintegrRuta = @"ipintegr\pintegrz.tgz";
        public bool dsarchi = false;
        public string dsarchiRuta = @"dsarchi\dsarchiz.tgz";
        public bool ipconts2 = false;
        public string ipconts2Ruta = @"ipconts2\pconts2z.tgz";
        public bool iprent23 = false;
        public string iprent23Ruta = @"iprent23\prent23z.tgz";
        public bool iprent22 = false;
        public string iprent22Ruta = @"iprent22\prent22z.tgz";
        public  bool iprent21 = false;
        public string iprent21Ruta = @"iprent21\prent21z.tgz";
        public bool ippatron = false;
        public string ippatronRuta = @"ippatron\ppatronz.tgz";
        public bool contalap = false;
        public string contalapRuta = @"ipcontal\pcontalz.tgz";
        public bool v000adc = false;
        public string v000adcRuta = @"ipcont08_mod\000adc\000adcz.tgz";
        public bool ipabogax = false;
        public string ipabogaxRuta = @"ipabogax\pabogaxz.tgz";
        public bool ipabogad = false;
        public string ipabogadRuta = @"ipabogad\pabogadz.tgz";
        public bool ipabopar = false;
        public string ipaboparRuta = @"ipabopar\paboparz.tgz";
        public bool siibase = false;
        public string siibaseRuta = @"siibase\siibasez.tgz";
        public bool certbase = false;
        public string certbaseRuta = @"certbase\ertbasez.tgz";
        public bool notibase = false;
        public string notibaseRuta = @"notibase\otibasez.tgz";
        public bool dsedespa = false;
        public string dsedespaRuta = @"dsedespa\sedespaz.tgz";
        public bool dsesign = false;
        public string dsesignRuta = @"dsesign\dsesignz.tgz";
        public bool n43base = false;
        public string n43baseRuta = @"n43base\n43basez.tgz";
    }

}
