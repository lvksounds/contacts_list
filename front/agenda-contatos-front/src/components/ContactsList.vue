<template>
  <ScrollPanel class="h-30rem w-full">
    <div>
      <p>Contacts: {{ contacts.length }}</p>
    </div>
    <div class="card">
      <DataView :value="filteredContacts">
        <template #empty="slotProps">
          <p>{{ (slotProps.layout = "Your contact list is empty.") }}</p>
        </template>
        <template #list="slotProps">
          <div class="grid grid-nogutter">
            <div
              v-for="(item, index) in slotProps.items"
              :key="index"
              class="col-12"
            >
              <div
                class="flex flex-column sm:flex-row sm:align-items-center p-4 gap-3"
                :class="{ 'border-top-1 surface-border': index !== 0 }"
              >
                <div class="md:w-4rem relative align-items-center">
                  <Avatar
                    :label="item.profileImg ? null : item.name.substring(0, 1)"
                    :image="item.profileImg ? item.profileImg : null"
                    size="xlarge"
                    shape="circle"
                    class="flex align-itens-center"
                  />
                </div>
                <div
                  class="flex flex-column md:flex-row justify-content-between md:align-items-center flex-1 gap-4"
                >
                  <div
                    class="flex flex-row md:flex-column justify-content-between align-items-start gap-2"
                  >
                    <div>
                      <span class="font-medium text-secondary text-sm">{{
                        item.name
                      }}</span>
                      <div class="text-lg font-medium text-900 mt-2">
                        {{ item.phone }}
                      </div>
                      <div class="font-medium text-sm mt-2">
                        {{ item.email }}
                      </div>
                    </div>
                    <!-- <div
                      class="surface-100 p-1"
                      style="border-radius: 30px"
                      v-if="item.isFavorite"
                    >
                      <div
                        class="surface-0 flex align-items-center gap-2 justify-content-center py-1 px-2"
                        style="
                          border-radius: 30px;
                          box-shadow: 0px 1px 2px 0px rgba(0, 0, 0, 0.04),
                            0px 1px 2px 0px rgba(0, 0, 0, 0.06);
                        "
                      >
                        <i class="pi pi-star-fill text-yellow-500"> </i>
                      </div>
                    </div> -->
                  </div>
                  <div class="flex flex-column md:align-items-end gap-5">
                    <div class="flex flex-row-reverse md:flex-row gap-2">
                      <Button
                        icon="pi pi-trash"
                        severity="danger"
                        aria-label="delete"
                        @click="deleteContact(item.contactId, item.userId)"
                      />
                      <!-- <Button
                        icon="pi pi-heart"
                        outlined
                        severity="success"
                      ></Button> -->
                      <Button
                        icon="pi pi-pencil"
                        label="Edit"
                        class="flex-auto md:flex-initial white-space-nowrap"
                        @click="editContact(item.contactId)"
                      ></Button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </template>
      </DataView>
    </div>
  </ScrollPanel>
  <edit-contact-modal-vue
    v-if="editContactModal"
    :openModal="editContactModal"
    :contact="currentContact"
    @close-modal="getModalState"
    @update-contact-event="handleUpdateContact"
  ></edit-contact-modal-vue>
</template>

<script>
import DataView from "primevue/dataview";
import ScrollPanel from "primevue/scrollpanel";
import EditContactModalVue from "./EditContactModal.vue";
import { deleteContact, getContacts } from "@/services/contactsService";

export default {
  components: {
    DataView,
    ScrollPanel,
    EditContactModalVue,
  },
  props: ["nameInput", "hasNewContact"],
  data() {
    return {
      contacts: [],
      filterValue: "",
      emptyListMessage: "Your contact list is empty.",
      loaded: false,
      editContactModal: false,
      currentContact: {},
    };
  },
  computed: {
    filteredContacts() {
      return (
        this.contacts
          .filter((contact) =>
            contact.name.toLowerCase().includes(this.filterValue.toLowerCase())
          )
          .sort() || []
      );
    },
    async atualizedContacts() {
      await this.fetchContacts();
    },
  },
  watch: {
    hasNewContact(value) {
      console.log(value);
      if (value) {
        return this.fetchContacts();
      }
      value = false;
      return null;
    },
    nameInput(value) {
      this.filterValue = value ? value : "";
    },
    contacts(value) {
      return value;
    },
  },
  async created() {
    await this.fetchContacts();
  },
  methods: {
    async fetchContacts() {
      this.contacts = await getContacts();
    },
    editContact(contactId) {
      this.editContactModal = true;
      this.currentContact = this.contacts.find(
        (contact) => contact.contactId === contactId
      );
    },
    async deleteContact(contactId, userId) {
      return await deleteContact(contactId, userId).then((req) => {
        this.$toast.add({
          severity: "success",
          summary: "Success!",
          detail: "Contact deleted.",
          life: 3000,
        });
        this.fetchContacts();
      });
    },
    getModalState(btnInfo) {
      this.editContactModal = btnInfo;
    },
    async handleUpdateContact(messageData) {
      this.$toast.add({
        severity: "info",
        summary: "Success!",
        detail: messageData.message,
        life: 3000,
      });
      await this.fetchContacts();
    },
  },
};
</script>
