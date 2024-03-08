import axiosInstance from "./api";
import { useAuthStore } from "@/stores/auth";

const getContacts = async () => {
  const auth = useAuthStore();
  try {
    const req = await axiosInstance.get("/get-user-contacts", {
      headers: {
        Authorization: `Bearer ${auth.$state.auth.token}`,
      },
    });
    return req.data;
  } catch (error) {
    console.log(error);
    return error;
  }
};

export default getContacts;
