import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CategoryDto extends EntityDto<string> {
  categoryName?: string;
}

export interface CreateCategoryDto {
  categoryName: string;
}

export interface GetCategoryListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UpdateCategoryDto {
  categoryName: string;
}
