<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" :clipped="$vuetify.breakpoint.lgAndUp" app>
      <v-list dense>
        <template>
          <v-list-item :to="{name : 'Home' }">
            <v-list-item-action>
              <v-icon>home</v-icon>
            </v-list-item-action>
            <v-list-item-title>INICIO</v-list-item-title>
          </v-list-item>
        </template>
        <template>
          <v-list-group>
            <v-list-item slot="activator">
              <!--Es esa flechita que crea hacia abajo de inicio-->
              <v-list-item-content>
                <v-list-item-title>Almacen</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--CREANDO LOS SUBGRUPOS-->
            <v-list-item :to="{ name:'categorias'}">
              <!--Aqui definimos la ruta del componente-->
              <v-list-item-action>
                <v-icon>table_chart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>Categorias</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--FIN-->
            <!--CREANDO LOS SUBGRUPOS-->
            <v-list-item :to="{ name:'articulo'}">
              <v-list-item-action>
                <v-icon>store</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>Artículos</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--FIN-->
          </v-list-group>
        </template>
        <template>
          <v-list-group>
            <v-list-item slot="activator">
              <!--Es esa flechita que crea hacia abajo de inicio-->
              <v-list-item-content>
                <v-list-item-title>Compras</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--CREANDO LOS SUBGRUPOS-->
            <v-list-item :to="{ name:'ingresos'}">
              <v-list-item-action>
                <v-icon>attach_money</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>Ingresos</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--FIN-->
            <!--CREANDO LOS SUBGRUPOS-->
            <v-list-item :to="{ name:'proveedor'}">
              <v-list-item-action>
                <v-icon>local_shipping</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>Proveedores</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--FIN-->
          </v-list-group>
        </template>
        <template>
          <v-list-group>
            <v-list-item slot="activator">
              <!--Es esa flechita que crea hacia abajo de inicio-->
              <v-list-item-content>
                <v-list-item-title>Ventas</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--CREANDO LOS SUBGRUPOS-->
            <v-list-item :to="{ name:'ventas'}">
              <v-list-item-action>
                <v-icon>shopping_cart</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>Ventas</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--FIN-->
            <!--CREANDO LOS SUBGRUPOS-->
            <v-list-item :to="{ name:'clientes'}">
              <v-list-item-action>
                <v-icon>mood</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>Clientes</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--FIN-->
          </v-list-group>
        </template>
        <template>
          <v-list-group>
            <v-list-item slot="activator">
              <!--Es esa flechita que crea hacia abajo de inicio-->
              <v-list-item-content>
                <v-list-item-title>Accesos</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--CREANDO LOS SUBGRUPOS-->
            <v-list-item :to="{ name:'roles'}">
              <v-list-item-action>
                <v-icon>people</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>Roles</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--FIN-->
            <!--CREANDO LOS SUBGRUPOS-->
            <v-list-item :to="{ name:'usuarios'}">
              <v-list-item-action>
                <v-icon>perm_identity</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>Usuarios</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--FIN-->
          </v-list-group>
        </template>
        <template>
          <v-list-group>
            <v-list-item slot="activator">
              <!--Es esa flechita que crea hacia abajo de inicio-->
              <v-list-item-content>
                <v-list-item-title>Consultas</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--CREANDO LOS SUBGRUPOS-->
            <v-list-item :to="{ name:'Ccompras'}">
              <v-list-item-action>
                <v-icon>question_answer</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>Consultas  Compras</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--FIN-->
            <!--CREANDO LOS SUBGRUPOS-->
            <v-list-item :to="{ name:'cventas'}">
              <v-list-item-action>
                <v-icon>trending_up</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>Consultas Ventas</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <!--FIN-->
          </v-list-group>
        </template>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar :clipped-left="$vuetify.breakpoint.lgAndUp" app color="blue darken-3" dark>
      <v-toolbar-title style="width: 300px" class="ml-0 pl-4">
        <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
        <span class="hidden-sm-and-down">Sistema ASP</span>
      </v-toolbar-title>

      <div class="flex-grow-1"></div>
      <v-btn icon>
        <v-icon>mdi-apps</v-icon>
      </v-btn>
      <v-btn icon>
        <v-icon>mdi-bell</v-icon>
      </v-btn>
    </v-app-bar>
    <v-content>
      <v-container class="fill-height" fluid>
        <v-slide-y-transition mode="out-in">
          <router-view/>
        </v-slide-y-transition>
      </v-container>
    </v-content>
    <v-btn bottom color="pink" dark fab fixed right @click="dialog = !dialog">
      <v-icon>mdi-plus</v-icon>
    </v-btn>
    <v-dialog v-model="dialog" width="800px">
      <v-card>
        <v-card-title class="grey darken-2">Create contact</v-card-title>
        <v-container>
          <v-row>
            <v-col class="align-center justify-space-between" cols="12">
              <v-row align="center">
                <v-avatar size="40px" class="mr-4">
                  <img src="//ssl.gstatic.com/s2/oz/images/sge/grey_silhouette.png" alt />
                </v-avatar>
                <v-text-field placeholder="Name"></v-text-field>
              </v-row>
            </v-col>
            <v-col cols="6">
              <v-text-field prepend-icon="business" placeholder="Company"></v-text-field>
            </v-col>
            <v-col cols="6">
              <v-text-field placeholder="Job title"></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field prepend-icon="mail" placeholder="Email"></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field type="tel" prepend-icon="phone" placeholder="(000) 000 - 0000"></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field prepend-icon="notes" placeholder="Notes"></v-text-field>
            </v-col>
          </v-row>
        </v-container>
        <v-card-actions>
          <v-btn text color="primary">More</v-btn>
          <div class="flex-grow-1"></div>
          <v-btn text color="primary" @click="dialog = false">Cancel</v-btn>
          <v-btn text @click="dialog = false">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-app>
</template>

<script>
export default {
  name: "App",
  data() {
    return {
      drawer: null //Para desplegar el menú
    };
  }
};
</script>
