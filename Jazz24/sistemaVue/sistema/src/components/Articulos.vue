<template>
  <v-layout align-start>
     <!-- alineacion superior izquierda categoria:[] biene de abajo return-->
    <v-flex>
      <v-data-table 
       :headers="headers"
       :items="articulos" 
       :search="search"
       class="elevation-1">
        <template v-slot:top>
          <v-toolbar flat color="white">
            <v-toolbar-title>Artículos</v-toolbar-title>
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
                    <v-flex xs6 sm6 md6>
                        <v-text-field v-model="codigo" label="Codigo">

                        </v-text-field>
                    </v-flex>
                    <v-flex xs6 sm6 md6>
                        <v-select v-model="idcategoria" :items="categorias" label="Categoria">

                        </v-select>
                    </v-flex>
                
                      <v-flex xs12 sm12 md12>
                        <v-text-field v-model="nombre" label="Nombre"></v-text-field>
                      </v-flex>
                        <v-flex xs6 sm6 md6>
                            <v-text-field type="number" v-model="stock" label="Stock">

                            </v-text-field>
                        </v-flex>
                        <v-flex xs6 sm6 md6>
                            <v-text-field type="number" v-model="precio_venta" label="Precio_Venta">

                            </v-text-field>
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
       articulos:[],
   
      //pegar el primer codigo
      dialog: false,
      headers: [
        { text: 'Opciones', value: 'opciones', sortable: false },
        { text: 'Código',value: 'codigo'},
        { text: 'Nombre', value: 'nombre',sortable: false },
        { text: 'Categoria', value: 'categoria',sortable: false },
        { text: 'Stock',value: 'stock'},
        { text: 'Precio_Venta', value: 'precio_venta',sortable: false },
        { text: 'Descripcion', value: 'descripcion',sortable: false },
        { text: 'Estado', value: 'condicion',sortable: false },
      ],
      search:'',
      editedIndex: -1,
      id:'',
      idcategoria:'',
      categorias:[],
      codigo:'',
      nombre:'',
      stock:0,
      precio_venta:0,
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
      return this.editedIndex === -1 ? "Nueva Artículo" : "Actualizar Artículo";
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
    this.select();
  },
  //fin
  methods: {
     // traer en el data table 
  
     // 1er metodo para traer informacion
     listar(){
           let me=this;
        axios.get('api/Articulos/Listar').
        then(function(response){
            //codigo
            //console.log(response);
            me.articulos=response.data;
        }).catch(function(error){
           console.log(error);
        });
     },

     //CREANDO METODO PARA SELECCIONAR CATEGORIA
     select(){
           let me=this;
           var categoriasArray=[];
        axios.get('api/Categorias/Select').
        then(function(response){
            //codigo
            //console.log(response);
            categoriasArray=response.data;
            categoriasArray.map(function(x){
                me.categorias.push({text: x.nombre, value:x.idcategoria}); //LA X LA CREAMOS AHI MISMO
            });
            //me.articulos=response.data;
        }).catch(function(error){
           console.log(error);
        });
     },
   
    editItem(item) {
      this.idarticulo = item.idarticulo;  
      this.idcategoria = item.idcategoria;
      this.codigo = item.codigo;
      this.nombre = item.nombre;
      this.stock = item.stock;
      this.precio_venta = item.precio_venta;
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
      this.idarticulo="";
      this.codigo="";
      this.nombre="";
      this.idcategoria="";
      this.stock = 0;
      this.precio_venta = 0;
      this.descripcion="";
      this.editedIndex=-1;

    },

    guardar () {
      

      if (this.editedIndex > -1) {
          //codigo para editar
          let me=this;
          axios.put('api/Articulos/Actualizar',{
            'idarticulo':me.idarticulo, 
            'codigo':me.codigo,
            'nombre':me.nombre, 
            'idcategoria': me.idcategoria,
            'stock' : me.stock,
            'precio_venta' : me.precio_venta,
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
        axios.post('api/Articulos/CrearArt',{
            'codigo':me.codigo,
            'nombre':me.nombre, 
            'idcategoria': me.idcategoria,
            'stock' : me.stock,
            'precio_venta' : me.precio_venta,
            'descripcion':me.descripcion
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
        this.addId=item.idarticulo;

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
        axios.put('api/Articulos/Activar/'+this.addId,{}).then(function(response){
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
        axios.put('api/Articulos/Desactivar/'+this.addId,{}).then(function(response){
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