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
        public string destino { get; set; }
        public string origen { get; set; }

        //Programas
        public string ipcont08 { get; set; }
        public bool ipbasica { get; set; }
        public bool ipmodelo { get; set; }
        public bool ipintegr { get; set; }
        public bool dsarchi { get; set; }
        public bool ipconts2 { get; set; }
        public bool ipabogax { get; set; }
        public bool iprent22 { get; set; }
        public bool iprent21 { get; set; }
        public bool iprent20 { get; set; }
        public bool ippatron { get; set; }
        public bool contalap { get; set; }
        public bool v000adc { get; set; }
        public bool ipabogad { get; set; }
        public bool siibase { get; set; }
        public bool certbase { get; set; }
        public bool notibase { get; set; }
        public bool dsedespa { get; set; }
        public bool dsesign { get; set; }
        public bool n43base { get; set; }
        public bool ipabopar { get; set; }


        public variables()
        {

            rutaPi = @"\\185.57.175.101\basprog_cyc\master9\EstandarPI";
            rutanoPi = @"\\185.57.175.101\basprog_cyc\master9\EstandarAsesoria";
            rutaGestion = @"\\185.57.175.101\basprog_cyc\master9\EstandarEmpresa";
            rutaGasoleos = @"\\185.57.175.101\basprog_cyc\master9\Medida";

            //Ver como dejar la variable destino que podria ser un listbox con las opciones que hay (pi,nopi, local, etc)
            destinoPi = @"/u/dspi/master/";
            destinoLocal = @"c:\descargas_geco72\";
            destinonoPi = @"/u/dsnopi/master/";
            destinoPasesPi = @"/u/pases_pi/master/";
            destinoPasesnoPi = @"/u/pases_nopi/master/";
            destino = destinoPi;
            origen = rutaPi;


            //Modificar estas variables para que sean string igual que ipcont08 y se pueda almacenar el fichero a copiar
            ipcont08 = string.Empty;
            ipbasica = false;
            ipmodelo = false;
            ipintegr = false;
            dsarchi = false;
            ipconts2 = false;
            ipabogax = false;
            iprent22 = false;
            iprent21 = false;
            iprent20 = false;
            ippatron = false;
            contalap = false;
            v000adc = false;
            ipabogad = false;
            siibase = false;
            certbase = false;
            notibase = false;
            dsedespa = false;
            dsesign = false;
            n43base = false;
            ipabopar = false;
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

}
