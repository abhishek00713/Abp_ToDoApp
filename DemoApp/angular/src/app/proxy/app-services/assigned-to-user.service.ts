import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AssignedToUserDto, CreateAssignedToUserDto, GetAssignedToUserListDto, UpdateAssignedToUserDto } from '../assigned-to-user-dtos/models';

@Injectable({
  providedIn: 'root',
})
export class AssignedToUserService {
  apiName = 'Default';

  createASyncByInput = (input: CreateAssignedToUserDto) =>
    this.restService.request<any, AssignedToUserDto>({
      method: 'POST',
      url: '/api/app/assigned-to-user/a-sync',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/assigned-to-user/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, AssignedToUserDto>({
      method: 'GET',
      url: `/api/app/assigned-to-user/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: GetAssignedToUserListDto) =>
    this.restService.request<any, PagedResultDto<AssignedToUserDto>>({
      method: 'GET',
      url: '/api/app/assigned-to-user',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateAssignedToUserDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/assigned-to-user/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
