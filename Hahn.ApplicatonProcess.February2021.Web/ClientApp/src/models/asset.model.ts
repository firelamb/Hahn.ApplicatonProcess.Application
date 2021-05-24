export class AssetModel {
  id: number;
  assetName: string;
  department: DepartmentEnum;
  countryOfDepartment: string;
  eMailAdressOfDepartment: string;
  purchaseDate: Date;
  broken: boolean;
}

export enum DepartmentEnum {
  HQ = 1,
  Store1,
  Store2,
  Store3,
  MantenanceStation,
}
