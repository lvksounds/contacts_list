import { defineStore } from "pinia";
import axiosInstance from "@/services/api";
import { useLocalStorage } from "@vueuse/core";

export const useAuthStore = defineStore("auth", {
  state: () => ({
    auth: useLocalStorage("auth", {
      isAuthenticated: false,
      user: {},
      token: "",
    }),
    hasError: false,
    error: "",
  }),

  getters: {
    userName(state) {
      return state.auth.user.userName;
    },
    userId(state) {
      return state.auth.user.id;
    },
    isAuthenticated(state) {
      return state.auth.isAuthenticated;
    },
    isNoTAuthenticated(state) {
      return state.hasError;
    },
    errorMessage(state) {
      return state.error;
    },
  },

  actions: {
    async login(user) {
      const { data } = await axiosInstance
        .post(`/auth`, user)
        .then((res) => {
          this.auth = {
            isAuthenticated: true,
            user: res.data.user,
            token: res.data.token,
          };
          this.hasError = false;
        })
        .catch((err) => {
          this.hasError = true;
          console.log(err);
        });
    },

    async checkToken() {
      try {
        const tokenAuth = "Bearer " + this.auth.token;
        const { data } = await axiosInstance.get("/auth/verify", {
          headers: {
            Authorization: tokenAuth,
          },
        });
        return data;
      } catch (error) {
        console.log(error);
      }
    },

    clear() {
      localStorage.removeItem("auth");

      this.auth = {
        isAuthenticated: false,
        user: {},
        token: "",
      };
    },
  },
});
