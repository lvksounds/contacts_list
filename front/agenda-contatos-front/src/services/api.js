import axios from "axios";

const apiClient = axios.create({
  baseURL: "https://localhost:7198/api/Api",
  headers: {
    "Content-Type": "application/json",
  },
});

export default apiClient;
