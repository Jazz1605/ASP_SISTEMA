<template>
  <v-layout align-start>
     <!-- alineacion superior izquierda categoria:[] biene de abajo return-->
    <v-flex>
      <v-data-table 
       :headers="headers"

       :items="categorias" 
       :search="search"
       class="elevation-1">
        <template v-slot:top>
          <v-toolbar flat color="white">
            <v-toolbar-title>Categoria</v-toolbar-title>
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
                      <v-flex xs12 sm12 md12>
                        <v-text-field v-model="nombre" label="Nombre"></v-text-field>
                      </v-flex>
                      <v-flex xs12 sm12 md12>
                        <v-text-field v-model="descripcion" label="Descripcion"></v-text-field>
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
                    <template v-if="item.condicion">
                <v-icon small   @click="activarDesactivarMostrar(2,item)">block</v-icon>
                </template>
                <template v-else>
                    <v-icon small   @click="activarDesactivarMostrar(1,item)">check</v-icon>
                </template>
         </template>

        <template v-slot:item.condicion="{ item }">
                <v-card-text v-if="item.condicion" class="blue--text">Activo</v-card-text>
                 <v-card-text v-if="!item.condicion" class="red--text">Inactivo</v-card-text>
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
       categorias:[],
   
      //pegar el primer codigo
      dialog: false,
      headers: [
        { text: 'Opciones', value: 'opciones', sortable: false },
        { text: 'Nombre',value: 'nombre'},
        { text: 'Descripción', value: 'descripcion',sortable: false },
        { text: 'Estado', value: 'condicion',sortable: false },
      ],
      search:'',
      editedIndex: -1,
      id:'',
      nombre: '',
      descripcion:'',
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
      return this.editedIndex === -1 ? "Nueva categoria" : "Actualizar categoria";
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
        axios.get('api/Categorias/Listar').
        then(function(response){
            //codigo
            //console.log(response);
            me.categorias=response.data;
        }).catch(function(error){
           console.log(error);
        });
     },
   
    editItem(item) {
      
      this.id = item.idcategoria;
      this.nombre = item.nombre;
      this.descripcion=item.descripcion;
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
      this.nombre="";
      this.descripcion="";
      this.editedIndex=-1;

    },

    guardar () {
      

      if (this.editedIndex > -1) {
          //codigo para editar
          let me=this;
          axios.put('api/Categorias/Actualizar',{
            'idcategoria':me.id,
            'nombre':me.nombre,
            'descripcion':me.descripcion
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
        axios.post('api/Categorias/CrearCategoria',{
          'nombre' : me.nombre,
          'descripcion' : me.descripcion
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
       },
      activarDesactivarMostrar(accion,item){
        this.addModal=1;
        this.addNombre=item.nombre;
        this.addId=item.idcategoria;

        if(accion==1){
          this.addAccion=1;
        }else if(accion==2){
          this.addAccion=2;
        } else{
          this.addModal=0;
        }
      },
      activarDesactivarCerrar(){
        this.addModal=0;
      },
      activar(){
        let me=this;
        axios.put('api/Categorias/Activar/'+this.addId,{}).then(function(response){
            me.addModal=0;
            me.addAccion=0;
            me.addNombre="";
            me.addId="";
            me.listar();
        }).catch(function(error){
          console.log(error);
        });
      },
      desactivar(){
        let me=this;
        axios.put('api/Categorias/Desactivar/'+this.addId,{}).then(function(response){
            me.addModal=0;
            me.addAccion=0;
            me.addNombre="";
            me.addId="";
            me.listar();
        }).catch(function(error){
          console.log(error);
        });
      }
    }
  
    
};
</script>