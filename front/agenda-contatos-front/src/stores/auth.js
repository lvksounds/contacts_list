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
  }),

  getters: {
    userName(state) {
      return state.auth.user.userName;
    },
    isAuthenticated(state) {
      return state.auth.isAuthenticated;
    },
  },

  actions: {
    async login(user) {
      try {
        const { data } = await axiosInstance.post(`/auth`, user);

        this.auth = {
          isAuthenticated: true,
          user: data.user,
          token: data.token,
        };
      } catch (error) {
        console.log(error);
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
