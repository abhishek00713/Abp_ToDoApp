import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateStatusDto, GetStatusListDto, StatusDto, UpdateStatusDto } from '../status-dtos/models';

@Injectable({
  providedIn: 'root',
})
export class StatusService {
  apiName = 'Default';

  createASyncByInput = (input: CreateStatusDto) =>
    this.restService.request<any, StatusDto>({
      method: 'POST',
      url: '/api/app/status/a-sync',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/status/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, StatusDto>({
      method: 'GET',
      url: `/api/app/status/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: GetStatusListDto) =>
    this.restService.request<any, PagedResultDto<StatusDto>>({
      method: 'GET',
      url: '/api/app/status',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateStatusDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/status/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
