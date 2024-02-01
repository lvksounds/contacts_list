import { createRouter, createWebHashHistory } from "vue-router";
import { mapStores } from "pinia";
import HomeView from "../views/HomeView.vue";
import LoginView from "../views/LoginView.vue";
import { useAuthStore } from "@/stores/auth";

const routes = [
  {
    path: "/",
    name: "login",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    //component: () =>
    //import(/* webpackChunkName: "about" */ "../views/LoginView.vue"),
    component: LoginView,
  },
  {
    path: "/home",
    name: "home",
    component: HomeView,
    meta: {
      auth: true,
    },
  },
];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  if (to.meta?.auth) {
    const auth = useAuthStore();

    if (auth.token && auth.user) {
      const isAuthenticated = auth.checkToken();
      if (isAuthenticated) {
        next();
      }
    } else {
      next(to.name === "login");
    }
  } else {
    next();
  }
});

export default router;
