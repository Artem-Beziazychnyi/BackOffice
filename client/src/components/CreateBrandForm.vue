<template>
  <div>
    <h4>Create brand</h4>
    <form @submit.prevent="createBrand">
      <div class="form-group">
        <label for="name">Name</label>
        <input id="name" class="form-control" v-model="name" @keypress="duplicateValidate" />
        <span class="error" style="color: red" v-if="errors.length>0">{{errors[0]}}</span>
      </div>
      <div class="form-group">
        <label for="quantity">Quantity</label>
        <input id="quantity" class="form-control" v-model.number="quantity" type="number" required />
      </div>
      <button type="submit" class="btn btn-primary">Submit</button>
    </form>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "CreateBrandForm",
  data() {
    return {
      quantity: null,
      name: null,
      errors: []
    };
  },

  methods: {
    async createBrand() {
      if (
        this.$store.state.brands.filter(b => b.name === this.name).length > 0
      ) {
        this.errors.push("Duplicate name. Please, enter a unique name");
        return;
      } else {
        this.errors = [];
      }

      const data = new FormData();

      data.append("quantity", this.quantity);
      data.append("name", this.name);

      await axios
        .post("https://localhost:5001/api/brands", data)
        .then(responce => {
          this.$store.dispatch("createBrand", {
            id: responce.data.id,
            sum: this.quantity,
            name: responce.data.name,
            numberOfUploads: 1
          });
        });

      this.quantity = null;
      this.name = null;
    },
    duplicateValidate() {}
  }
};
</script>
