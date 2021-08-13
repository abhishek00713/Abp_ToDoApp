import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateToDo_Assigned, GetToDo_Assigned, ToDoAssignedDto, UpdateToDo_Assigned } from '../to-do-assigned-dtos/models';

@Injectable({
  providedIn: 'root',
})
export class ToDoAssignedService {
  apiName = 'Default';

  createASyncByInput = (input: CreateToDo_Assigned) =>
    this.restService.request<any, ToDoAssignedDto>({
      method: 'POST',
      url: '/api/app/to-do-assigned/a-sync',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/to-do-assigned/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, ToDoAssignedDto>({
      method: 'GET',
      url: `/api/app/to-do-assigned/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: GetToDo_Assigned) =>
    this.restService.request<any, PagedResultDto<ToDoAssignedDto>>({
      method: 'GET',
      url: '/api/app/to-do-assigned',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateToDo_Assigned) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/to-do-assigned/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
