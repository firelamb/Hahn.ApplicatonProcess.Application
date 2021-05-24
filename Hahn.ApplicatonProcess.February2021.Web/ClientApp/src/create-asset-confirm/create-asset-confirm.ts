import { inject } from "aurelia-dependency-injection";
import { AssetModel } from "../models/asset.model";
import { Router } from "aurelia-router";
import { DepartmentModel } from "../models/deparment.model";
import { DepartmentService } from "../services/department.service";

@inject(Router, DepartmentService)
export class CreateAssetConfirm {
  model: AssetModel;
  departments = new Array<DepartmentModel>();
  purchaseDate: string;

  constructor(
    private router: Router,
    private departmentService: DepartmentService
  ) {
    const asset = sessionStorage.getItem("asset");

    if (asset) {
      this.model = JSON.parse(asset);

      try {
        const arrayDate = this.model.purchaseDate
          .toString()
          .split("T")[0]
          .split("-");

        if (arrayDate.length == 3) {
          this.purchaseDate = `${arrayDate[2]}/${arrayDate[1]}/${arrayDate[0]}`;
        }
      } catch (error) {}
    } else {
      this.createAnotherAsset();
    }
  }

  async created(owningView, myView) {
    // Invoked once the component is created...
    this.departments = await this.departmentService.getDepartments();
  }

  createAnotherAsset() {
    this.router.navigateToRoute("createasset");
  }
}
