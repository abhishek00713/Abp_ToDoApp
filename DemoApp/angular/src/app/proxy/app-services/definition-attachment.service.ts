import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateDefinitionAttachmentDto, DefinitionAttachmentDto, GetDefinitionAttachmentListDto, UpdateDefinitionAttachmentDto } from '../definition-attachment-dtos/models';
import type { IBlobContainer } from '../volo/abp/blob-storing/models';

@Injectable({
  providedIn: 'root',
})
export class DefinitionAttachmentService {
  apiName = 'Default';

  createASyncByInput = (input: CreateDefinitionAttachmentDto) =>
    this.restService.request<any, DefinitionAttachmentDto>({
      method: 'POST',
      url: '/api/app/definition-attachment/a-sync',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, boolean>({
      method: 'DELETE',
      url: `/api/app/definition-attachment/${id}`,
    },
    { apiName: this.apiName });

  deleteBlobFile = (fname: string) =>
    this.restService.request<any, boolean>({
      method: 'DELETE',
      url: '/api/app/definition-attachment/blob-file',
      params: { fname },
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, DefinitionAttachmentDto>({
      method: 'GET',
      url: `/api/app/definition-attachment/${id}`,
    },
    { apiName: this.apiName });

  getBlobFile = (fname: string) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/app/definition-attachment/blob-file',
      params: { fname },
    },
    { apiName: this.apiName });

  getList = (input: GetDefinitionAttachmentListDto) =>
    this.restService.request<any, PagedResultDto<DefinitionAttachmentDto>>({
      method: 'GET',
      url: '/api/app/definition-attachment',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  saveBlobFile = (container: IBlobContainer, bytes: number[], fname: string) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/definition-attachment/save-blob-file',
      params: { fname },
      body: bytes,
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateDefinitionAttachmentDto) =>
    this.restService.request<any, DefinitionAttachmentDto>({
      method: 'PUT',
      url: `/api/app/definition-attachment/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
