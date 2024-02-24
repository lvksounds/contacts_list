import axios from "@/config/axios-config";

const axiosInstance = axios.create({
  baseURL: "https://localhost:7198/api",
  headers: {
    "Content-Type": "application/json",
  },
});

export default axiosInstance;
