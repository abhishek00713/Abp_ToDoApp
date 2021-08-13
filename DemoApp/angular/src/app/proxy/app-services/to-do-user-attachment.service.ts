import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateToDoUserAttachmentDto, GetToDoUserAttachmentListDto, ToDoUserAttachmentDto, UpdateToDoUserAtatchmentDto } from '../to-do-user-attachment-dtos/models';

@Injectable({
  providedIn: 'root',
})
export class ToDoUserAttachmentService {
  apiName = 'Default';

  createASyncByInput = (input: CreateToDoUserAttachmentDto) =>
    this.restService.request<any, ToDoUserAttachmentDto>({
      method: 'POST',
      url: '/api/app/to-do-user-attachment/a-sync',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, boolean>({
      method: 'DELETE',
      url: `/api/app/to-do-user-attachment/${id}`,
    },
    { apiName: this.apiName });

  deleteBlobFile = (fname: string) =>
    this.restService.request<any, boolean>({
      method: 'DELETE',
      url: '/api/app/to-do-user-attachment/blob-file',
      params: { fname },
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, ToDoUserAttachmentDto>({
      method: 'GET',
      url: `/api/app/to-do-user-attachment/${id}`,
    },
    { apiName: this.apiName });

  getBlob = (fname: string) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/app/to-do-user-attachment/blob',
      params: { fname },
    },
    { apiName: this.apiName });

  getList = (input: GetToDoUserAttachmentListDto) =>
    this.restService.request<any, PagedResultDto<ToDoUserAttachmentDto>>({
      method: 'GET',
      url: '/api/app/to-do-user-attachment',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  saveBlob = (bytes: number[], fname: string) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/to-do-user-attachment/save-blob',
      params: { fname },
      body: bytes,
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateToDoUserAtatchmentDto) =>
    this.restService.request<any, ToDoUserAttachmentDto>({
      method: 'PUT',
      url: `/api/app/to-do-user-attachment/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
