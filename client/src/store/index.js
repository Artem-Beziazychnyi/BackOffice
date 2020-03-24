import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    brands: new Array,
    sum: 0
  },
  mutations: {
    ADD_BRAND(state, brand) {
      state.brands.push(brand);
      state.sum += brand.sum;
    },
    SET_BRANDS(state, brands) {
      state.brands = brands;
    },
    DELETE_BRAND(state, id) {
      var removeIndex = state.brands
        .map(brand => {
          return brand.id;
        })
        .indexOf(id);
      state.sum -= state.brands[removeIndex].sum;
      state.brands.splice(removeIndex, 1);
    },
    SET_SUM(state, sum) {
      state.sum = sum;
    }
  },
  actions: {
    createBrand({ state, commit }, brand) {
      if (state.brands) {
        commit("ADD_BRAND", brand);
      }
    },
    deleteBrand({ state, commit }, id) {
      if (state.brands) {
        commit("DELETE_BRAND", id);
      }
    },
    setBrand({ state, commit }, brands) {
      if (state.brands) {
        commit("SET_BRANDS", brands);
      }
    },
    setSum({ state, commit }, sum) {
      if (state.brands) {
        commit("SET_SUM", sum);
      }
    }
  },
  modules: {}
});
