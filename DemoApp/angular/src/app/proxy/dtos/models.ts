import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateProductDto {
  name: string;
  price: number;
  quantity: number;
  productType?: string;
}

export interface GetProductListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface ProductDto extends EntityDto<string> {
  name?: string;
  price: number;
  quantity: number;
  productType?: string;
}

export interface UpdateProductDto {
  name: string;
  price: number;
  quantity: number;
  productType?: string;
}
