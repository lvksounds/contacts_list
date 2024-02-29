import { defineStore } from "pinia";
import axiosInstance from "@/services/api";

import { computed } from "vue";

export const useAuthStore = defineStore("auth", {
  state: () => ({
    token: localStorage.getItem("token"),
    user: JSON.parse(localStorage.getItem("user")),
  }),

  getters: {
    userName(state) {
      return state.user.userName;
    },

    isAuthenticated(state) {
      return state.token && state.user;
    },
  },

  actions: {
    setToken(tokenValue) {
      localStorage.setItem("token", tokenValue);
      this.token = tokenValue;
    },
    setUser(userValue) {
      localStorage.setItem("user", userValue);
      this.user = userValue;
    },

    async checkToken() {
      try {
        const tokenAuth = "Bearer " + this.token;
        const { data } = await axiosInstance.get("/auth/verify", {
          headers: {
            Authorization: tokenAuth,
          },
        });
        return data;
      } catch (error) {
        console.log(error.response.data);
      }
    },

    clear() {
      localStorage.removeItem("token");
      localStorage.removeItem("user");

      this.token = "";
      this.user = "";
    },
  },
});
