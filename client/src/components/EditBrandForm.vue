<template>
  <div>
    <h4>
      Edit
      <b>{{ brand.name }}</b> quantity
    </h4>
    <form @submit.prevent="save">
      <div class="form-group">
        <label for="name">Quantity</label>
        <input
          v-model.number="quantity"
          class="form-control"
          type="number"
          @keydown.enter.prevent="save"
        />
      </div>
      <button type="submit" class="btn btn-primary">Save</button>
    </form>
  </div>
</template>

<script>
import axios from "axios";

export default {
  props: {
    brand: {
      type: Object
    }
  },
  data() {
    return {
      quantity: 0
    };
  },
  methods: {
    async save() {
      const data = new FormData();
      data.append("quantity", this.quantity);
      axios
        .put(`https://localhost:5001/api/brands/${this.brand.id}`, data)
        .then(() => {
          this.brand.sum += this.quantity;
          this.$store.state.sum += this.quantity;
          this.$emit("brand-updated");
        });
    }
  }
};
</script>
