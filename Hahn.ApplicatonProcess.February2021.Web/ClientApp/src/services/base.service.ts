import { HttpClient } from "aurelia-fetch-client";
import * as environment from "../../config/environment.json";

export abstract class BaseService {
  httpClient = new HttpClient();
  urlApi = environment.urlEndPoint;
}
