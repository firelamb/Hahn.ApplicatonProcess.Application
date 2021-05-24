import { DepartmentModel } from "../models/deparment.model";
import { BaseService } from "./base.service";

export class DepartmentService extends BaseService {
  async getDepartments(): Promise<DepartmentModel[]> {
    const response = await this.httpClient.get(
      `${this.urlApi}Department/GetAll`
    );

    return await response.json();
  }
}
