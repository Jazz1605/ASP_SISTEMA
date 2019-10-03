import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import Categoria from './components/Categoria.vue'
import Articulos from './components/Articulos.vue'
import Ccompra from './components/Ccompra.vue'
import Cliente from './components/Cliente.vue'
import Cventa from './components/Cventa.vue'
import Ingreso from './components/Ingreso.vue'
import Proveedores from './components/Proveedores.vue'
import Rol from './components/Rol.vue'
import Usuario from './components/Usuario.vue'
import Venta from './components/Venta.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/categorias',
      name: 'categorias',
      component: Categoria
    },
    {
      path: '/articulo',
      name: 'articulo',
      component: Articulos
    },
    {
      path: '/C.Compras',
      name: 'Ccompras',
      component: Ccompra
    },
    {
      path: '/clientes',
      name: 'clientes',
      component: Cliente
    },
    {
      path: '/C.Ventas',
      name: 'cventas',
      component: Cventa
    },
    {
      path: '/ingresos',
      name: 'ingresos',
      component: Ingreso
    }
    ,
    {
      path: '/proveedor',
      name: 'proveedor',
      component: Proveedores
    },
    {
      path: '/roles',
      name: 'roles',
      component: Rol
    },
    {
      path: '/usuarios',
      name: 'usuarios',
      component: Usuario
    },
    {
      path: '/ventas',
      name: 'ventas',
      component: Venta
    }
  ]
})
