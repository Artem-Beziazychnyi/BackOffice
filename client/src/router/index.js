import Vue from "vue";
import Router from "vue-router";
import BrandsTable from "@/components/BrandsTable.vue";
import UploadFile from "@/components/UploadFile.vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "upload",
      component: UploadFile
    },
    {
      path: "/brands",
      name: "brands",
      component: BrandsTable
    }
  ]
});
