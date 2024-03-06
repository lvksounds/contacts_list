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
      try {
        const { data } = await axiosInstance.post(`/auth`, user);
        console.log(data);
        this.auth = {
          isAuthenticated: true,
          user: data.user,
          token: data.token,
        };
        this.hasError = false;
      } catch (error) {
        const res = error.response.data;
        this.hasError = true;
        this.error = res.message;
      }
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
