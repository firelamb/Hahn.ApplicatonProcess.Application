import { PLATFORM } from "aurelia-pal";
import { inject } from "aurelia-dependency-injection";
import {
  ValidationControllerFactory,
  ValidationRules,
  ValidationController,
  Validator,
} from "aurelia-validation";
import { Router } from "aurelia-router";
import { DialogService } from "aurelia-dialog";
import { AssetService } from "../services/asset.service";
import { DepartmentService } from "../services/department.service";
import { DepartmentModel } from "../models/deparment.model";
import { BootstrapFormRenderer } from "../bootstrap-form-renderer/bootstrap-form-renderer";
import { DepartmentEnum } from "../models/asset.model";
import { MessageModal } from "../modal/message-modal";
import { MessageModalModel } from "../models/message-modal.model";
import { ErrorToastr } from "../modal/error-toastr";
PLATFORM.moduleName("../modal/error-toastr");

@inject(
  ValidationControllerFactory,
  DialogService,
  DepartmentService,
  Router,
  Validator,
  AssetService
)
export class CreateAssetForm {
  //Model properties
  assetName: string;
  department: DepartmentEnum;
  countryOfDepartment: string;
  eMailAdressOfDepartment: string;
  purchaseDate: Date;
  broken: boolean;

  controller: ValidationController;
  messageModel = new MessageModalModel();
  departments = new Array<DepartmentModel>();

  constructor(
    private controllerFactory: ValidationControllerFactory,
    private dialogService: DialogService,
    private departmentService: DepartmentService,
    private router: Router,
    private validator: Validator,
    private assetService: AssetService
  ) {
    this.controller = controllerFactory.createForCurrentScope();
    this.controller.addRenderer(new BootstrapFormRenderer());
  }

  async created(owningView, myView) {
    // Invoked once the component is created...
    this.departments = await this.departmentService.getDepartments();
  }

  async submit() {
    const validation = await this.controller.validate();

    if (validation.valid) {
      const result = await this.assetService.insert({
        assetName: this.assetName,
        department: this.department,
        countryOfDepartment: this.countryOfDepartment,
        eMailAdressOfDepartment: this.eMailAdressOfDepartment,
        purchaseDate: this.purchaseDate,
        broken: this.broken,
      });

      const errors = result["errors"];
      if (errors) {
        this.dialogService.open({ viewModel: ErrorToastr, model: errors });
        return;
      }

      if (result.id) {
        sessionStorage.setItem("asset", JSON.stringify(result));
        this.router.navigateToRoute("createasset/confirm");
      }
    }
  }

  reset() {
    this.messageModel.Message = "You're really sure to reset all the data?";

    this.dialogService
      .open({ viewModel: MessageModal, model: this.messageModel })
      .whenClosed(({ wasCancelled }) => {
        if (wasCancelled) return;

        this.assetName = undefined;
        this.department = undefined;
        this.countryOfDepartment = undefined;
        this.eMailAdressOfDepartment = undefined;
        this.purchaseDate = undefined;
        this.broken = undefined;
      });
  }

  get canSave() {
    return this.controller?.errors?.length > 0;
  }

  get canReset() {
    return (
      this.assetName ||
      this.department ||
      this.countryOfDepartment ||
      this.eMailAdressOfDepartment ||
      this.purchaseDate ||
      this.broken
    );
  }
}

ValidationRules.customRule(
  "date",
  (value, _) =>
    value === null ||
    value === undefined ||
    value instanceof Date ||
    !isNaN(Date.parse(value)),
  `Purchase date must be a date.`
);

ValidationRules.customRule(
  "oneyeardate",
  (value, _) => {
    const date = new Date();
    const year = date.getFullYear();
    const month = date.getMonth();
    const day = date.getDate();
    const newDate = new Date(year - 1, month, day);

    return Date.parse(value) >= newDate.getTime();
  },

  `Purchase date cannot be older than one year.`
);

ValidationRules.ensure((a: CreateAssetForm) => a.assetName)
  .required()
  .withMessage(`Asset name cannot be blank.`)
  .minLength(5)
  .withMessage("Asset name – at least 5 Characters")
  .ensure((a: CreateAssetForm) => a.department)
  .required()
  .withMessage(`Country of department cannot be blank.`)
  .ensure((a: CreateAssetForm) => a.countryOfDepartment)
  .required()
  .withMessage(`Country of department cannot be blank.`)
  .ensure((a: CreateAssetForm) => a.eMailAdressOfDepartment)
  .required()
  .withMessage(`EMail adress of department cannot be blank.`)
  .email()
  .withMessage(`EMail adress of department should be an valid email.`)
  .ensure((a: CreateAssetForm) => a.purchaseDate)
  .required()
  .withMessage(`Purchase date cannot be blank.`)
  .satisfiesRule("date")
  .satisfiesRule("oneyeardate")
  .on(CreateAssetForm);
