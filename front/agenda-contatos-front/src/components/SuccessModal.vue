<template>
  <Modal
    v-model:visible="visible"
    modal
    header="Sucesso"
    :style="{ width: '26rem' }"
  >
    <div class="flex flex-row justify-content-center align-items-center">
      <p class="font-semibold">
        {{ message }}
      </p>
    </div>
  </Modal>
</template>

<style>
.p-dialog-header {
  color: green;
}
</style>

<script>
export default {
  props: {
    message: String,
    hasMessage: Boolean,
  },
  data() {
    return {
      visible: this.hasMessage,
    };
  },

  methods: {
    closeModal() {
      setTimeout(() => {
        if (this.visible === true) {
          this.visible = false;
        }
      }, 5000);
    },
  },
};
</script>
