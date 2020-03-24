<template>
  <div class="wrapp d-flex flex-column flex-md-row">
    <div class="brandsTable col-md-6 col-sm-12">
      <table class="table">
        <thead>
          <tr>
            <th scope="col" @click="sortByName">Brand</th>
            <th scope="col" @click="sortBySum">Sum</th>
            <th scope="col" @click="sortByNumberOfUploads">Number of changes</th>
            <th scope="col"></th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="brand in brands" :key="brand.id">
            <th>{{ brand.name }}</th>
            <th>{{ brand.sum }}</th>
            <th>{{ brand.numberOfUploads }}</th>
            <th>
              <button @click="removeBrand(brand.id)">Remove</button>
            </th>
            <th>
              <button @click="editingBrand = brand; show = true;">Edit</button>
            </th>
          </tr>
          <tr>
            <th>Brand's</th>
            <th>{{sum}}</th>
          </tr>
        </tbody>
      </table>
      <div class="editBrandForm" v-if="show">
        <EditBrandForm :brand="editingBrand" show="show" @brand-updated="show=false" />
      </div>
    </div>
    <div class="createBrandForm col-md-6 col-12">
      <CreateBrandForm />
    </div>
  </div>
</template>

<script>
import axios from "axios";
import { mapState } from "vuex";
import CreateBrandForm from "@/components/CreateBrandForm.vue";
import EditBrandForm from "@/components/EditBrandForm.vue";

export default {
  name: "BrandsTable",
  computed: mapState(["brands", "sum"]),
  data() {
    return {
      editingBrand: {},
      show: false
    };
  },
  components: {
    CreateBrandForm,
    EditBrandForm
  },
  methods: {
    async removeBrand(id) {
      await axios.delete(`https://localhost:5001/api/brands/${id}`).then(() => {
        this.$store.dispatch("deleteBrand", id);
      });
    },
    async editBrand(id) {
      await axios.put(`https://localhost:5001/api/brands/${id}`).then(() => {
        this.$store.dispatch("deleteBrand", id);
      });
    },
    sortByNumberOfUploads() {
      this.brands.sort((a, b) => {
        return a.numberOfUploads - b.numberOfUploads;
      });
    },
    sortBySum() {
      this.brands.sort((a, b) => {
        return a.sum - b.sum;
      });
    },
    sortByName() {
      this.brands.sort((a, b) => {
        var x = a.name.toLowerCase();
        var y = b.name.toLowerCase();
        if (x < y) {
          return -1;
        }
        if (x > y) {
          return 1;
        }
        return 0;
      });
    }
  },
  async beforeMount() {
    await axios.get("https://localhost:5001/api/brands").then(response => {
      this.$store.dispatch("setBrand", response.data);
    });
    await axios.get("https://localhost:5001/api/brands/sum").then(response => {
      this.$store.dispatch("setSum", response.data);
    });
  }
};
</script>