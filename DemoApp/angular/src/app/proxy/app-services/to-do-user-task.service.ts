import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateToDoUserTaskDto, GetToDoUserTaskListDto, ToDoUserTaskDto, UpdateToDoUserTaskDto } from '../to-do-user-task-dtos/models';

@Injectable({
  providedIn: 'root',
})
export class ToDoUserTaskService {
  apiName = 'Default';

  createASyncByInput = (input: CreateToDoUserTaskDto) =>
    this.restService.request<any, ToDoUserTaskDto>({
      method: 'POST',
      url: '/api/app/to-do-user-task/a-sync',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/to-do-user-task/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, ToDoUserTaskDto>({
      method: 'GET',
      url: `/api/app/to-do-user-task/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: GetToDoUserTaskListDto) =>
    this.restService.request<any, PagedResultDto<ToDoUserTaskDto>>({
      method: 'GET',
      url: '/api/app/to-do-user-task',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateToDoUserTaskDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/to-do-user-task/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
