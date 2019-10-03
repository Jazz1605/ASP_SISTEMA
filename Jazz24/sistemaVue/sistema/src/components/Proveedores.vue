<template>
  <v-layout align-start>
     <!-- alineacion superior izquierda categoria:[] biene de abajo return-->
    <v-flex>
      <v-data-table 
       :headers="headers"

       :items="personas" 
       :search="search"
       class="elevation-1">
        <template v-slot:top>
          <v-toolbar flat color="white">
            <v-toolbar-title>Proveedor</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <div class="flex-grow-1"></div>

            <!--copiar codigo buqueda-->
            <v-spacer></v-spacer>
            <v-text-field
              class="text-xs-center"
              v-model="search"
              append-icon="search"
              label="Busqueda"
              single-line
              hide-details
            ></v-text-field>
            <v-spacer></v-spacer>

            <!--fin-->

            <v-dialog v-model="dialog" max-width="500px">
              <template v-slot:activator="{ on }">
                <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo</v-btn>
              </template>
              <v-card>
                <v-card-title>
                  <span class="headline">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container grid-list-md>
                    <v-layout wrap>
                      <v-flex xs12 sm12 md6>
                        <v-text-field v-model="tipopersona" label="Tipo de Persona" readonly ></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm12 md6>
                        <v-text-field v-model="nombre" label="Nombre"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm12 md6>
                        <v-text-field v-model="tipodocumento" label="T. Documento"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm12 md6>
                        <v-text-field v-model="num_documento" label="N. Documento"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm12 md6>
                        <v-text-field v-model="direccion" label="Dirección"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm12 md6>
                        <v-text-field v-model="telefono" label="Teléfono"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm12 md12>
                        <v-text-field v-model="email" label="Email"></v-text-field>
                      </v-flex>
                    </v-layout>
                  </v-container>
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click.native="close">Cancelar</v-btn>
                  <v-btn color="blue darken-1" text @click.native="guardar">Guardar</v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
            <v-dialog v-model="addModal" max-width="290">
              <v-card>
                <v-card-title class="headline" v-if="addAccion==1">¿Activar item?</v-card-title>
                <v-card-title class="headline" v-if="addAccion==2">¿Desactivar item?</v-card-title>
                <v-card-text>
                  Estás a punto de
                  <span v-if="addAccion==1">Activar</span>
                  <span v-if="addAccion==2">Desactivar</span>
                  el item {{addNombre}}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="green darken-1" flat="flat" @click="activarDesactivarCerrar"> Cancelar </v-btn>
                  <v-btn v-if="addAccion==1" color="orange darken-1" flat="flat" @click="activar"> Activar </v-btn>
                  <v-btn v-if="addAccion==2" color="orange darken-4" flat="flat" @click="desactivar"> Desactivar </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>

         <template v-slot:item.opciones="{ item }">
                <v-icon small  class="mp-2" @click="editItem(item)">edit</v-icon>  
         </template>
        <template v-slot:no-data>
          <v-btn color="primary" @click="listar">Reset</v-btn>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>
</template>
<script>
import axios from 'axios'
export default {
  data() {
    return {
       personas:[],
   
      //pegar el primer codigo
      dialog: false,
      headers: [
        { text: 'Opciones', value: 'opciones', sortable: false },
        { text: 'Tipopersona',value: 'tipopersona'},
        { text: 'Nombre',value: 'nombre'},
        { text: 'Tipodocumento',value: 'tipodocumento'},
        { text: 'Num_documento',value: 'num_documento'},
        { text: 'Direccion',value: 'direccion'},
        { text: 'Telefono', value: 'telefono',sortable: false },
        { text: 'Email', value: 'email',sortable: false },
      ],
      search:'',
      editedIndex: -1,
      id:'',
      tipopersona: 'Proveedor',
      nombre:'',
      tipodocumento: '',
      num_documento:'',
      direccion: '',
      telefono:'',
      email:'',
      valida:0,
      validaMensaje:[],
      addModal:0,
      addAccion:0,
      addNombre:'',
      addId:''
      }
    
  },
  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo Cliente" : "Actualizar Cliente";
    }
  },

  watch: {
    dialog(val) {
      val || this.close();
    }
  },

  created() {
     //llamado de las funciones
   
    this.listar();
  },
  //fin
  methods: {
     // traer en el data table 
  
     // 1er metodo para traer informacion
     listar(){
           let me=this;
        axios.get('api/Personas/Listar2').
        then(function(response){
            //codigo
            //console.log(response);
            me.personas=response.data;
            
        }).catch(function(error){
           console.log(error);
        });
     }, 
   
    editItem(item) {
      
      this.id = item.idpersona;
      this.tipopersona = item.tipopersona ;
      this.nombre = item.nombre;
      this.tipodocumento = item.tipodocumento;
      this.num_documento = item.num_documento;
      this.direccion = item.direccion;
      this.telefono = item.telefono;
      this.email = item.email;
      this.editedIndex=1;
      this.dialog = true;
    },

    deleteItem(item) {
      const index = this.desserts.indexOf(item);
      confirm("Deseas eliminar el dato?") &&
        this.desserts.splice(index, 1);
    },

    close() {
     this.dialog = false;
     this.limpiar();
    },
    limpiar(){
      this.id="";
      this.nombre ="";
      this.tipodocumento ="";
      this.num_documento ="";
      this.direccion ="";
      this.telefono ="";
      this.email ="";
      this.editedIndex=-1;

    },

    guardar () {
      

      if (this.editedIndex > -1) {
          //codigo para editar
          let me=this;
          axios.put('api/Personas/Actualizar',{
            'idpersona':me.id,
            'tipopersona':me.tipopersona,
            'nombre':me.nombre,
            'tipodocumento':me.tipodocumento,
            'num_documento':me.num_documento,
            'direccion':me.direccion,
            'telefono':me.telefono,
            'email':me.email,


            
          }).then(function(response){
            me.listar();
            me.close();
            me.limpiar();
          }).catch(function(error){
            console.log(error)
          })
          ;
      } else {
        //codigo para guardar
        let me =this;
        axios.post('api/Personas/CrearPersona',{
            'tipopersona':me.tipopersona,
            'nombre':me.nombre,
            'tipodocumento':me.tipodocumento,
            'num_documento':me.num_documento,
            'direccion':me.direccion,
            'telefono':me.telefono,
            'email':me.email,
        }).then(function(response){
          me.listar();
          me.close();
          me.limpiar();
          
        }).catch(function(error){
            console.log(error)
        });
      }
    },
       validar(){
        this.valida=0;
        this.validaMensaje=[];
        if(this.nombre.length<3 || this.nombre>50){
          this.validaMensaje.push("El nombre debe tene más de 3 caracteres y menos de 50")
        }
        if(this.validaMensaje.lenght){
          this.valida=1;
        };
        return this.valida;
       }
      
    
    }
  
    
};
</script>