import "bootstrap";
import "bootstrap/dist/css/bootstrap.css";
import { RouterConfiguration, Router } from "aurelia-router";
import { PLATFORM } from "aurelia-framework";

export class App {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router): void {
    this.router = router;
    config.title = "Asset";

    config.map([
      {
        route: ["", "createasset"],
        name: "createasset",
        moduleId: PLATFORM.moduleName("create-asset/create-asset"),
        nav: true,
        title: "Create asset",
      },
      {
        route: "createasset/confirm",
        name: "createasset/confirm",
        moduleId: PLATFORM.moduleName(
          "create-asset-confirm/create-asset-confirm"
        ),
        title: "Create asset confirmation",
      },
    ]);
  }
}
