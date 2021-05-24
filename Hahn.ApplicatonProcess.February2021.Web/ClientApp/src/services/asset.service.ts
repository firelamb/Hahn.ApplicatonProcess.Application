import { AssetModel } from "../models/asset.model";
import { BaseService } from "./base.service";
export class AssetService extends BaseService {
  // getAllData(): Promise<Array<AssetModel>> {
  //   return this.httpClient
  //     .fetch("http://localhost:51891/Applicant/GetAll")
  //     .then((response) => response.json())
  //     .then((data) => JSON.parse(data));
  // }

  // getData(id): Promise<AssetModel> {
  //   return this.httpClient
  //     .fetch("http://localhost:51891/Applicant/GetById?id=" + parseInt(id.id), {
  //       method: "GET",
  //     })
  //     .then((response) => response.json())
  //     .then((data) => JSON.parse(data));
  // }

  async insert(model: Partial<AssetModel>): Promise<AssetModel> {
    const response = await this.httpClient.post(
      `${this.urlApi}Asset/Insert`,
      JSON.stringify(model)
    );

    return await response.json();
  }

  // updateData(updateData: AssetModel): Promise<AssetModel> {
  //   return this.httpClient
  //     .fetch("http://localhost:51891/Applicant/Update", {
  //       method: "PUT",
  //       body: JSON.stringify(updateData),
  //     })
  //     .then((response) => response.json())
  //     .then((data) => JSON.parse(data));
  // }

  // deleteData(id): Promise<boolean> {
  //   return this.httpClient
  //     .fetch("http://localhost:51891/Applicant/Delete?id=" + id, {
  //       method: "DELETE",
  //     })
  //     .then((response) => response.json())
  //     .then((data) => JSON.parse(data));
  // }
}
