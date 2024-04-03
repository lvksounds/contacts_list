import axiosInstance from "./api";
import { useAuthStore } from "@/stores/auth";

const auth = useAuthStore();

export const getContacts = async () => {
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

export const deleteContact = async (contactId, userId) => {
  try {
    const req = await axiosInstance.delete(
      `/delete-contact?contactId=${contactId}&userId=${userId}`,
      {
        headers: {
          Authorization: `Bearer ${auth.$state.auth.token}`,
        },
      }
    );
    return req.data;
  } catch (error) {
    console.log(error);
    return error;
  }
};

export const createContact = async (contact) => {
  try {
    const { req } = await axiosInstance
      .post("/create-contact/", contact, {
        headers: {
          "Content-Type": "multipart/form-data",
          Authorization: `Bearer ${auth.$state.auth.token}`,
        },
      })
      .then((res) => {
        return res.data;
      });
    return req;
  } catch (error) {
    return error;
  }
};

export const updateContact = async (contact) => {
  try {
    const { req } = await axiosInstance.put("/update-contact", contact, {
      headers: {
        "Content-Type": "multipart/form-data",
        Authorization: `Bearer ${auth.$state.auth.token}`,
      },
    });

    return req;
  } catch (err) {
    return err;
  }
};
