<template>
  <div class="flex flex-row gap-2">
    <div
      class="flex flex-row gap-2 align-items-center w-full justify-content-center"
    >
      <p for="username">Pesquisar contatos</p>
      <InputText
        id="username"
        v-model="value"
        aria-describedby="username-help"
        class="w-9 h-2rem p-2"
      />
    </div>
  </div>
</template>

<script>
export default {};
</script>
