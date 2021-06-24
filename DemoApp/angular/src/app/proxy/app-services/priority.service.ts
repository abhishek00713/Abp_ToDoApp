import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreatePriorityDto, GetPriorityListDto, PriorityDto, UpdatePriorityDto } from '../priority-dtos/models';

@Injectable({
  providedIn: 'root',
})
export class PriorityService {
  apiName = 'Default';

  createASyncByInput = (input: CreatePriorityDto) =>
    this.restService.request<any, PriorityDto>({
      method: 'POST',
      url: '/api/app/priority/a-sync',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/priority/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, PriorityDto>({
      method: 'GET',
      url: `/api/app/priority/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: GetPriorityListDto) =>
    this.restService.request<any, PagedResultDto<PriorityDto>>({
      method: 'GET',
      url: '/api/app/priority',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdatePriorityDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/priority/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
