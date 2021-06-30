import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateProductDto, GetProductListDto, ProductDto, UpdateProductDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  apiName = 'Default';

  createASyncByInput = (input: CreateProductDto) =>
    this.restService.request<any, ProductDto>({
      method: 'POST',
      url: '/api/app/product/a-sync',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/product/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, ProductDto>({
      method: 'GET',
      url: `/api/app/product/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: GetProductListDto) =>
    this.restService.request<any, PagedResultDto<ProductDto>>({
      method: 'GET',
      url: '/api/app/product',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateProductDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/product/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
