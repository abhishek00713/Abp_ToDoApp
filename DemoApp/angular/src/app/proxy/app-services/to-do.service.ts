import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateToDoDto, GetAllToDoListDto, GetToDoListDto, ToDoDto, UpdateToDoDto } from '../to-do-dtos/models';

@Injectable({
  providedIn: 'root',
})
export class ToDoService {
  apiName = 'Default';

  createASyncByInput = (input: CreateToDoDto) =>
    this.restService.request<any, ToDoDto>({
      method: 'POST',
      url: '/api/app/to-do/a-sync',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/to-do/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, ToDoDto>({
      method: 'GET',
      url: `/api/app/to-do/${id}`,
    },
    { apiName: this.apiName });

  getAllList = (input: GetAllToDoListDto) =>
    this.restService.request<any, PagedResultDto<ToDoDto>>({
      method: 'GET',
      url: '/api/app/to-do/list',
      params: { filterCategory: input.filterCategory, filterPriority: input.filterPriority, filterStatus: input.filterStatus, filterTask: input.filterTask, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  getList = (input: GetToDoListDto) =>
    this.restService.request<any, PagedResultDto<ToDoDto>>({
      method: 'GET',
      url: '/api/app/to-do',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateToDoDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/to-do/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
