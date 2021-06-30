import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class OrderManagerService {
  apiName = 'Default';

  testWithDetailsById = (id: string) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/order-manager/${id}/test-with-details`,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
