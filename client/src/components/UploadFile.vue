<template>
  <div class="upload">
    <input type="file" @change="upload" />
  </div>
</template>
<script>
import axios from "axios";

export default {
  methods: {
    upload(e) {
      if (e.target.files.length > 0) {
        const data = new FormData();
        data.append("file", e.target.files[0]);

        axios
          .post("https://localhost:5001/api/upload", data, {
            contentType: "multypart/form-data"
          })
          .then(() => {
            this.$router.push("brands");
          });
      }
    }
  }
};
</script>
<style scoped>
.upload {
  margin: 20px;
  text-align: center;
}
</style>
