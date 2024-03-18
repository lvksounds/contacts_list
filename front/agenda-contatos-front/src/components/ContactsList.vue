<template>
  <div class="card">
    <DataView :value="filteredContacts">
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
              <div class="md:w-4rem relative">
                <Avatar
                  :label="item.profileImg ? null : item.name.substring(0, 1)"
                  :image="item.profileImg ? item.profileImg : null"
                  size="xlarge"
                  shape="circle"
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
                  </div>
                  <div
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
                  </div>
                </div>
                <div class="flex flex-column md:align-items-end gap-5">
                  <div class="flex flex-row-reverse md:flex-row gap-2">
                    <Button icon="pi pi-heart" outlined></Button>
                    <Button
                      icon="pi pi-pencil"
                      label="Edit"
                      class="flex-auto md:flex-initial white-space-nowrap"
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
</template>

<script>
import DataView from "primevue/dataview";

import getContacts from "@/services/contactsService";

export default {
  components: {
    DataView,
  },
  props: ["nameInput"],
  data() {
    return {
      contacts: [],
      filterValue: "",
      loaded: false,
    };
  },
  computed: {
    filteredContacts() {
      return (
        this.contacts.filter((contact) =>
          contact.name.toLowerCase().includes(this.filterValue.toLowerCase())
        ) || []
      );
    },
  },
  watch: {
    nameInput(value) {
      this.filterValue = value ? value : "";
    },
    contacts(value) {
      return value;
    },
  },
  async created() {
    this.contacts = await this.fetchContacts();
  },
  methods: {
    async fetchContacts() {
      return await getContacts();
    },
  },
};
</script>
