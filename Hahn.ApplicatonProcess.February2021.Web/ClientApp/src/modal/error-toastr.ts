import { inject } from "aurelia-framework";
import { DialogController } from "aurelia-dialog";
import { useView } from "aurelia-framework";

@useView("./error-toastr.html")
@inject(DialogController)
export class ErrorToastr {
  errors = [];

  constructor(public controller: DialogController) {}

  activate(model) {
    for (const [_, value] of Object.entries(model)) {
      if (value && Array.isArray(value) && value.length > 0) {
        this.errors = [this.errors, ...value];
      }
    }

    this.errors = this.errors.filter((f) => f && f.length > 0);
  }
}
