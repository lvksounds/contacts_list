import { createApp } from "vue";
import { createPinia } from "pinia";
import ToastService from "primevue/toastservice";

import PrimeVue from "primevue/config";
import "/node_modules/primeflex/primeflex.css";
import "primeicons/primeicons.css";

import App from "./App.vue";
import router from "./router";

import InputText from "primevue/inputtext";
import Divider from "primevue/divider";
import Button from "primevue/button";
import Card from "primevue/card";
import Dialog from "primevue/dialog";
import Message from "primevue/message";
import Avatar from "primevue/avatar";

import "primevue/resources/themes/lara-light-blue/theme.css";

const pinia = createPinia();
const app = createApp(App);

app.use(pinia);
app.use(router);
app.use(PrimeVue);
app.use(ToastService);

app.component("InputText", InputText);
app.component("Divider", Divider);
app.component("Button", Button);
app.component("Card", Card);
app.component("Modal", Dialog);
app.component("Message", Message);
app.component("Avatar", Avatar);

app.mount("#app");
