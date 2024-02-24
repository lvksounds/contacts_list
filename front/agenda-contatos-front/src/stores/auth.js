import { defineStore } from "pinia";
import axiosInstance from "@/services/api";

export const useAuthStore = defineStore("auth", {
  state: () => ({
    token: localStorage.getItem("token"),
    user: JSON.parse(localStorage.getItem("user")),
  }),
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
        console.log(this.token);
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
  },
});
