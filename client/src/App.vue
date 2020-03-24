
<template>
  <div id="app">
    <div class="nav col d-flex justify-content-center mb-4">
      <ul class="nav nav-tabs">
        <li class="nav-item">
          <router-link :to="{ name: 'upload' }" class="nav-link" active-class="active">Upload</router-link>
        </li>
        <li class="nav-item">
          <router-link :to="{ name: 'brands' }" class="nav-link" active-class="active">Brands</router-link>
        </li>
      </ul>
    </div>
    <router-view />
  </div>
</template>

<script>
export default {
  name: "App"
};
</script>
<style scoped>
</style>