import { inject } from "aurelia-framework";
import { DialogController } from "aurelia-dialog";
import { MessageModalModel } from "../models/message-modal.model";
import { useView } from "aurelia-framework";

@useView("./message-modal.html")
@inject(DialogController)
export class MessageModal {
  model: MessageModalModel;
  constructor(public controller: DialogController) {}

  activate(model) {
    this.model = model;
  }
}
